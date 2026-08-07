using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Lookups;
using FleetErp.Domain.Entities.Vehicles;

namespace FleetErp.Domain.Entities.Maintenance;

/// <summary>
/// Represents a maintenance record for a vehicle.
/// When completed with a cost, posts a MaintenanceExpense transaction via the Transaction Engine.
/// </summary>
public class MaintenanceRecord : BaseEntity
{
    public int VehicleId { get; set; }
    public int MaintenanceTypeId { get; set; }
    public int StatusId { get; set; }

    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int? OdometerAtService { get; set; }
    public string? ServiceProvider { get; set; }
    public string? Notes { get; set; }

    /// <summary>
    /// Whether the expense transaction has been posted to the accounting engine.
    /// Set to true after PostMaintenanceExpenseAsync is called.
    /// </summary>
    public bool IsExpensePosted { get; set; }

    // Navigation properties
    public Vehicle Vehicle { get; set; } = null!;
    public LookupItem MaintenanceType { get; set; } = null!;
    public LookupItem Status { get; set; } = null!;
    public ICollection<MaintenanceDocument> Documents { get; set; } = [];
}
