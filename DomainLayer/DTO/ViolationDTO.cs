using System;

namespace RentalCar.DomainLayer.DTO
{
    public class ViolationDTO : BaseDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }

        // Foreign keys
        public long? CarId { get; set; }
    }
}
