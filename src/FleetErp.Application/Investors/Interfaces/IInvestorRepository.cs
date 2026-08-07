using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Investors;

namespace FleetErp.Application.Investors.Interfaces;

public interface IInvestorRepository
{
    Task<Investor?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Investor?> GetByIdWithDocumentsAsync(int id, CancellationToken ct = default);
    Task<PagedResult<Investor>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<bool> EmailExistsAsync(string email, int? excludeInvestorId = null, CancellationToken ct = default);
    Task<bool> NationalIdExistsAsync(string nationalId, int? excludeInvestorId = null, CancellationToken ct = default);
    Task AddAsync(Investor investor, CancellationToken ct = default);
    void Update(Investor investor);
    void Delete(Investor investor);
}
