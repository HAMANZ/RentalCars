using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class Insurance : BaseEntity
    {

        [Key]
        public long Id { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public double Premium { get; set; }
        public double? CoverageAmount { get; set; }
        public double? Deductible { get; set; }
        public string? CoverageDetails { get; set; }
        public string? Notes { get; set; }

        public bool RenewalReminderSent { get; set; }

        [ForeignKey("InsuranceCompanyId")]
        public InsuranceCompany InsuranceCompany { get; set; }
        [ForeignKey("CarId")]

        public Car Car { get; set; }
        [ForeignKey("InsuranceTypeId")]
        public InsuranceType InsuranceType { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }


        public ICollection<InsuranceDocument> Documents { get; set; }
    }
}