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
    public class RentalContract : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CustomerId")]

        public Customer Customer { get; set; }




        [ForeignKey("CarId")]
        public Car Car { get; set; }



        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }


        [Column(TypeName = "decimal(18,2)")]

        public decimal DailyRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal PaidAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal Remaining { get; set; }



        public string Status { get; set; }



        public ICollection<Payment> Payments { get; set; }
    }
}
