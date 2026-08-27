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
        public long Id { get; set; }
        public string VIN { get; set; }
        public string EngineNo { get; set; }
        public string Model { get; set; }
        public string ChassisNumber { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public double PurchasePrice { get; set; }
        public int CurrentKM { get; set; }
        public string Description  { get; set; }
        public string InvestorId { get; set; }



        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        [ForeignKey("FuelTypeId")]
        public FuelType FuelType { get; set; }
        [ForeignKey("LicensePlateId")]
        public LicensePlate LicensePlate { get; set; }
        [ForeignKey("CarOwnerId")]
        public CarOwner CarOwner { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        [ForeignKey("CarStatusId")]
        public CarStatus CarStatus { get; set; }
        public Investor? Investor { get; set; }



        // الصيانة الدورية
        public ICollection<OilChangeSchedule> OilSchedules { get; set; }
        public ICollection<TireSchedule> TireSchedules { get; set; }
        public ICollection<BatterySchedule> BatterySchedules { get; set; }

        public ICollection<RentalContract> Contracts { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; }
        public ICollection<Insurance> Insurances { get; set; }
        public ICollection<CarDocuments> Documents { get; set; }
    }
}