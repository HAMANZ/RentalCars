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
    public class Repair : BaseEntity
    {
        [Key]
        public int Id { get; set; }


        public string Name { get; set; }


        public string Category { get; set; }
        public string Details { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal WorkTime { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal LaborCost { get; set; }



        public ICollection<WorkOrderDetail> WorkOrderDetails { get; set; }
    }
}
