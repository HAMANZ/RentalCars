using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.Models
{

    public class FuelType : BaseEntity
    {

        [Key]
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
    }
}