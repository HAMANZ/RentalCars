using FleetErp.Domain.Entities.Investors;

namespace FleetErp.Application.Investors.Interfaces;

public interface IInvestorDocumentRepository
{
    Task<InvestorDocument?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<InvestorDocument>> GetByInvestorIdAsync(int investorId, CancellationToken ct = default);
    Task AddAsync(InvestorDocument document, CancellationToken ct = default);
    void Update(InvestorDocument document);
    void Delete(InvestorDocument document);
}
