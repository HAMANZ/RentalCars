using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.Models
{
    [Comment("Banner Table is for predefined banners used in the landing page")]
    public partial class Banner : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string AimTitle { get; set; }
        public string AimText { get; set; }
        public string ContributeTitle { get; set; }
        public string ContributeText { get; set; }
        public string EbookTitle { get; set; }
        public string EbookText { get; set; }
        public string Link { get; set; }

    }
}
