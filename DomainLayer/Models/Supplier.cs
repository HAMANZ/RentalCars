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
    public class Supplier : BaseEntity
    { 

       [Key]
        public int Id { get; set; }


        public string Name { get; set; }


        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }



        public ICollection<SparePart> Parts { get; set; }
    }
}
