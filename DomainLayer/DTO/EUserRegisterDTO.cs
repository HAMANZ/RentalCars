using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
    public class EUserRegisterDTO
    {
        public long UserId { get; set; }
        public string Id { get; set; }
        public string FullName { get; set; }
        public string FullName_ar { get; set; }
        public string Profile { get; set; }

        [MinLength(8, ErrorMessage = "The Password value cannot less 8 symbols. ")]
        [MaxLength(25, ErrorMessage = "The Password value cannot exceed 25 symbols. ")]
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImage { get; set; }
        public IFormFile Image { get; set; }
        public string Role { get; set; }
        public bool Is_deleted { get; set; }
        public bool Is_Admin { get; set; }
        public long Created_by { get; set; }
        public DateTime Created_at { get; set; }
    }
}
