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
    public class RentalPayment : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public string TransactionReference { get; set; }
        public string Notes { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }


        [ForeignKey("PaymentMethodId")]
        public PaymentMethod PaymentMethod { get; set; }

        [ForeignKey("RentalContractId")]
        public RentalContract RentalContract { get; set; }
    }
}
