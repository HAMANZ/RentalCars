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
    public class RepairType : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Name_ar { get; set; }


        public bool IsActive { get; set; }
        public string Code { get; set; }
        public int RepairCategoryId { get; set; }

        [ForeignKey(nameof(RepairCategoryId))]
        public RepairCategory RepairCategory { get; set; }
    }
}
