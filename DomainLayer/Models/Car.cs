using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.DomainLayer.Models
{
    public class Car : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string VIN { get; set; }

        public string EngineNo { get; set; }



        public string Model { get; set; }

        public int Year { get; set; }

        public string Color { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal PurchasePrice { get; set; }


        public int CurrentKM { get; set; }
        public string Description  { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }


        [ForeignKey("LicensePlateId")]

        public LicensePlate LicensePlate { get; set; }

        [ForeignKey("CarOwnerId")]

        public CarOwner CarOwner { get; set; }
        [ForeignKey("BrandId")]

        public Brand Brand { get; set; }

        // الصيانة الدورية

        public ICollection<OilChangeSchedule> OilSchedules { get; set; }


        public ICollection<TireSchedule> TireSchedules { get; set; }


        public ICollection<BatterySchedule> BatterySchedules { get; set; }
        public ICollection<RentalContract> Contracts { get; set; }

        public ICollection<WorkOrder> WorkOrders { get; set; }

        public ICollection<Insurance> Insurances { get; set; }
    }
}