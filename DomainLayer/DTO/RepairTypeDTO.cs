using System;

namespace RentalCar.DomainLayer.DTO
{
    public class RepairTypeDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public int RepairCategoryId { get; set; }
        public string RepairCategoryName { get; set; }
    }
}
