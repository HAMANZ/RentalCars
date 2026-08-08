using System;

namespace RentalCar.DomainLayer.DTO
{
    public class RepairDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Details { get; set; }
        public decimal WorkTime { get; set; }
        public decimal LaborCost { get; set; }
    }
}
