using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.CommonObjects.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
    }
}