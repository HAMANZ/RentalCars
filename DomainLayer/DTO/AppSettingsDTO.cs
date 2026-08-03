using RentalCar.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.DomainLayer.DTO
{
     public class AppSettingsDTO:BaseDTO 
    {
        public long Id { get; set; }
        public string Logo { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationUrl { get; set; }
        public string ApplicationApiUrl { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ContactWebsite { get; set; }
        public string ContactEmail { get; set; }
        public string PrivacyPolicy { get; set; }
        public string TermsConditions { get; set; }
        public string LicenseDetail { get; set; }
        public string RefundPolicy  { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Mobile { get; set; }
        public string Mobile2 { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
        public string Youtube { get; set; }
        public string Instagram { get; set; }
        public string Snapchat { get; set; }
        public string Tiktok { get; set; }
        public string Whatsapp { get; set; }
    }
}
