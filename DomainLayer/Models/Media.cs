using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.Models
{
    public partial class Media: BaseEntity
    {


        [Column("id")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public string Name_ar { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SysDate { get; set; }
        [Column("isVideo")]
        public bool? IsVideo { get; set; }
        public long? LookUpId { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }

        [ForeignKey("LookUpId")]
        [InverseProperty("Media")]
        public LookUps LookUp { get; set; }
    }
}
