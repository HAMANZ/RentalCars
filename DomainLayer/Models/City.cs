using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.Models
{
    [Comment("City Table is for predefined cities used in the app")]
    public partial class City : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }

    }
}
