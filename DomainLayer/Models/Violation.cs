using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class Violation : BaseEntity
    {

        [Key]
        public long Id { get; set; }

        [ForeignKey("CarId")]

        public Car Car { get; set; }

        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal Amount { get; set; }


        public bool Paid { get; set; }
    }
}