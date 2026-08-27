using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    [Comment("Language Table is for predefined data used in the app")]
    public partial class Language : BaseEntity
    {
        public Language()
        {
            LookUpMultiLang = new HashSet<LookUpMultiLang>();
        }

        [Column("id")]
        public int Id { get; set; }
        [StringLength(50)]
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
        public string Name_ex { get; set; }
        public string Flag { get; set; }
        public bool Is_ltr { get; set; }

        [InverseProperty("Language")]
        public ICollection<LookUpMultiLang> LookUpMultiLang { get; set; }
    }
}