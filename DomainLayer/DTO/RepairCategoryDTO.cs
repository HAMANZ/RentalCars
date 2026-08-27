using System;

namespace RentalCar.DomainLayer.DTO
{
    public class RepairCategoryDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
    }
}
