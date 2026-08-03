using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.CommonObjects.Requests
{
    public class UserRequest
    {
        [Required]
        public string Email_Phone { get; set; }
        public bool Is_Email { get; set; }
    }
}