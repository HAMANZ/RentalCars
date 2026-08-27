using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.Models
{
    public partial class Documents: BaseEntity
    {


        [Key]
        public long Id { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }

        public DateTime? ExpiresAt { get; set; }

        // Computed property
        public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;

        [Column("isActive")]
        public bool? IsActive { get; set; } = true;

        [ForeignKey("DocumentTypeId")]
        public DocumentType DocumentType { get; set; }

        [ForeignKey("UserId")]
        public EUser User { get; set; }
        public bool IsCustomerDoc { get; set; }
    }
}
