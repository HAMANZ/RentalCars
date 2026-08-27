using System;

namespace RentalCar.DomainLayer.DTO
{
    public class LicensePlateDTO : BaseDTO
    {
        public long Id { get; set; }
        public string PlateNumber { get; set; }
        public long? PlateRegionId { get; set; }
        public long PlateTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
