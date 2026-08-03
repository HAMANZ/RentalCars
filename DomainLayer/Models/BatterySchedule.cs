using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class BatterySchedule : BaseEntity
    {

        [Key]
        public long Id { get; set; }

        [ForeignKey("CarId")]

        public Car Car { get; set; }

        // تاريخ تركيب البطارية
        public DateTime InstallDate { get; set; }



        // عمر البطارية المتوقع بالشهور
        // عادة 24 شهر
        public int LifeMonths { get; set; } = 24;



        // تاريخ انتهاء العمر المتوقع
        public DateTime ExpiryDate
        {
            get
            {
                return InstallDate.AddMonths(LifeMonths);
            }
        }



        public string Brand { get; set; }


        [Column(TypeName = "decimal(18,2)")]

        public decimal Cost { get; set; }



        public string Warranty { get; set; }



        public string Notes { get; set; }
    }
}