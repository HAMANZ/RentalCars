using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RentalCar.DomainLayer.Models
{

    [Comment("ResetPassword Table is for storing otp code")]
    public class ResetPassword
    {
        [Key]
        public long Id { get; set; }

        [StringLength(450)]
        public string UserId { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(5000)]
        public string Token { get; set; }

        [StringLength(10)]
        public string OTP { get; set; }

        public DateTime InsertDateTimeUTC { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}

