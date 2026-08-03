using RentalCar.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.DomainLayer.Model
{
    [Comment(" AppSocialMediaAccounts table to add all information used and related for the website like: social media........")]
    public class AppSocialMediaAccounts : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        [Comment("Facebook Link")]
        public string Facebook { get; set; }
        [Comment("Twitter Link")]
        public string Twitter { get; set; }
        [Comment("Instagram Link")]
        public string Instagram { get; set; }
        [Comment("TikTok Link")]
        public string TikTok { get; set; }
    }
}
