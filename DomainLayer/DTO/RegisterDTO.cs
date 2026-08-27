using RentalCar.DomainLayer.DTO;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.DTO
{
    public partial class RegisterDTO : BaseDTO
    {

        public string Id { get; set; }
        public long EUserId { get; set; }
        public string FullName_ar { get; set; }
        public string FullName { get; set; }
        public long GenderId { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
       public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string TokenId { get; set; }
        //public string Discriminator { get; set; }
        //public string Profile { get; set; }
        //public long GenderId { get; set; }
    }
}
