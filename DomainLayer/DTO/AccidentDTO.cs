using System;

namespace RentalCar.DomainLayer.DTO
{
    public class AccidentDTO : BaseDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        // Foreign keys
        public long? CarId { get; set; }
    }
}
