using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.Models
{
    public partial class STransactionDocuments : BaseEntity
    {

        [Key]
        public long Id { get; set; }
        public string FilePath { get; set; }

        [Column("isActive")]
        public bool? IsActive { get; set; } = true;

        [ForeignKey("DocumentTypeId")]
        public DocumentType DocumentType { get; set; }

        // Scalar FK typed to match STransaction's primary key (TransactionId : long).
        public long? STransactionId { get; set; }

        [ForeignKey("STransactionId")]
        public STransaction STransaction { get; set; }
    }
}
