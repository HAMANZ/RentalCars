using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.CommonObjects.Requests
{
    public class UserResetPassRequest
    {
        [Required]
        public string Email_Phone { get; set; }
        public bool Is_Email { get; set; }

        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string Otp { get; set; }
    }
}