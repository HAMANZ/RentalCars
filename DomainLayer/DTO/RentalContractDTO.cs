using System;

namespace RentalCar.DomainLayer.DTO
{
    public class RentalContractDTO : BaseDTO
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public int OdometerStart { get; set; }
        public int? OdometerEnd { get; set; }

        // Financial fields
        public double DailyRate { get; set; }
        public double Discount { get; set; }
        public double TotalAmount { get; set; }
        public double PaidAmount { get; set; }

        // Computed (read-only) properties
        public int TotalDays => (EndDate.Date - StartDate.Date).Days + 1;
        public double SubTotal => DailyRate * TotalDays;
        public double RemainingAmount => TotalAmount - PaidAmount;

        // Foreign keys
        public long? StatusId { get; set; }
        public string CustomerId { get; set; }
        public long? CarId { get; set; }
    }
}
