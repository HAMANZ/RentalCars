using System;

namespace RentalCar.DomainLayer.DTO
{
    public class InsuranceDTO : BaseDTO
    {
        public long Id { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Premium { get; set; }
        public double? CoverageAmount { get; set; }
        public double? Deductible { get; set; }
        public string CoverageDetails { get; set; }
        public string Notes { get; set; }
        public bool RenewalReminderSent { get; set; }

        // Foreign keys
        public long? InsuranceCompanyId { get; set; }
        public long? CarId { get; set; }
        public long? InsuranceTypeId { get; set; }
        public long? StatusId { get; set; }
    }
}
