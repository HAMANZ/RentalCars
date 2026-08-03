using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.CommonObjects.Requests
{
    public class TokenRequest
    {
        [Required]
        public string Token { get; set; }

         [Required]
        public string RefreshToken { get; set; }
    }
}