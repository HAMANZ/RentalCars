using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.Models
{

    [Comment("Brand Table is for predefined data used in the app")]
    public class Brand : BaseEntity
    {

        [Key]
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
    }
}