using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Insurance;

namespace FleetErp.Application.Insurance.Interfaces;

public interface IInsuranceRepository
{
    Task<InsuranceRecord?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<InsuranceRecord?> GetByIdWithDocumentsAsync(int id, CancellationToken ct = default);
    Task<PagedResult<InsuranceRecord>> GetPagedAsync(int page, int pageSize, string? search, int? vehicleId, int? statusId, int? insuranceTypeId, int? companyId, CancellationToken ct = default);
    Task<IReadOnlyList<InsuranceRecord>> GetByVehicleIdAsync(int vehicleId, CancellationToken ct = default);
    Task<IReadOnlyList<InsuranceRecord>> GetExpiringAsync(int days, CancellationToken ct = default);
    Task<InsuranceRecord?> GetActiveByVehicleIdAsync(int vehicleId, CancellationToken ct = default);
    Task<bool> ExistsByPolicyNumberAsync(string policyNumber, int? excludeId = null, CancellationToken ct = default);
    Task AddAsync(InsuranceRecord record, CancellationToken ct = default);
    void Update(InsuranceRecord record);
    void Delete(InsuranceRecord record);
}
