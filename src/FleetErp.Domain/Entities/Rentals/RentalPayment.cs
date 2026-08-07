using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Lookups;

namespace FleetErp.Domain.Entities.Rentals;

public class RentalPayment : BaseEntity
{
    public int RentalId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int PaymentMethodId { get; set; }
    public string? TransactionReference { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public Rental Rental { get; set; } = null!;
    public LookupItem PaymentMethod { get; set; } = null!;
}
