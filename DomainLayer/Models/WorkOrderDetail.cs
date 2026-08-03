using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class WorkOrderDetail : BaseEntity
    {

        [Key]
        public long Id { get; set; }


        [ForeignKey("WorkOrderId")]

        public WorkOrder WorkOrder { get; set; }




        [ForeignKey("RepairId")]


        public Repair Repair { get; set; }




        public int? SparePartId { get; set; }


        public SparePart SparePart { get; set; }




        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
    }
}