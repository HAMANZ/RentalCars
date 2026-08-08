using System;

namespace RentalCar.DomainLayer.DTO
{
    public class InvestorDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DrivingLicense { get; set; }
        public string Address { get; set; }
        public DateTime LicenseExpiryDate { get; set; }

        // Foreign keys
        public long? StatusId { get; set; }
        public long? NationalityId { get; set; }
        public string UserId { get; set; }
    }
}
