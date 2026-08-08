using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.DomainLayer.Models
{
    public class BaseEntity
    {

        [Comment("Is Deleted Record")]
        public bool Is_deleted { get; set; }

        [Comment("User Id that Created_by")]
        public long Created_by { get; set; }

        [Comment("User Id that Updated_by")]
        public long Updated_by { get; set; }

        [Comment("DateTime that Created_at")]
        public DateTime Created_at { get; set; } = DateTime.UtcNow;

        [Comment("DateTime that Updated_at")]
        public DateTime Updated_at { get; set; }
    }
}
