using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.Models
{
    public partial class LookUpMultiLang : BaseEntity
    {
        [Column("id")]
        public int Id { get; set; }
        public long LookUpId { get; set; }
  
        public string Description { get; set; }
        public int? LanguageId { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SysDate { get; set; }

        [ForeignKey("LanguageId")]
        [InverseProperty("LookUpMultiLang")]
        public Language Language { get; set; }
        [ForeignKey("LookUpId")]
        [InverseProperty("LookUpMultiLang")]
        public LookUps LookUp { get; set; }
    }
}
