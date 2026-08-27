using System;

namespace RentalCar.DomainLayer.DTO
{
    public class CarDTO : BaseDTO
    {
        public long Id { get; set; }
        public string VIN { get; set; }
        public string EngineNo { get; set; }
        public string Model { get; set; }
        public string ChassisNumber { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public double PurchasePrice { get; set; }
        public int CurrentKM { get; set; }
        public string Description { get; set; }

        // Foreign keys
        public string InvestorId { get; set; }
        public long? BranchId { get; set; }
        public long? FuelTypeId { get; set; }
        public long? LicensePlateId { get; set; }
        public string CarOwnerId { get; set; }
        public long? BrandId { get; set; }
        public long? CarStatusId { get; set; }
    }
}
