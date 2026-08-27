using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class OilChangeSchedule : BaseEntity
    {

        [Key]
        public long Id { get; set; }

        [ForeignKey("CarId")]

        public Car Car { get; set; }

        // آخر تغيير زيت
        public DateTime LastChangeDate { get; set; }
        // عداد السيارة وقت تغيير الزيت
        public int LastChangeKM { get; set; }
        // كل كم يجب تغيير الزيت
        // مثال: كل 5000 KM
        public int ChangeIntervalKM { get; set; } = 5000;
        // موعد التغيير القادم بالكيلومتر
        public int NextChangeKM
        {
            get
            {
                return LastChangeKM + ChangeIntervalKM;
            }
        }

        // نوع الزيت
        public string OilType { get; set; }

        // تكلفة الزيت

        public double Cost { get; set; }
        // ملاحظات
        public string Notes { get; set; }
    }
}