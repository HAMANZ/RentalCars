using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.Models
{

    [Comment("Nationality Table is for predefined data used in the app")]
    public class Nationality : BaseEntity
    {

        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
    }
}