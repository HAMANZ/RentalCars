using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class LicensePlate : BaseEntity
    {

        [Key]
        public long Id { get; set; }
        public string PlateNumber { get; set; }
        public long PlateTypeId { get; set; }
        [ForeignKey("PlateTypeId")]
        public PlateType PlateType { get; set; }
        public long? PlateRegionId { get; set; }
        [ForeignKey("PlateRegionId")]
        public PlateRegion PlateRegion { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Car> Cars { get; set; }

        public ICollection<LicensePlateOwnership> Ownerships { get; set; }
    }
}