using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.DomainLayer.Models
{
    public class Branch : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        public string Phone { get; set; }


        public ICollection<Car> Cars { get; set; }
    }
}
