using System;

namespace RentalCar.DomainLayer.DTO
{
    public class OilChangeScheduleDTO : BaseDTO
    {
        public long Id { get; set; }
        public DateTime LastChangeDate { get; set; }
        public int LastChangeKM { get; set; }
        public int ChangeIntervalKM { get; set; } = 5000;
        public string OilType { get; set; }
        public decimal Cost { get; set; }
        public string Notes { get; set; }

        // Computed (read-only)
        public int NextChangeKM => LastChangeKM + ChangeIntervalKM;

        // Foreign keys
        public long? CarId { get; set; }
    }
}
