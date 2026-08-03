using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.Models
{

    [Comment("App Label table for adding the label of the website in different languages")]
    public  class AppLabel : BaseEntity
    {
       
        [Key]
        public long Id { get; set; }
        [Comment("Label Name for the website")]
        public string LabelName { get; set; }
        [Comment("friendly Name for Label")]
        public string FriendlyName { get; set; }
        public string Value { get; set; }
        [Comment(" Description for label")]
        public string Desc { get; set; }
        [Comment("For which language  this label")]
        public int LanguagId { get; set; }
    }
}
