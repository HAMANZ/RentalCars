
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.Models
{
    public partial class Announcements : BaseEntity
    {
      
        [Key]
        public long Id { get; set; }
        public DateTime PublishDate { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }

        [ForeignKey("LanguageId")]
        public int LanguageId { get; set; }

    }
}
