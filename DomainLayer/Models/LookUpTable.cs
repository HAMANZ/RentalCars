using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.Models
{
    public partial class LookUpTable : BaseEntity
    {
        public LookUpTable()
        {
            LookUps = new HashSet<LookUps>();
        }

        [Column("id")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SysDate { get; set; }

        [InverseProperty("Table")]
        public ICollection<LookUps> LookUps { get; set; }
    }
}
