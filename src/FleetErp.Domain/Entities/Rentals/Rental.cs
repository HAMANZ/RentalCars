using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Customers;
using FleetErp.Domain.Entities.Lookups;
using FleetErp.Domain.Entities.Vehicles;

namespace FleetErp.Domain.Entities.Rentals;

public class Rental : BaseEntity
{
    public string RentalNumber { get; set; } = string.Empty;
    public int VehicleId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
    public int OdometerStart { get; set; }
    public int? OdometerEnd { get; set; }

    // Financial fields
    public decimal DailyRate { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }

    // Computed properties
    public int TotalDays => (EndDate.Date - StartDate.Date).Days + 1;
    public decimal SubTotal => DailyRate * TotalDays;
    public decimal RemainingAmount => TotalAmount - PaidAmount;

    public int StatusId { get; set; }
    public int PaymentStatusId { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public Vehicle Vehicle { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public LookupItem Status { get; set; } = null!;
    public LookupItem PaymentStatus { get; set; } = null!;
    public ICollection<RentalPayment> Payments { get; set; } = [];
}
