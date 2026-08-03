using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.DTO
{
   
    public partial class AnnouncementDTO : BaseDTO
    {
        public long Id { get; set; }
        public DateTime PublishDate { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public int LanguageId { get; set; }

    }
}
