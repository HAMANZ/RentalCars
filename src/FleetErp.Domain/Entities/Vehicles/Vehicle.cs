using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Accounting;
using FleetErp.Domain.Entities.Investors;
using FleetErp.Domain.Entities.Lookups;

namespace FleetErp.Domain.Entities.Vehicles;

/// <summary>
/// Represents a vehicle in the fleet.
/// Each vehicle belongs to an Investor and has various lookup-based attributes.
/// Each vehicle has an associated Account for tracking operational expenses.
/// </summary>
public class Vehicle : BaseEntity
{
    public string PlateNumber { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? Color { get; set; }
    public string? Vin { get; set; }
    public string? EngineNumber { get; set; }
    public string? ChassisNumber { get; set; }
    public int Odometer { get; set; }
    public decimal DailyRate { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public string? Notes { get; set; }

    // Foreign keys
    public int InvestorId { get; set; }
    public int StatusId { get; set; }
    public int VehicleTypeId { get; set; }
    public int FuelTypeId { get; set; }
    public int TransmissionTypeId { get; set; }
    public int? AccountId { get; set; }

    // Navigation properties
    public Investor Investor { get; set; } = null!;
    public LookupItem Status { get; set; } = null!;
    public LookupItem VehicleType { get; set; } = null!;
    public LookupItem FuelType { get; set; } = null!;
    public LookupItem TransmissionType { get; set; } = null!;
    public Account? Account { get; set; }
    public ICollection<VehicleDocument> Documents { get; set; } = [];
}
