using RentalCar.DomainLayer.DTO;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.DTO
{
    public partial class EUserDTO
    {

        public string Id { get; set; }
        public string FullName_ar { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Profile { get; set; }
        public string Role { get; set; }
        public long? GenderId { get; set; }
        public long Created_by { get; set; }
        public long Updated_by { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
