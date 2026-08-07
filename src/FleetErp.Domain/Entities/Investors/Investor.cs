using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Accounting;
using FleetErp.Domain.Entities.Lookups;

namespace FleetErp.Domain.Entities.Investors;

/// <summary>
/// Represents an investor who owns vehicles and receives revenue.
/// Investors never touch operations directly.
/// Each investor has an associated Account for tracking payables.
/// </summary>
public class Investor : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? NationalId { get; set; }
    public int StatusId { get; set; }
    public int? AccountId { get; set; }

    // Navigation properties
    public LookupItem Status { get; set; } = null!;
    public Account? Account { get; set; }
    public ICollection<InvestorDocument> Documents { get; set; } = [];
}
