using FleetErp.Application.Common;
using FleetErp.Application.Investors.Dtos;

namespace FleetErp.Application.Investors.Interfaces;

public interface IInvestorService
{
    Task<Result<InvestorDto>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<PagedResult<InvestorListDto>>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<Result<InvestorDto>> CreateAsync(CreateInvestorRequest request, CancellationToken ct = default);
    Task<Result<InvestorDto>> UpdateAsync(int id, UpdateInvestorRequest request, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);

    // Document operations
    Task<Result<IReadOnlyList<InvestorDocumentDto>>> GetDocumentsAsync(int investorId, CancellationToken ct = default);
    Task<Result<InvestorDocumentDto>> AddDocumentAsync(int investorId, CreateInvestorDocumentRequest request, CancellationToken ct = default);
    Task<Result> DeleteDocumentAsync(int investorId, int documentId, CancellationToken ct = default);
}
