using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.Models
{

    public partial class EUser : IdentityUser
    {

        public long EUserId { get; set; }
        public string FullName { get; set; }
        public string FullName_ar { get; set; }
        public string Profile { get; set; }
        public string DOB { get; set; }
        public string EmergencyContact { get; set; }
        //public long RoleId { get; set; }
        //[ForeignKey("RoleId")]
        //public Role Role { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public long? GenderId { get; set; }
        [ForeignKey("GenderId")]
        public Gender? Gender { get; set; }
        public string FToken { get; set; }
        public bool Is_deleted { get; set; }
        public long Created_by { get; set; }
        public long Updated_by { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
