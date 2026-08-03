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

namespace RentalCar.DomainLayer.Model
{
    [Comment("App Setting table to add all information used and related for the website like: Application name, Contact data ........")]
    public class AppSettings : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        [Comment("Logo for website")]
        public string Logo { get; set; }
        public string ApplicationName { get; set; }

        public string ApplicationUrl { get; set; }
        public string ApplicationApiUrl { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        [Comment("Contact data Used for website")]
        public string ContactWebsite { get; set; }
        public string PrivacyPolicy { get; set; }
        public string TermsConditions { get; set; }
        public string LicenseDetail { get; set; }
        public string RefundPolicy  { get; set; }
        public string Phone { get; set; }

        public string Phone2 { get; set; }
        public string Mobile { get; set; }
        public string Mobile2 { get; set; }
        public string ContactEmail { get; set; }

        public string Password { get; set; }
        [Comment("Email Used for website")]
        public string Email { get; set; }
        [Comment("Facebook Link")]
        public string Facebook { get; set; }
        [Comment("Twitter Link")]
        public string Twitter { get; set; }
        [Comment("LinkedIn Link")]
        public string LinkedIn { get; set; }
        [Comment("Youtube Link")]
        public string Youtube { get; set; }
        [Comment("Instagram Link")]
        public string Instagram { get; set; }
        [Comment("snapchat Link")]
        public string Snapchat { get; set; }
        [Comment("tiktok Link")]
        public string Tiktok { get; set; }
        [Comment("Whatsapp Link")]
        public string Whatsapp { get; set; }
    }
}
