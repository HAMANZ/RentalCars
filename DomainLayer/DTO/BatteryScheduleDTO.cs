using System;

namespace RentalCar.DomainLayer.DTO
{
    public class BatteryScheduleDTO : BaseDTO
    {
        public long Id { get; set; }
        public DateTime InstallDate { get; set; }
        public int LifeMonths { get; set; } = 24;
        public string Brand { get; set; }
        public decimal Cost { get; set; }
        public string Warranty { get; set; }
        public string Notes { get; set; }

        // Computed (read-only)
        public DateTime ExpiryDate => InstallDate.AddMonths(LifeMonths);

        // Foreign keys
        public long? CarId { get; set; }
    }
}
