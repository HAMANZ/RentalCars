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
    public class Payment : BaseEntity
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("RentalContractId")]

        public RentalContract RentalContract { get; set; }



        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal Amount { get; set; }


        public string PaymentMethod { get; set; }
    }
}
