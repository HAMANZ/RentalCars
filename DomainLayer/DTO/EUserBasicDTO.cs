using RentalCar.DomainLayer.DTO;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.DTO
{
    public partial class EUserBasicDTO
    {

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Profile { get; set; }
        public string Token { get; set; }
    }
}
