using System;

namespace RentalCar.DomainLayer.DTO
{
    public class InspectionDTO : BaseDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpiryDate { get; set; }

        // Foreign keys
        public long? CarId { get; set; }
    }
}
