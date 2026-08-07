using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Insurance;

namespace FleetErp.Application.Insurance.Interfaces;

public interface IInsuranceCompanyRepository
{
    Task<InsuranceCompany?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PagedResult<InsuranceCompany>> GetPagedAsync(int page, int pageSize, string? search, bool? activeOnly, CancellationToken ct = default);
    Task<IReadOnlyList<InsuranceCompany>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<InsuranceCompany>> GetAllActiveAsync(CancellationToken ct = default);
    Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken ct = default);
    Task AddAsync(InsuranceCompany company, CancellationToken ct = default);
    void Update(InsuranceCompany company);
    void Delete(InsuranceCompany company);
}
