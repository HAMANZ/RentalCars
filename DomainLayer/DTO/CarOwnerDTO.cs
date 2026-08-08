using System;

namespace RentalCar.DomainLayer.DTO
{
    public class CarOwnerDTO : BaseDTO
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string PassportNo { get; set; }
        public string CommercialRegister { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsCompany { get; set; }
        public bool IsActive { get; set; } = true;
        public string Notes { get; set; }

        // Foreign keys
        public string UserId { get; set; }
        public long? NationalityId { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
    }
}
