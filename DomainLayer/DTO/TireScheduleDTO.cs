using System;

namespace RentalCar.DomainLayer.DTO
{
    public class TireScheduleDTO : BaseDTO
    {
        public long Id { get; set; }
        public DateTime InstallDate { get; set; }
        public int InstallKM { get; set; }
        public int ExpectedKM { get; set; } = 40000;
        public string Brand { get; set; }
        public int Quantity { get; set; } = 4;
        public decimal Cost { get; set; }
        public string Notes { get; set; }

        // Computed (read-only)
        public int NextChangeKM => InstallKM + ExpectedKM;

        // Foreign keys
        public long? CarId { get; set; }
    }
}
