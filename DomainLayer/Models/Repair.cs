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
        public string Name_ar { get; set; }




        public int RepairTypeId { get; set; }

        [ForeignKey(nameof(RepairTypeId))]
        public RepairType RepairType { get; set; }
        public string Details { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal WorkTime { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal LaborCost { get; set; }

        public double Price { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string Description_ar { get; set; }
        public string Note_ar { get; set; }


        public ICollection<WorkOrderDetail> WorkOrderDetails { get; set; }
    }
}
