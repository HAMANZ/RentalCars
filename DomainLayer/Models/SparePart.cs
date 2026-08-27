using RentalCar.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
   public class SparePart : BaseEntity

    {
        [Key]
        public int Id { get; set; }

        public string PartNo { get; set; }


        public string Name { get; set; }
        public string Name_ar { get; set; }
        public string Description { get; set; }
        public string Whse { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal SellingPrice { get; set; }


        public int StockQty { get; set; }


        public long? SupplierId { get; set; }

        [ForeignKey("SupplierId")]


        public Supplier Supplier { get; set; }
    }
}
