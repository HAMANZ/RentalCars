using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class Inspection : BaseEntity
    {

        [Key]
        public long Id { get; set; }

        [ForeignKey("CarId")]

        public Car Car { get; set; }

        public DateTime Date { get; set; }


        public DateTime ExpiryDate { get; set; }
    }
}