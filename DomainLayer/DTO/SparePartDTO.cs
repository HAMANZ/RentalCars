using System;

namespace RentalCar.DomainLayer.DTO
{
    public class SparePartDTO : BaseDTO
    {
        public int Id { get; set; }
        public string PartNo { get; set; }
        public string Name { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int StockQty { get; set; }

        // Foreign keys
        public int? SupplierId { get; set; }
    }
}
