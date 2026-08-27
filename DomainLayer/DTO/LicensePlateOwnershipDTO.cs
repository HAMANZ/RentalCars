using System;

namespace RentalCar.DomainLayer.DTO
{
    public class LicensePlateOwnershipDTO : BaseDTO
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public string Notes { get; set; }

        // Foreign keys
        public long? LicensePlateId { get; set; }
        public string PlateOwnerId { get; set; }
    }
}
