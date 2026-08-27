using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RentalCar.DomainLayer.Models;

namespace RentalCar
{
    /// <summary>
    /// Seeds sample Cars and all their related detail records (oil/tire/battery
    /// schedules, work orders, insurance, documents, rental contracts) so the
    /// Cars list and Car Details page have data to display. Idempotent: it does
    /// nothing once any non-deleted car exists. Lookups (Brand, FuelType,
    /// CarStatus, Branch, Status, InsuranceCompany, InsuranceType, DocumentType)
    /// are already seeded via HasData, so they are only referenced here.
    /// </summary>
    public static class CarDataSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<RentalCarDbContext>();

            // Idempotent guard.
            if (await db.Cars.AnyAsync(c => !c.Is_deleted))
                return;

            // Existing lookups. The DbContext defaults to NoTracking, so we must
            // AsTracking() here — otherwise assigning them as navigations to a new
            // Car makes EF try to INSERT them again (identity-insert failure).
            var brand = await db.Brands.AsTracking().FirstOrDefaultAsync(b => !b.Is_deleted);
            var fuel = await db.FuelTypes.AsTracking().FirstOrDefaultAsync(f => !f.Is_deleted);
            var carStatus = await db.CarStatus.AsTracking().FirstOrDefaultAsync(s => !s.Is_deleted);
            var branch = await db.Branches.AsTracking().FirstOrDefaultAsync(br => !br.Is_deleted);
            var status = await db.Statuses.AsTracking().FirstOrDefaultAsync(s => !s.Is_deleted);
            var insCompany = await db.InsuranceCompanies.AsTracking().FirstOrDefaultAsync(i => !i.Is_deleted);
            var insType = await db.InsuranceTypes.AsTracking().FirstOrDefaultAsync(i => !i.Is_deleted);
            var docType = await db.DocumentTypes.AsTracking().FirstOrDefaultAsync(d => !d.Is_deleted);

            // Core lookups must exist to build a meaningful car.
            if (brand == null || fuel == null || carStatus == null || branch == null)
            {
                Console.WriteLine("[CarDataSeeder] Missing core lookups (Brand/FuelType/CarStatus/Branch); skipping.");
                return;
            }

            var now = DateTime.UtcNow;

            // ---- Owner / Investor / Customer (not seeded via HasData) ----
            var owner = new CarOwner
            {
                FullName = "Ahmad Al-Sayed",
                CompanyName = "",
                Phone1 = "0500000001",
                Email = "owner1@rentalcar.com",
                Address = "Riyadh",
                IsCompany = false,
                IsActive = true,
                Notes = "Sample owner",
                Created_at = now, Updated_at = now, Created_by = 1, Updated_by = 1
            };
            db.CarOwners.Add(owner);

            var investor = new Investor
            {
                Phone = "0555000001",
                Email = "investor1@rentalcar.com",
                DrivingLicense = "INV-DL-001",
                Address = "Riyadh",
                LicenseExpiryDate = now.AddYears(3),
                Created_at = now, Updated_at = now, Created_by = 1, Updated_by = 1
            };
            db.Investors.Add(investor);

            var customer = new Customer
            {
                Phone = "0533000001",
                Email = "customer1@rentalcar.com",
                DrivingLicense = "CUS-DL-001",
                Address = "Jeddah",
                LicenseExpiryDate = now.AddYears(2),
                Created_at = now, Updated_at = now, Created_by = 1, Updated_by = 1
            };
            db.Customers.Add(customer);

            // ---- Sample cars ----
            var seedCars = new[]
            {
                new { Model = "Corolla", Year = 2021, Color = "White", Plate = "ABC-1234", KM = 45000, Price = 75000d, VIN = "VINCOROLLA2021001", Engine = "ENG-CR-001", Chassis = "CHS-CR-001" },
                new { Model = "Sunny",   Year = 2020, Color = "Silver", Plate = "DEF-5678", KM = 82000, Price = 62000d, VIN = "VINSUNNY2020002",   Engine = "ENG-SN-002", Chassis = "CHS-SN-002" },
                new { Model = "Accent",  Year = 2022, Color = "Black",  Plate = "GHI-9012", KM = 21000, Price = 81000d, VIN = "VINACCENT2022003",  Engine = "ENG-AC-003", Chassis = "CHS-AC-003" }
            };

            foreach (var c in seedCars)
            {
                var plate = new LicensePlate
                {
                    PlateNumber = c.Plate,
                    PlateTypeId = 1,
                    PlateRegionId = 2,
                    IsActive = true,
                    Created_at = now, Updated_at = now, Created_by = 1, Updated_by = 1
                };
                db.LicensePlates.Add(plate);

                var car = new Car
                {
                    VIN = c.VIN,
                    EngineNo = c.Engine,
                    Model = c.Model,
                    ChassisNumber = c.Chassis,
                    Year = c.Year,
                    Color = c.Color,
                    Image = "",
                    PurchasePrice = c.Price,
                    CurrentKM = c.KM,
                    Description = $"{brand.Name} {c.Model} {c.Year}",
                    Brand = brand,
                    FuelType = fuel,
                    CarStatus = carStatus,
                    Branch = branch,
                    LicensePlate = plate,
                    CarOwner = owner,
                    Investor = investor,
                    Created_at = now, Updated_at = now, Created_by = 1, Updated_by = 1
                };
                db.Cars.Add(car);

                // Oil schedule
                db.OilChangeSchedules.Add(new OilChangeSchedule
                {
                    Car = car,
                    LastChangeDate = now.AddMonths(-3),
                    LastChangeKM = c.KM - 4000,
                    ChangeIntervalKM = 5000,
                    OilType = "5W-30 Synthetic",
                    Cost = 250,
                    Notes = "Routine oil change",
                    Created_at = now, Updated_at = now, Created_by = 1, Updated_by = 1
                });

                // Tire schedule
                db.TireSchedules.Add(new TireSchedule
                {
                    Car = car,
                    InstallDate = now.AddMonths(-8),
                    InstallKM = c.KM - 15000,
                    ExpectedKM = 40000,
                    Brand = "Michelin",
                    Quantity = 4,
                    Cost = 1600m,
                    Notes = "All-season tires",
                    Created_at = now, Updated_at = now, Created_by = 1, Updated_by = 1
                });

                // Battery schedule
                db.BatterySchedules.Add(new BatterySchedule
                {
                    Car = car,
                    InstallDate = now.AddMonths(-10),
                    LifeMonths = 24,
                    Brand = "AC Delco",
                    Cost = 450m,
                    Warranty = "18 months",
                    Notes = "70Ah battery",
                    Created_at = now, Updated_at = now, Created_by = 1, Updated_by = 1
                });

                // Work order
                if (status != null)
                {
                    db.WorkOrders.Add(new WorkOrder
                    {
                        Car = car,
                        Date = now.AddMonths(-1),
                        CurrentKM = c.KM - 1000,
                        Status = status,
                        Created_at = now, Updated_at = now, Created_by = 1, Updated_by = 1
                    });
                }

                // Insurance
                if (insCompany != null && insType != null && status != null)
                {
                    db.Insurances.Add(new Insurance
                    {
                        Car = car,
                        PolicyNumber = $"POL-{c.Year}-{c.Plate}",
                        StartDate = now.AddMonths(-6),
                        EndDate = now.AddMonths(6),
                        Premium = 3200,
                        CoverageAmount = 100000,
                        Deductible = 1000,
                        CoverageDetails = "Comprehensive coverage",
                        Notes = "Annual policy",
                        RenewalReminderSent = false,
                        InsuranceCompany = insCompany,
                        InsuranceType = insType,
                        Status = status,
                        Created_at = now, Updated_at = now, Created_by = 1, Updated_by = 1
                    });
                }

                // Document
                if (docType != null)
                {
                    db.CarDocuments.Add(new CarDocuments
                    {
                        Car = car,
                        DocumentType = docType,
                        FilePath = $"/Images/Cars/Documents/{c.Plate}.pdf",
                        ExpiresAt = now.AddYears(1),
                        IsActive = true,
                        Created_at = now, Updated_at = now, Created_by = 1, Updated_by = 1
                    });
                }

                // Rental contract
                if (status != null)
                {
                    db.RentalContracts.Add(new RentalContract
                    {
                        Car = car,
                        Customer = customer,
                        Status = status,
                        StartDate = now.AddDays(-10),
                        EndDate = now.AddDays(-3),
                        ActualReturnDate = now.AddDays(-3),
                        OdometerStart = c.KM - 800,
                        OdometerEnd = c.KM,
                        DailyRate = 150,
                        Discount = 0,
                        TotalAmount = 1050,
                        PaidAmount = 1050,
                        Created_at = now, Updated_at = now, Created_by = 1, Updated_by = 1
                    });
                }
            }

            await db.SaveChangesAsync();
            Console.WriteLine("[CarDataSeeder] Seeded sample cars and related records.");
        }
    }
}
