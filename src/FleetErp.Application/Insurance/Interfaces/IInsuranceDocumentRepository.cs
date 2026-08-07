using FleetErp.Domain.Entities.Insurance;

namespace FleetErp.Application.Insurance.Interfaces;

public interface IInsuranceDocumentRepository
{
    Task<InsuranceDocument?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<InsuranceDocument>> GetByInsuranceRecordIdAsync(int insuranceRecordId, CancellationToken ct = default);
    Task AddAsync(InsuranceDocument document, CancellationToken ct = default);
    void Delete(InsuranceDocument document);
}
