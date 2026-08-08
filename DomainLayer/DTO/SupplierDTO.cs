using System;

namespace RentalCar.DomainLayer.DTO
{
    public class SupplierDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
