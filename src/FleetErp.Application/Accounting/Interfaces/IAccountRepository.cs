using FleetErp.Domain.Entities.Accounting;

namespace FleetErp.Application.Accounting.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Account?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task<Account?> GetByOwnerAsync(string ownerType, int ownerId, CancellationToken ct = default);
    Task<IReadOnlyList<Account>> GetByOwnerTypeAsync(string ownerType, CancellationToken ct = default);
    Task<IReadOnlyList<Account>> GetSystemAccountsAsync(CancellationToken ct = default);
    Task AddAsync(Account account, CancellationToken ct = default);
    void Update(Account account);
}
