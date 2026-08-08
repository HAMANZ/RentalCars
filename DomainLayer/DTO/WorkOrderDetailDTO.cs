using System;

namespace RentalCar.DomainLayer.DTO
{
    public class WorkOrderDetailDTO : BaseDTO
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        // Foreign keys
        public int? WorkOrderId { get; set; }
        public int? RepairId { get; set; }
        public int? SparePartId { get; set; }
    }
}
