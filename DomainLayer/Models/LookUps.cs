using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.Models
{
    public partial class LookUps
    {
        public LookUps()
        {
            LookUpMultiLang = new HashSet<LookUpMultiLang>();
            Media = new HashSet<Media>();
        }

        [Column("id")]
        public long Id { get; set; }
        public int? TableId { get; set; }
        [Column("code")]
        [StringLength(50)]
        public string Code { get; set; }
        [Column("UserId`")]
        public long? UserId { get; set; }
        public long? ParentId { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SysDate { get; set; }

        [ForeignKey("TableId")]
        [InverseProperty("LookUps")]
        public LookUpTable Table { get; set; }
        [InverseProperty("LookUp")]
        public ICollection<LookUpMultiLang> LookUpMultiLang { get; set; }
        [InverseProperty("LookUp")]
        public ICollection<Media> Media { get; set; }
        public bool isPublished { get; set; }
    }
}
