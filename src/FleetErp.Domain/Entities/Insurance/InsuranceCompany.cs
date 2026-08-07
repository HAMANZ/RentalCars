using FleetErp.Domain.Common;

namespace FleetErp.Domain.Entities.Insurance;

/// <summary>
/// Represents an insurance company.
/// This is a dedicated entity (not a generic lookup) because it needs additional contact fields.
/// </summary>
public class InsuranceCompany : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? ContactPerson { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public ICollection<InsuranceRecord> InsuranceRecords { get; set; } = [];
}
