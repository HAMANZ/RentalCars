using RentalCar.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Supplier :BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long Id { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }


        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }



        public string? UserId { get; set; }

        public EUser User { get; set; }
        public ICollection<SparePart> Parts { get; set; }
    }
}
