using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class TireSchedule : BaseEntity
    {

        [Key]
        public long Id { get; set; }

        [ForeignKey("CarId")]

        public Car Car { get; set; }


        // تاريخ تركيب الإطارات
        public DateTime InstallDate { get; set; }



        // عداد السيارة عند التركيب
        public int InstallKM { get; set; }



        // العمر المتوقع للإطار بالكيلومتر
        // مثال: 40000 KM
        public int ExpectedKM { get; set; } = 40000;



        // موعد التغيير القادم
        public int NextChangeKM
        {
            get
            {
                return InstallKM + ExpectedKM;
            }
        }



        public string Brand { get; set; }



        public int Quantity { get; set; } = 4;


        [Column(TypeName = "decimal(18,2)")]

        public decimal Cost { get; set; }



        public string Notes { get; set; }
    }
}