using System;

namespace RentalCar.DomainLayer.DTO
{
    public class RepairDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
        public int RepairTypeId { get; set; }
        public string RepairTypeName { get; set; }
        public string Details { get; set; }
        public decimal WorkTime { get; set; }
        public decimal LaborCost { get; set; }
    }
}
