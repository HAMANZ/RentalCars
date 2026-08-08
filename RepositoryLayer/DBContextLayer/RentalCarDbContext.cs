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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<EUser>().ToTable("EUser");
            builder.HasDefaultSchema("dbo");
            builder.Entity<STransaction>()
      .HasOne(t => t.DebitAccount)
      .WithMany(a => a.DebitTransactions)
      .HasForeignKey(t => t.DebitAccountId)
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
        #region Application & System

        public virtual DbSet<AppLabel> AppLabels { get; set; }
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

        #endregion


        #region Accounting & Finance

        public virtual DbSet<SAccount> SAccounts { get; set; }
        public virtual DbSet<SAccountType> SAccountTypes { get; set; }
        public virtual DbSet<STransaction> STransactions { get; set; }
        public virtual DbSet<STransactionType> STransactionType { get; set; }

        #endregion





    }
}
