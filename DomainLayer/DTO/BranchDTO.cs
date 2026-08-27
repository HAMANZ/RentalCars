using System;

namespace RentalCar.DomainLayer.DTO
{
    public class BranchDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
        public string Phone { get; set; }

        // Foreign keys
        public long? CityId { get; set; }
    }
}
