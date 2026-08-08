using System;

namespace RentalCar.DomainLayer.DTO
{
    public class LicensePlateDTO : BaseDTO
    {
        public long Id { get; set; }
        public string PlateNumber { get; set; }
        public string PlateType { get; set; }
        public bool IsActive { get; set; }
    }
}
