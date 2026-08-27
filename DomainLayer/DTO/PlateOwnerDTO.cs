using System;

namespace RentalCar.DomainLayer.DTO
{
    public class PlateOwnerDTO : BaseDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string NationalId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
