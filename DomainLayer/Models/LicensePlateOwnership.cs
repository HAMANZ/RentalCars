using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class LicensePlateOwnership : BaseEntity
    {

        [Key]
        public long Id { get; set; }

        [ForeignKey("LicensePlateId")]


        public LicensePlate LicensePlate { get; set; }

        [ForeignKey("PlateOwner")]
        public string PlateOwnerId { get; set; }

        public PlateOwner PlateOwner { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrent { get; set; }

        public string Notes { get; set; }

    }
}