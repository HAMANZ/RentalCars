using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.CommonObjects.Requests
{
    public class ChangePassword
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
