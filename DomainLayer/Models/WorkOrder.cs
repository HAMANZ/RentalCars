using RentalCar.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.DomainLayer.Models
{
    public class WorkOrder : BaseEntity
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("CarId")]


        public Car Car { get; set; }



        public DateTime Date { get; set; }



        public int CurrentKM { get; set; }


        public double price { get; set; }
        public double PartsCost { get; set; }

        public double TotalCost { get; set; }


        [ForeignKey("StatusId")]


        public Status Status { get; set; }



        public ICollection<WorkOrderDetail> Details { get; set; }
    }
}
