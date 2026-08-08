using System;

namespace RentalCar.DomainLayer.DTO
{
    public class WorkOrderDTO : BaseDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CurrentKM { get; set; }

        // Foreign keys
        public long? CarId { get; set; }
        public long? StatusId { get; set; }
    }
}
