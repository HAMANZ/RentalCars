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
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public int OdometerStart { get; set; }
        public int? OdometerEnd { get; set; }

        // Financial fields
        public double DailyRate { get; set; }
        public double Discount { get; set; }
        public double TotalAmount { get; set; }
        public double PaidAmount { get; set; }

        // Computed properties
        public int TotalDays => (EndDate.Date - StartDate.Date).Days + 1;
        public double SubTotal => DailyRate * TotalDays;
        public double RemainingAmount => TotalAmount - PaidAmount;






        [ForeignKey("StatusId")]
        public Status Status { get; set; }


        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }


        [ForeignKey("CarId")]
        public Car Car { get; set; }
        public ICollection<RentalPayment> Payments { get; set; }
    }
}
