using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RentalCar.DomainLayer.Model;
using System;
using System.ComponentModel;
using System.Reflection.Emit;

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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<EUser>().ToTable("EUser");
            builder.HasDefaultSchema("dbo");
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


            builder.Entity<IdentityRole>().HasData(new IdentityRole[] {
                  new IdentityRole{Name="EUser",NormalizedName="EUSER"},
                  new IdentityRole{Name="Adminstrator",NormalizedName="ADMINSTRATOR"},
                  new IdentityRole{Name="Tutor",NormalizedName="TUTOR"},
                  new IdentityRole{Name="Student",NormalizedName="STUDENT"},
              });
         

            builder.Entity<EUser>().HasData(new EUser[] {
                  new EUser{UserName="admin",NormalizedUserName="ADMIN",GenderId=1, Email="hudaabumayha.ham@gmail.com",Created_at=DateTime.Now,Created_by=1,Updated_at=DateTime.Now,Updated_by=1,Is_deleted=false},
              });

            builder.Entity<AppSettings>().HasData(new AppSettings[] {
                  new AppSettings{Id=1,Logo="RentalCar.jpg",ApplicationName="RentalCar",Description="",ContactWebsite="",LicenseDetail="",Phone="09999999", Email="oonlinetutoring@gmail.com",Password="P@ssw0rdsse",Facebook="",Twitter="",Created_at=DateTime.Now,Created_by=1,Updated_at=DateTime.Now,Updated_by=1,Is_deleted=false},
              });
            builder.Entity<Language>().HasData(new Language[] {
                  new Language{Id=1,Name="Arabic",LanguageCode="ar",Created_at=DateTime.Now,Created_by=1,Updated_at=DateTime.Now,Updated_by=1,Is_deleted=false},
                  new Language{Id = 2, Name="English",LanguageCode="en",Created_at=DateTime.Now,Created_by=1,Updated_at=DateTime.Now,Updated_by=1,Is_deleted=false},
              });
            builder.Entity<Gender>().HasData(new Gender[] {
                  new Gender{Id=1,Name="Male",Code="M",Created_at=DateTime.Now,Created_by=1,Updated_at=DateTime.Now,Updated_by=1,Is_deleted=false},
                  new Gender{Id = 2, Name="Female",Code="F",Created_at=DateTime.Now,Created_by=1,Updated_at=DateTime.Now,Updated_by=1,Is_deleted=false},
              });


            builder.Entity<City>().HasData(new City[] {
                  new City{Id= 1, Name="AL-Kouds",CountryId=1,Created_at=DateTime.Now,Created_by=1,Updated_at=DateTime.Now,Updated_by=1,Is_deleted=false},
                  new City{Id = 2, Name="Beirut",CountryId=2,Created_at=DateTime.Now,Created_by=1,Updated_at=DateTime.Now,Updated_by=1,Is_deleted=false},
                  new City{Id = 3, Name="Istanbul",CountryId=3,Created_at=DateTime.Now,Created_by=1,Updated_at=DateTime.Now,Updated_by=1,Is_deleted=false},
              });
            builder.Entity<Country>().HasData(new Country[] {
                  new Country{Id = 1, Name="Palestinne",Created_at=DateTime.Now,Created_by=1,Updated_at=DateTime.Now,Updated_by=1,Is_deleted=false},
                  new Country{Id = 2, Name="Lebanon",Created_at=DateTime.Now,Created_by=1,Updated_at=DateTime.Now,Updated_by=1,Is_deleted=false},
                  new Country{Id = 3, Name="Turkey",Created_at=DateTime.Now,Created_by=1,Updated_at=DateTime.Now,Updated_by=1,Is_deleted=false},
              });


           

        }
        //Add-Migration Intial_Migration_2022_06_01
        //Update-database

        public virtual DbSet<AppLabel> AppLabel { get; set; }
        public virtual DbSet<AppSettings> AppSettings { get; set; }
        public virtual DbSet<Announcements> Announcements { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Nationality> Nationality { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Contactus> Contactus { get; set; }
        public virtual DbSet<MessageTemplate> MessageTemplate { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<LoggerAction> LoggerAction { get; set; }
        public virtual DbSet<LoggerError> LoggerError { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ResetPassword> ResetPasswords { get; set; }
        public virtual DbSet<EUser> EUser { get; set; }

        public virtual DbSet<LookUpMultiLang> LookUpMultiLang { get; set; }
        public virtual DbSet<LookUps> LookUps { get; set; }
        public virtual DbSet<LookUpTable> LookUpTable { get; set; }
        public DbSet<Media> Media { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<RentalContract> RentalContracts { get; set; }

        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderDetail> WorkOrderDetails { get; set; }

        public DbSet<Repair> Repairs { get; set; }

        public DbSet<SparePart> SpareParts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public DbSet<Inspection> Inspection { get; set; }

        public DbSet<Accident> Accidents { get; set; }

        public DbSet<Violation> Violations { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Payment> Payment { get; set; }

        public DbSet<OilChangeSchedule> OilChangeSchedules { get; set; }

        public DbSet<TireSchedule> TireSchedules { get; set; }

        public DbSet<BatterySchedule> BatterySchedules { get; set; }

        public DbSet<CarOwner> CarOwners { get; set; }
        public DbSet<LicensePlate> LicensePlate { get; set; }
        public DbSet<LicensePlateOwnership> LicensePlateOwnership { get; set; }
        public DbSet<PlateOwner> PlateOwner { get; set; }
        public DbSet<Brand> Brand { get; set; }

    }
}
