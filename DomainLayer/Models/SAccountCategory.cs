using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.Models
{
    public class SAccountCategory : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
        public ICollection<SAccountType> AccountTypes { get; set; }
     = new List<SAccountType>();
    }
}