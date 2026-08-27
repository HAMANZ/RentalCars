using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RentalCar.DomainLayer.Model;
using System;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Transactions;

namespace RentalCar.DomainLayer.Models
{
    public partial class RentalCarDbContext : IdentityDbContext
    {
        public RentalCarDbContext()
        {
        }

        public RentalCarDbContext(DbContextOptions<RentalCarDbContext> options)
            : base(options)
        {

        }

        // Pins Created_at to a fixed value for HasData seed rows that don't set it explicitly.
        // BaseEntity.Created_at defaults to DateTime.UtcNow, which is evaluated fresh every time
        // OnModelCreating runs (i.e. every build/migration scaffold) - without this, EF would see
        // "changed" seed data on every scaffold and generate spurious UpdateData migrations.
        private static T[] PinCreatedAt<T>(params T[] items) where T : BaseEntity
        {
            foreach (var item in items)
            {
                item.Created_at = new DateTime(2026, 1, 1);
            }
            return items;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<EUser>().ToTable("EUser");
            // Party entities inherit EUser -> use Table-Per-Type so each gets its own table
            // (otherwise EF collapses them onto the IdentityUser "User" table with no discriminator).
            builder.Entity<CarOwner>().ToTable("CarOwners");
            builder.Entity<Customer>().ToTable("Customers");
            builder.Entity<Investor>().ToTable("Investors");
            builder.Entity<PlateOwner>().ToTable("PlateOwners");
            builder.Entity<Supplier>().ToTable("Suppliers");
            builder.HasDefaultSchema("dbo");
            builder.Entity<STransaction>()
      .HasOne(t => t.DebitAccount)
      .WithMany(a => a.DebitTransactions)
      .HasForeignKey(t => t.DebitAccountId)
      .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<EUser>()
    .HasOne(x => x.Gender)
    .WithMany()
    .HasForeignKey(x => x.GenderId)
    .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<STransaction>()
                .HasOne(t => t.CreditAccount)
                .WithMany(a => a.CreditTransactions)
                .HasForeignKey(t => t.CreditAccountId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            builder.Entity<MenuItem>()
     .HasOne(x => x.Parent)
     .WithMany(x => x.Children)
     .HasForeignKey(x => x.ParentId)
     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserMenuPermission>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserMenuPermission>()
                .HasOne(x => x.MenuItem)
                .WithMany(x => x.UserPermissions)
                .HasForeignKey(x => x.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserMenuPermission>()
                .HasIndex(x => new { x.UserId, x.MenuItemId })
                .IsUnique();

            // Id/ConcurrencyStamp are pinned (IdentityRole's ctor otherwise assigns Guid.NewGuid()
            // and ConcurrencyStamp defaults to Guid.NewGuid() too), so re-scaffolding a migration
            // doesn't produce spurious Role churn. Values match what is already applied in the DB.
            builder.Entity<IdentityRole>().HasData(new IdentityRole[] {
                  new IdentityRole{Id="952d1a6e-9779-480d-8d37-18c09326f3db",ConcurrencyStamp="8fafc2bc-6ac0-44b0-94d7-844d9ae29748",Name="EUser",NormalizedName="EUSER"},
                  new IdentityRole{Id="e190e721-dc47-46d8-b69d-90292452042d",ConcurrencyStamp="70c89d1b-58dc-4b6d-b5cf-46b9c65f3315",Name="Adminstrator",NormalizedName="ADMINSTRATOR"},
                  new IdentityRole{Id="5f042b95-ef2d-47ad-8984-80f39175ff33",ConcurrencyStamp="ae8414b4-cc41-441d-b3ee-d3cc11a68b95",Name="Customer",NormalizedName="CUSTOMER"},
                  new IdentityRole{Id="4c39da44-ac6e-4690-bbff-06ed5835784a",ConcurrencyStamp="b45ebf8e-e95e-41a4-9a76-ac34a9d313cb",Name="Investor",NormalizedName="INVESTOR"},
                  new IdentityRole{Id="6eac4958-cd2d-45ab-b8ad-bfe6d67362fc",ConcurrencyStamp="ed99bc31-efdf-46cc-a1d2-0809da1e4df1",Name="Accountant",NormalizedName="ACCOUNTANT"},
                  new IdentityRole{Id="ddea2205-f6c2-47f3-8a4b-659dde82d6f1",ConcurrencyStamp="cfaf2859-a1f1-4f97-85ee-f797e0cea255",Name="CarOwner",NormalizedName="CAROWNER"},
                  new IdentityRole{Id="b6ce9d08-161e-4dcf-952e-de668e940a85",ConcurrencyStamp="b6b05b15-edd3-4175-b36a-973a2f8300d9",Name="PlateOwner",NormalizedName="PLATEOWNER"},
                  new IdentityRole{Id="73cf4cfd-e05b-45e4-b5c2-2876714b2b4b",ConcurrencyStamp="584f2b4e-1652-40b7-a8a6-bd33adb746a4",Name="Supplier",NormalizedName="SUPPLIER"},
              });

            builder.Entity<Supplier>().HasData(new Supplier[] {
                  new Supplier{Name="Supplier",Name_ar="Supplier",Id=1, Email="Supplier.S@gmail.com",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
              });


            // Id/ConcurrencyStamp/SecurityStamp are pinned (EUser inherits IdentityUser, whose ctor
            // assigns Guid.NewGuid() to Id, and ConcurrencyStamp/SecurityStamp default to Guid.NewGuid()
            // too), so re-scaffolding a migration doesn't produce spurious User/EUser churn. Values
            // match what is already applied in the DB.
            builder.Entity<EUser>().HasData(new EUser[] {
                  new EUser{Id="ab5c9ba4-887c-4a55-926b-feafe762931f",ConcurrencyStamp="4123e37e-a8f6-4ad8-a76b-9343204c2fa1",SecurityStamp="99f941aa-a98d-4bac-8953-be64cf27eaf0",UserName="admin",NormalizedUserName="ADMIN",GenderId=1, Email="hudaabumayha.ham@gmail.com",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
              });

            builder.Entity<AppSettings>().HasData(new AppSettings[] {
                  new AppSettings{Id=1,Logo="RentalCar.jpg",ApplicationName="RentalCar",Description="",ContactWebsite="",LicenseDetail="",Phone="09999999", Email="oonlinetutoring@gmail.com",Password="P@ssw0rdsse",Facebook="",Twitter="",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
              });
            builder.Entity<Language>().HasData(new Language[] {
                  new Language{Id=1,Name="Arabic",LanguageCode="ar",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
                  new Language{Id = 2, Name="English",LanguageCode="en",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
              });
            builder.Entity<Gender>().HasData(new Gender[] {
                  new Gender{Id=1,Name="Male",Code="M",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
                  new Gender{Id = 2, Name="Female",Code="F",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
              });
            
            builder.Entity<PlateType>().HasData(new PlateType[] {
                  new PlateType{Id=1,Name="Private ",Code="PRIVATE",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
                  new PlateType{Id = 2, Name="Public",Code="P",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
                  new PlateType{Id = 3, Name="Company/Commercial",Code="C",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
                  new PlateType{Id = 4, Name="Rental Vehicle",Code="RENTAL",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
              });
            builder.Entity<PlateRegion>().HasData(new PlateRegion[]
{
    new PlateRegion
    {
        Id = 1,
        Name = "Beirut",
        Code = "B",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },

    new PlateRegion
    {
        Id = 2,
        Name = "Aley",
        Code = "Y",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },

    new PlateRegion
    {
        Id = 3,
        Name = "Jounieh",
        Code = "G",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },

    new PlateRegion
    {
        Id = 4,
        Name = "Nabatieh",
        Code = "N",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },

    new PlateRegion
    {
        Id = 5,
        Name = "Ouzai",
        Code = "O",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },

    new PlateRegion
    {
        Id = 6,
        Name = "Sidon",
        Code = "S",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },

    new PlateRegion
    {
        Id = 7,
        Name = "Tripoli",
        Code = "T",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },

    new PlateRegion
    {
        Id = 8,
        Name = "Baalbek",
        Code = "K",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },

    new PlateRegion
    {
        Id = 9,
        Name = "Zahle",
        Code = "Z",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    }
});

            builder.Entity<City>().HasData(new City[] {
                  new City{Id= 1, Name="AL-Kouds",CountryId=1,Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
                  new City{Id = 2, Name="Beirut",CountryId=2,Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
                  new City{Id = 3, Name="Istanbul",CountryId=3,Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
              });
            builder.Entity<Country>().HasData(new Country[] {
                  new Country{Id = 1, Name="Palestinne",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
                  new Country{Id = 2, Name="Lebanon",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
                  new Country{Id = 3, Name="Turkey",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
              });
            builder.Entity<Brand>().HasData(new Brand[]
            {
    new Brand
    {
        Id = 1,
        Name = "Kia",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 2,
        Name = "Toyota",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 3,
        Name = "Hyundai",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 4,
        Name = "Nissan",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 5,
        Name = "Honda",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 6,
        Name = "Mercedes-Benz",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 7,
        Name = "BMW",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 8,
        Name = "Audi",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 9,
        Name = "Ford",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 10,
        Name = "Chevrolet",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 11,
        Name = "Volkswagen",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 12,
        Name = "Mitsubishi",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 13,
        Name = "Mazda",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 14,
        Name = "Lexus",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 15,
        Name = "Jeep",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 16,
        Name = "Porsche",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 17,
        Name = "Volvo",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 18,
        Name = "Subaru",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 19,
        Name = "Peugeot",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Brand
    {
        Id = 20,
        Name = "Renault",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    }
            });


            builder.Entity<FuelType>().HasData(new FuelType[]
{
    new FuelType
    {
        Id = 1,
        Name = "Gasoline",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new FuelType
    {
        Id = 2,
        Name = "Diesel",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new FuelType
    {
        Id = 3,
        Name = "Hybrid",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new FuelType
    {
        Id = 4,
        Name = "Plug-in Hybrid",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new FuelType
    {
        Id = 5,
        Name = "Electric",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new FuelType
    {
        Id = 6,
        Name = "LPG",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new FuelType
    {
        Id = 7,
        Name = "CNG",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    }
});

            builder.Entity<DocumentType>().HasData(new DocumentType[]
{
    new DocumentType
    {
        Id = 1,
        Name = "Vehicle Registration",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new DocumentType
    {
        Id = 2,
        Name = "Vehicle Insurance",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new DocumentType
    {
        Id = 3,
        Name = "Technical Inspection",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new DocumentType
    {
        Id = 4,
        Name = "Vehicle License",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new DocumentType
    {
        Id = 5,
        Name = "Ownership Certificate",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new DocumentType
    {
        Id = 6,
        Name = "Customs Document",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new DocumentType
    {
        Id = 7,
        Name = "Purchase Invoice",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new DocumentType
    {
        Id = 8,
        Name = "Rental Agreement",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new DocumentType
    {
        Id = 9,
        Name = "Maintenance Record",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new DocumentType
    {
        Id = 10,
        Name = "Other",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    }
});

            builder.Entity<InsuranceType>().HasData(new InsuranceType[]
{
    new InsuranceType
    {
        Id = 1,
        Name = "Third Party",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceType
    {
        Id = 2,
        Name = "Comprehensive",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceType
    {
        Id = 3,
        Name = "Collision Damage Waiver (CDW)",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceType
    {
        Id = 4,
        Name = "Theft Protection",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceType
    {
        Id = 5,
        Name = "Personal Accident Insurance",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceType
    {
        Id = 6,
        Name = "Full Coverage",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    }
});

            builder.Entity<InsuranceCompany>().HasData(new InsuranceCompany[]
{
    new InsuranceCompany
    {
        Id = 1,
        Name = "Medgulf",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 2,
        Name = "Libano-Suisse",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 3,
        Name = "Allianz SNA",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 4,
        Name = "Bankers Assurance",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 5,
        Name = "Fidelity Assurance",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 6,
        Name = "Arope Insurance",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 7,
        Name = "Arabia Insurance",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 8,
        Name = "LIA Assurex",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 9,
        Name = "GroupMed",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 10,
        Name = "Comin Insurance",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 11,
        Name = "Cumberland Insurance",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 12,
        Name = "Securite Assurance",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 13,
        Name = "Commercial Insurance Company",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 14,
        Name = "Trust Insurance",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new InsuranceCompany
    {
        Id = 15,
        Name = "UCA",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    }
});
            builder.Entity<Status>().HasData(new Status[]
            {
    new Status
    {
        Id = 1,
        Name = "Active",
        Is_WorkOrderStatus = true,
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Status
    {
        Id = 2,
        Name = "Expired",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Status
    {
        Id = 3,
        Name = "Pending",
        Is_WorkOrderStatus = true,
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Status
    {
        Id = 4,
        Name = "Cancelled",
        Is_WorkOrderStatus = true,
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Status
    {
        Id = 5,
        Name = "Suspended",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new Status
    {
        Id = 6,
        Name = "Renewal Required",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    }
            });

            builder.Entity<CarStatus>().HasData(new CarStatus[]
{
    new CarStatus
    {
        Id = 1,
        Name = "Available",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new CarStatus
    {
        Id = 2,
        Name = "Rented",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new CarStatus
    {
        Id = 3,
        Name = "Reserved",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new CarStatus
    {
        Id = 4,
        Name = "Maintenance",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new CarStatus
    {
        Id = 5,
        Name = "Accident",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new CarStatus
    {
        Id = 6,
        Name = "Out of Service",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new CarStatus
    {
        Id = 7,
        Name = "Sold",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new CarStatus
    {
        Id = 8,
        Name = "Inactive",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    }
});
            builder.Entity<Branch>().HasData(new Branch[] {
                  new Branch{Id = 1, Name="Saida",Created_at=new DateTime(2026,1,1),Created_by=1,Updated_at=new DateTime(2026,1,1),Updated_by=1,Is_deleted=false},
                         });

            builder.Entity<PaymentMethod>().HasData(new PaymentMethod[]
{
    new PaymentMethod
    {
        Id = 1,
        Name = "Cash",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new PaymentMethod
    {
        Id = 2,
        Name = "OMT",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new PaymentMethod
    {
        Id = 3,
        Name = "WISH",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new PaymentMethod
    {
        Id = 4,
        Name = "Bank Transfer",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new PaymentMethod
    {
        Id = 5,
        Name = "Credit Card",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new PaymentMethod
    {
        Id = 6,
        Name = "Debit Card",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    },
    new PaymentMethod
    {
        Id = 7,
        Name = "Cheque",
        Created_at = new DateTime(2026, 1, 1),
        Created_by = 1,
        Updated_at = new DateTime(2026, 1, 1),
        Updated_by = 1,
        Is_deleted = false
    }
});
            builder.Entity<MenuItem>().HasData(PinCreatedAt(new MenuItem[]
{
    // =====================================================
    // Dashboard
    // =====================================================
    new MenuItem
    {
        Id = 1,
        Name = "Dashboard",
        Title = "Dashboard",
        Icon = "fas fa-tachometer-alt",
        Url = "/Dashboard",
        ParentId = null,
        SortOrder = 1,
        IsActive = true
    },

    // =====================================================
    // Fleet Management
    // =====================================================
    new MenuItem
    {
        Id = 10,
        Name = "FleetManagement",
        Title = "Fleet Management",
        Icon = "fas fa-car",
        Url = "#",
        ParentId = null,
        SortOrder = 2,
        IsActive = true
    },
    new MenuItem
    {
        Id = 11,
        Name = "Cars",
        Title = "Cars",
        Icon = "fas fa-car-side",
        Url = "/Cars",
        ParentId = 10,
        SortOrder = 1,
        IsActive = true
    },
    new MenuItem
    {
        Id = 12,
        Name = "CarOwners",
        Title = "Car Owners",
        Icon = "fas fa-user-tie",
        Url = "/CarOwners",
        ParentId = 10,
        SortOrder = 2,
        IsActive = true
    },
    new MenuItem
    {
        Id = 13,
        Name = "Brands",
        Title = "Brands",
        Icon = "fas fa-tags",
        Url = "/Brands",
        ParentId = 10,
        SortOrder = 3,
        IsActive = true
    },
    new MenuItem
    {
        Id = 14,
        Name = "CarModels",
        Title = "Car Models",
        Icon = "fas fa-car",
        Url = "/CarModels",
        ParentId = 10,
        SortOrder = 4,
        IsActive = true
    },
    new MenuItem
    {
        Id = 15,
        Name = "FuelTypes",
        Title = "Fuel Types",
        Icon = "fas fa-gas-pump",
        Url = "/FuelTypes",
        ParentId = 10,
        SortOrder = 5,
        IsActive = true
    },
    new MenuItem
    {
        Id = 16,
        Name = "CarStatuses",
        Title = "Car Statuses",
        Icon = "fas fa-info-circle",
        Url = "/CarStatuses",
        ParentId = 10,
        SortOrder = 6,
        IsActive = true
    },
    //new MenuItem
    //{
    //    Id = 17,
    //    Name = "LicensePlates",
    //    Title = "License Plates",
    //    Icon = "fas fa-id-card",
    //    Url = "/LicensePlates",
    //    ParentId = 10,
    //    SortOrder = 7,
    //    IsActive = true
    //},
    //new MenuItem
    //{
    //    Id = 18,
    //    Name = "PlateOwnerships",
    //    Title = "Plate Ownerships",
    //    Icon = "fas fa-user-tag",
    //    Url = "/LicensePlateOwnerships",
    //    ParentId = 10,
    //    SortOrder = 8,
    //    IsActive = true
    //},

    // =====================================================
    // Rental Management
    // =====================================================
    new MenuItem
    {
        Id = 20,
        Name = "RentalManagement",
        Title = "Rental Management",
        Icon = "fas fa-file-contract",
        Url = "#",
        ParentId = null,
        SortOrder = 3,
        IsActive = true
    },
    new MenuItem
    {
        Id = 21,
        Name = "RentalContracts",
        Title = "Rental Contracts",
        Icon = "fas fa-file-signature",
        Url = "/RentalContracts",
        ParentId = 20,
        SortOrder = 1,
        IsActive = true
    },
    new MenuItem
    {
        Id = 22,
        Name = "Reservations",
        Title = "Reservations",
        Icon = "fas fa-calendar-check",
        Url = "/Reservations",
        ParentId = 20,
        SortOrder = 2,
        IsActive = true
    },
    new MenuItem
    {
        Id = 23,
        Name = "Returns",
        Title = "Vehicle Returns",
        Icon = "fas fa-undo",
        Url = "/Returns",
        ParentId = 20,
        SortOrder = 3,
        IsActive = true
    },
    new MenuItem
    {
        Id = 24,
        Name = "RentalPayments",
        Title = "Rental Payments",
        Icon = "fas fa-money-bill-wave",
        Url = "/RentalPayments",
        ParentId = 20,
        SortOrder = 4,
        IsActive = true
    },

    // =====================================================
    // Customers
    // =====================================================
    new MenuItem
    {
        Id = 30,
        Name = "Customers",
        Title = "Customers",
        Icon = "fas fa-users",
        Url = "#",
        ParentId = null,
        SortOrder = 4,
        IsActive = true
    },
    new MenuItem
    {
        Id = 31,
        Name = "CustomerList",
        Title = "Customers",
        Icon = "fas fa-user",
        Url = "/Customers",
        ParentId = 30,
        SortOrder = 1,
        IsActive = true
    },
    new MenuItem
    {
        Id = 32,
        Name = "CustomerDocuments",
        Title = "Customer Documents",
        Icon = "fas fa-id-card",
        Url = "/CustomerDocuments",
        ParentId = 30,
        SortOrder = 2,
        IsActive = true
    },

    // =====================================================
    // Insurance
    // =====================================================
    new MenuItem
    {
        Id = 40,
        Name = "Insurance",
        Title = "Insurance",
        Icon = "fas fa-shield-alt",
        Url = "#",
        ParentId = null,
        SortOrder = 5,
        IsActive = true
    },
    new MenuItem
    {
        Id = 41,
        Name = "InsurancePolicies",
        Title = "Insurance Policies",
        Icon = "fas fa-file-alt",
        Url = "/Insurance",
        ParentId = 40,
        SortOrder = 1,
        IsActive = true
    },
    new MenuItem
    {
        Id = 42,
        Name = "InsuranceCompanies",
        Title = "Insurance Companies",
        Icon = "fas fa-building",
        Url = "/InsuranceCompanies",
        ParentId = 40,
        SortOrder = 2,
        IsActive = true
    },
    new MenuItem
    {
        Id = 43,
        Name = "InsuranceTypes",
        Title = "Insurance Types",
        Icon = "fas fa-shield-alt",
        Url = "/InsuranceTypes",
        ParentId = 40,
        SortOrder = 3,
        IsActive = true
    },
    new MenuItem
    {
        Id = 44,
        Name = "InsuranceStatuses",
        Title = "Insurance Statuses",
        Icon = "fas fa-check-circle",
        Url = "/InsuranceStatuses",
        ParentId = 40,
        SortOrder = 4,
        IsActive = true
    },

    // =====================================================
    // Maintenance
    // =====================================================
    new MenuItem
    {
        Id = 50,
        Name = "Maintenance",
        Title = "Maintenance",
        Icon = "fas fa-tools",
        Url = "#",
        ParentId = null,
        SortOrder = 6,
        IsActive = true
    },
    new MenuItem
    {
        Id = 51,
        Name = "MaintenanceRecords",
        Title = "Maintenance Records",
        Icon = "fas fa-wrench",
        Url = "/Maintenance",
        ParentId = 50,
        SortOrder = 1,
        IsActive = true
    },
    new MenuItem
    {
        Id = 52,
        Name = "OilChangeSchedule",
        Title = "Oil Change Schedule",
        Icon = "fas fa-oil-can",
        Url = "/OilChangeSchedule",
        ParentId = 50,
        SortOrder = 2,
        IsActive = true
    },
    new MenuItem
    {
        Id = 53,
        Name = "TireSchedule",
        Title = "Tire Schedule",
        Icon = "fas fa-circle",
        Url = "/TireSchedule",
        ParentId = 50,
        SortOrder = 3,
        IsActive = true
    },
    new MenuItem
    {
        Id = 54,
        Name = "BatterySchedule",
        Title = "Battery Schedule",
        Icon = "fas fa-car-battery",
        Url = "/BatterySchedule",
        ParentId = 50,
        SortOrder = 4,
        IsActive = true
    },
    new MenuItem
    {
        Id = 55,
        Name = "Repairs",
        Title = "Repairs",
        Icon = "fas fa-tools",
        Url = "/Repairs",
        ParentId = 50,
        SortOrder = 5,
        IsActive = true
    },

    // =====================================================
    // Documents
    // =====================================================
    new MenuItem
    {
        Id = 60,
        Name = "Documents",
        Title = "Documents",
        Icon = "fas fa-folder",
        Url = "#",
        ParentId = null,
        SortOrder = 7,
        IsActive = true
    },
    new MenuItem
    {
        Id = 61,
        Name = "CarDocuments",
        Title = "Car Documents",
        Icon = "fas fa-file",
        Url = "/CarDocuments",
        ParentId = 60,
        SortOrder = 1,
        IsActive = true
    },
    new MenuItem
    {
        Id = 62,
        Name = "DocumentTypes",
        Title = "Document Types",
        Icon = "fas fa-file-alt",
        Url = "/DocumentTypes",
        ParentId = 60,
        SortOrder = 2,
        IsActive = true
    },

    // =====================================================
    // Accidents & Fines
    // =====================================================
    new MenuItem
    {
        Id = 70,
        Name = "AccidentsAndFines",
        Title = "Accidents & Fines",
        Icon = "fas fa-exclamation-triangle",
        Url = "#",
        ParentId = null,
        SortOrder = 8,
        IsActive = true
    },
    new MenuItem
    {
        Id = 71,
        Name = "Accidents",
        Title = "Accidents",
        Icon = "fas fa-car-crash",
        Url = "/Accidents",
        ParentId = 70,
        SortOrder = 1,
        IsActive = true
    },
    new MenuItem
    {
        Id = 72,
        Name = "Fines",
        Title = "Fines",
        Icon = "fas fa-ticket-alt",
        Url = "/Fines",
        ParentId = 70,
        SortOrder = 2,
        IsActive = true
    },

    // =====================================================
    // Payments & Finance
    // =====================================================
    new MenuItem
    {
        Id = 80,
        Name = "Finance",
        Title = "Finance",
        Icon = "fas fa-wallet",
        Url = "#",
        ParentId = null,
        SortOrder = 9,
        IsActive = true
    },
    new MenuItem
    {
        Id = 81,
        Name = "Payments",
        Title = "Payments",
        Icon = "fas fa-money-check-alt",
        Url = "/Payments",
        ParentId = 80,
        SortOrder = 1,
        IsActive = true
    },
    new MenuItem
    {
        Id = 82,
        Name = "PaymentMethods",
        Title = "Payment Methods",
        Icon = "fas fa-credit-card",
        Url = "/PaymentMethods",
        ParentId = 80,
        SortOrder = 2,
        IsActive = true
    },
    new MenuItem
    {
        Id = 83,
        Name = "Expenses",
        Title = "Expenses",
        Icon = "fas fa-receipt",
        Url = "/Expenses",
        ParentId = 80,
        SortOrder = 3,
        IsActive = true
    },

    // =====================================================
    // Reports
    // =====================================================
    new MenuItem
    {
        Id = 90,
        Name = "Reports",
        Title = "Reports",
        Icon = "fas fa-chart-bar",
        Url = "#",
        ParentId = null,
        SortOrder = 10,
        IsActive = true
    },
    new MenuItem
    {
        Id = 91,
        Name = "RentalReport",
        Title = "Rental Report",
        Icon = "fas fa-file-invoice",
        Url = "/Reports/Rental",
        ParentId = 90,
        SortOrder = 1,
        IsActive = true
    },
    new MenuItem
    {
        Id = 92,
        Name = "MaintenanceReport",
        Title = "Maintenance Report",
        Icon = "fas fa-tools",
        Url = "/Reports/Maintenance",
        ParentId = 90,
        SortOrder = 2,
        IsActive = true
    },
    new MenuItem
    {
        Id = 93,
        Name = "InsuranceReport",
        Title = "Insurance Report",
        Icon = "fas fa-shield-alt",
        Url = "/Reports/Insurance",
        ParentId = 90,
        SortOrder = 3,
        IsActive = true
    },
    new MenuItem
    {
        Id = 94,
        Name = "FinancialReport",
        Title = "Financial Report",
        Icon = "fas fa-chart-line",
        Url = "/Reports/Financial",
        ParentId = 90,
        SortOrder = 4,
        IsActive = true
    },
    new MenuItem
    {
        Id = 95,
        Name = "FleetReport",
        Title = "Fleet Report",
        Icon = "fas fa-car",
        Url = "/Reports/Fleet",
        ParentId = 90,
        SortOrder = 5,
        IsActive = true
    },

    // =====================================================
    // Administration
    // =====================================================
    new MenuItem
    {
        Id = 100,
        Name = "Administration",
        Title = "Administration",
        Icon = "fas fa-cogs",
        Url = "#",
        ParentId = null,
        SortOrder = 11,
        IsActive = true
    },
    new MenuItem
    {
        Id = 101,
        Name = "Users",
        Title = "Users",
        Icon = "fas fa-users-cog",
        Url = "/Admin/Users",
        ParentId = 100,
        SortOrder = 1,
        IsActive = true
    },
    new MenuItem
    {
        Id = 102,
        Name = "Roles",
        Title = "Roles",
        Icon = "fas fa-user-shield",
        Url = "/Admin/Roles",
        ParentId = 100,
        SortOrder = 2,
        IsActive = true
    },
    new MenuItem
    {
        Id = 103,
        Name = "UserMenuPermissions",
        Title = "User Menu Permissions",
        Icon = "fas fa-key",
        Url = "/Admin/UserMenuPermissions",
        ParentId = 100,
        SortOrder = 3,
        IsActive = true
    },
    new MenuItem
    {
        Id = 104,
        Name = "MenuItems",
        Title = "Menu Items",
        Icon = "fas fa-list",
        Url = "/Admin/MenuItems",
        ParentId = 100,
        SortOrder = 4,
        IsActive = true
    },
    new MenuItem
    {
        Id = 105,
        Name = "Announcements",
        Title = "Announcements",
        Icon = "fas fa-bullhorn",
        Url = "/Admin/Announcements",
        ParentId = 100,
        SortOrder = 5,
        IsActive = true
    },
    new MenuItem
    {
        Id = 106,
        Name = "SystemSettings",
        Title = "System Settings",
        Icon = "fas fa-cog",
        Url = "/Admin/Settings",
        ParentId = 100,
        SortOrder = 6,
        IsActive = true
    },
    new MenuItem { Id = 107, Name = "PlateTypes", Title = "Plate Types", Icon = "fas fa-info-circle", Url = "/PlateTypes", ParentId = 10, SortOrder = 9, IsActive = true },
    new MenuItem { Id = 108, Name = "PlateRegions", Title = "Plate Regions", Icon = "fas fa-map-marker-alt", Url = "/PlateRegions", ParentId = 10, SortOrder = 10, IsActive = true },
    new MenuItem { Id = 109, Name = "PlateOwners", Title = "Plate Owners", Icon = "fas fa-user-tie", Url = "/PlateOwners", ParentId = 10, SortOrder = 11, IsActive = true },
    new MenuItem { Id = 110, Name = "SAccountTypes", Title = "Account Types", Icon = "fas fa-list-alt", Url = "/SAccountTypes", ParentId = 80, SortOrder = 4, IsActive = true },
    new MenuItem { Id = 111, Name = "STransactionTypes", Title = "Transaction Types", Icon = "fas fa-exchange-alt", Url = "/STransactionTypes", ParentId = 80, SortOrder = 5, IsActive = true },
    new MenuItem { Id = 112, Name = "Statuses", Title = "Statuses", Icon = "fas fa-check-circle", Url = "/Statuses", ParentId = 100, SortOrder = 7, IsActive = true }
}));




            builder.Entity<SAccountCategory>().HasData(PinCreatedAt(
    new SAccountCategory
    {
        Id = 1,
        Code = "ASSET",
        Name = "ASSET",
        Name_ar = "الأصول"
    },
    new SAccountCategory
    {
        Id = 2,
        Code = "LIABILITY",
        Name = "LIABILITY",
        Name_ar = "الخصوم"
    },
    new SAccountCategory
    {
        Id = 3,
        Code = "REVENUE",
        Name = "REVENUE",
        Name_ar = "الإيرادات"
    },
    new SAccountCategory
    {
        Id = 4,
        Code = "EXPENSE",
        Name = "EXPENSE",
        Name_ar = "المصروفات"
    },
    new SAccountCategory
    {
        Id = 5,
        Code = "EQUITY",
        Name = "EQUITY",
        Name_ar = "حقوق الملكية"
    }
));

            builder.Entity<SAccountType>().HasData(PinCreatedAt(

    // ============================
    // الأصول
    // ============================

    new SAccountType
    {
        Id = 1,
        AccountCategoryId = 1,
        Code = "CASH",
        Name = "Cash",
        Name_ar = "الصندوق",
        IsActive = true
    },

    new SAccountType
    {
        Id = 2,
        AccountCategoryId = 1,
        Code = "BANK",
        Name = "Bank",
        Name_ar = "البنك",
        IsActive = true
    },

    new SAccountType
    {
        Id = 3,
        AccountCategoryId = 1,
        Code = "CUSTOMER",
        Name = "Customer Accounts",
        Name_ar = "حسابات العملاء",
        IsActive = true
    },

    new SAccountType
    {
        Id = 4,
        AccountCategoryId = 1,
        Code = "VEHICLE",
        Name = "Vehicles",
        Name_ar = "السيارات",
        IsActive = true
    },

    // ============================
    // الخصوم
    // ============================

    new SAccountType
    {
        Id = 5,
        AccountCategoryId = 2,
        Code = "INVESTOR_PAYABLE",
        Name = "Investor Payables",
        Name_ar = "مستحقات المستثمرين",
        IsActive = true
    },

    new SAccountType
    {
        Id = 6,
        AccountCategoryId = 2,
        Code = "OTHER_LIABILITY",
        Name = "Other Liabilities",
        Name_ar = "التزامات أخرى",
        IsActive = true
    },

    // ============================
    // الإيرادات
    // ============================

    new SAccountType
    {
        Id = 7,
        AccountCategoryId = 3,
        Code = "RENTAL_REVENUE",
        Name = "Car Rental Revenue",
        Name_ar = "إيرادات تأجير السيارات",
        IsActive = true
    },

    new SAccountType
    {
        Id = 8,
        AccountCategoryId = 3,
        Code = "FINE_REVENUE",
        Name = "Fine Revenue",
        Name_ar = "إيرادات الغرامات",
        IsActive = true
    },

    new SAccountType
    {
        Id = 9,
        AccountCategoryId = 3,
        Code = "OTHER_REVENUE",
        Name = "Other Revenue",
        Name_ar = "إيرادات أخرى",
        IsActive = true
    },

    // ============================
    // المصروفات
    // ============================

    new SAccountType
    {
        Id = 10,
        AccountCategoryId = 4,
        Code = "MAINTENANCE",
        Name = "Vehicle Maintenance",
        Name_ar = "صيانة السيارات",
        IsActive = true
    },

    new SAccountType
    {
        Id = 11,
        AccountCategoryId = 4,
        Code = "FUEL",
        Name = "Fuel",
        Name_ar = "الوقود",
        IsActive = true
    },

    new SAccountType
    {
        Id = 12,
        AccountCategoryId = 4,
        Code = "INSURANCE",
        Name = "Insurance",
        Name_ar = "التأمين",
        IsActive = true
    },

    new SAccountType
    {
        Id = 13,
        AccountCategoryId = 4,
        Code = "TIRE",
        Name = "Tires",
        Name_ar = "الإطارات",
        IsActive = true
    },

    new SAccountType
    {
        Id = 14,
        AccountCategoryId = 4,
        Code = "BATTERY",
        Name = "Batteries",
        Name_ar = "البطاريات",
        IsActive = true
    },

    new SAccountType
    {
        Id = 15,
        AccountCategoryId = 4,
        Code = "OTHER_EXPENSE",
        Name = "Other Expenses",
        Name_ar = "مصاريف أخرى",
        IsActive = true
    },

    // ============================
    // حقوق الملكية
    // ============================

    new SAccountType
    {
        Id = 16,
        AccountCategoryId = 5,
        Code = "INVESTOR_CAPITAL",
        Name = "Investor Capital",
        Name_ar = "رأس مال المستثمرين",
        IsActive = true
    }
));

            builder.Entity<SAccount>().HasData(PinCreatedAt(

    // =====================================
    // الصندوق
    // =====================================

    new SAccount
    {
        AccountId = 1,
        AccountTypeId = 1,
        OwnerType = AccountOwnerTypes.Cashbox,
        OwnerId = null,
        Code = "CASHBOX-MAIN",
        Name = "الصندوق الرئيسي",
        Currency = "USD",
        IsActive = true
    },

    // =====================================
    // البنك
    // =====================================

    new SAccount
    {
        AccountId = 2,
        AccountTypeId = 2,
        OwnerType = AccountOwnerTypes.Bank,
        OwnerId = null,
        Code = "BANK-MAIN",
        Name = "الحساب البنكي الرئيسي",
        Name_ar = "الحساب البنكي الرئيسي",
        Currency = "USD",
        IsActive = true
    },

    // =====================================
    // العملاء
    // =====================================

    new SAccount
    {
        AccountId = 3,
        AccountTypeId = 3,
        OwnerType = AccountOwnerTypes.Customer,
        OwnerId = 1,
        Code = "CUSTOMER-001",
        Name = "حساب عميل تجريبي",
        Currency = "USD",
        IsActive = true
    },

    // =====================================
    // السيارات
    // =====================================

    new SAccount
    {
        AccountId = 4,
        AccountTypeId = 4,
        OwnerType = AccountOwnerTypes.Vehicle,
        OwnerId = 1,
        Code = "VEHICLE-001",
        Name = "حساب سيارة تجريبية",
        Currency = "USD",
        IsActive = true
    },

    // =====================================
    // مستحقات المستثمرين
    // =====================================

    new SAccount
    {
        AccountId = 5,
        AccountTypeId = 5,
        OwnerType = AccountOwnerTypes.Investor,
        OwnerId = 1,
        Code = "INVESTOR-PAYABLE-001",
        Name = "مستحقات المستثمر - تجريبي",
        Currency = "USD",
        IsActive = true
    },

    // =====================================
    // إيرادات تأجير السيارات
    // =====================================

    new SAccount
    {
        AccountId = 6,
        AccountTypeId = 7,
        OwnerType = AccountOwnerTypes.Company,
        OwnerId = null,
        Code = "REVENUE-RENTAL",
        Name = "إيرادات تأجير السيارات",
        Currency = "USD",
        IsActive = true
    },

    // =====================================
    // إيرادات الغرامات
    // =====================================

    new SAccount
    {
        AccountId = 7,
        AccountTypeId = 8,
        OwnerType = AccountOwnerTypes.Company,
        OwnerId = null,
        Code = "REVENUE-FINES",
        Name = "إيرادات الغرامات",
        Currency = "USD",
        IsActive = true
    },

    // =====================================
    // صيانة السيارات
    // =====================================

    new SAccount
    {
        AccountId = 8,
        AccountTypeId = 10,
        OwnerType = AccountOwnerTypes.Company,
        OwnerId = null,
        Code = "EXPENSE-MAINTENANCE",
        Name = "مصروف صيانة السيارات",
        Currency = "USD",
        IsActive = true
    },

    // =====================================
    // الوقود
    // =====================================

    new SAccount
    {
        AccountId = 9,
        AccountTypeId = 11,
        OwnerType = AccountOwnerTypes.Company,
        OwnerId = null,
        Code = "EXPENSE-FUEL",
        Name = "مصروف الوقود",
        Currency = "USD",
        IsActive = true
    },

    // =====================================
    // التأمين
    // =====================================

    new SAccount
    {
        AccountId = 10,
        AccountTypeId = 12,
        OwnerType = AccountOwnerTypes.Company,
        OwnerId = null,
        Code = "EXPENSE-INSURANCE",
        Name = "مصروف التأمين",
        Currency = "USD",
        IsActive = true
    },

    // =====================================
    // الإطارات
    // =====================================

    new SAccount
    {
        AccountId = 11,
        AccountTypeId = 13,
        OwnerType = AccountOwnerTypes.Company,
        OwnerId = null,
        Code = "EXPENSE-TIRES",
        Name = "مصروف الإطارات",
        Currency = "USD",
        IsActive = true
    },

    // =====================================
    // البطاريات
    // =====================================

    new SAccount
    {
        AccountId = 12,
        AccountTypeId = 14,
        OwnerType = AccountOwnerTypes.Company,
        OwnerId = null,
        Code = "EXPENSE-BATTERY",
        Name = "مصروف البطاريات",
        Currency = "USD",
        IsActive = true
    },

    // =====================================
    // رأس مال المستثمرين
    // =====================================

    new SAccount
    {
        AccountId = 13,
        AccountTypeId = 16,
        OwnerType = AccountOwnerTypes.Investor,
        OwnerId = 1,
        Code = "EQUITY-INVESTOR-001",
        Name = "رأس مال المستثمر - تجريبي",
        Currency = "USD",
        IsActive = true
    }
));
        }
        //Add-Migration Intial_Migration_2022_06_01
        //Update-database
        #region Application & System

        public virtual DbSet<AppLabel> AppLabels { get; set; }
        public virtual DbSet<RepairType> RepairType { get; set; }
        public virtual DbSet<RepairCategory> RepairCategory { get; set; }
        public virtual DbSet<AppSettings> AppSettings { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<LookUpMultiLang> LookUpMultiLang { get; set; }
        public virtual DbSet<LookUps> LookUps { get; set; }
        public virtual DbSet<LookUpTable> LookUpTables { get; set; }
        public virtual DbSet<Media> Media { get; set; }

        #endregion


        #region Location & General Information

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<PlateType> PlateTypes { get; set; }
        public virtual DbSet<PlateRegion> PlateRegion { get; set; }


        #endregion


        #region Users & Communication

        public virtual DbSet<EUser> EUsers { get; set; }
        public virtual DbSet<Contactus> Contactus { get; set; }
        public virtual DbSet<MessageTemplate> MessageTemplates { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Announcements> Announcements { get; set; }

        #endregion


        #region Authentication & Security

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<ResetPassword> ResetPasswords { get; set; }

        #endregion


        #region Logging

        public virtual DbSet<LoggerAction> LoggerActions { get; set; }
        public virtual DbSet<LoggerError> LoggerErrors { get; set; }

        #endregion


        #region Vehicle

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<FuelType> FuelTypes { get; set; }
        public virtual DbSet<CarDocuments> CarDocuments { get; set; }
        public virtual DbSet<CarStatus> CarStatus { get; set; }

        #endregion


        #region Car Ownership & License Plates

        public virtual DbSet<CarOwner> CarOwners { get; set; }
        public virtual DbSet<LicensePlate> LicensePlates { get; set; }
        public virtual DbSet<LicensePlateOwnership> LicensePlateOwnerships { get; set; }
        public virtual DbSet<PlateOwner> PlateOwners { get; set; }
        public virtual DbSet<Investor> Investors { get; set; }

        #endregion


        #region Customers & Rental

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<RentalContract> RentalContracts { get; set; }
        public virtual DbSet<RentalPayment> RentalPayments { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }

        #endregion


        #region Maintenance & Repairs

        public virtual DbSet<WorkOrder> WorkOrders { get; set; }
        public virtual DbSet<WorkOrderDetail> WorkOrderDetails { get; set; }
        public virtual DbSet<Repair> Repairs { get; set; }
        public virtual DbSet<SparePart> SpareParts { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        #endregion


        #region Maintenance Schedules

        public virtual DbSet<OilChangeSchedule> OilChangeSchedules { get; set; }
        public virtual DbSet<TireSchedule> TireSchedules { get; set; }
        public virtual DbSet<BatterySchedule> BatterySchedules { get; set; }

        #endregion


        #region Insurance & Inspection

        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public virtual DbSet<InsuranceDocument> InsuranceDocuments { get; set; }
        public virtual DbSet<InsuranceType> InsuranceTypes { get; set; }
        public virtual DbSet<Inspection> Inspections { get; set; }

        #endregion


        #region Accidents & Violations

        public virtual DbSet<Accident> Accidents { get; set; }
        public virtual DbSet<Violation> Violations { get; set; }

        #endregion


        #region Documents

        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<STransactionDocuments> STransactionDocuments { get; set; }

        #endregion


        #region Menu & Permissions
        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<UserMenuPermission> UserMenuPermissions { get; set; }

        #endregion


        #region Accounting & Finance

        public virtual DbSet<SAccount> SAccounts { get; set; }
        public virtual DbSet<SAccountType> SAccountTypes { get; set; }
        public virtual DbSet<STransaction> STransactions { get; set; }
        public virtual DbSet<STransactionType> STransactionType { get; set; }

        #endregion





    }
}
