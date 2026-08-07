using FleetErp.Application.Common;
using FleetErp.Application.Customers.Dtos;

namespace FleetErp.Application.Customers.Interfaces;

public interface ICustomerService
{
    Task<Result<CustomerDto>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<PagedResult<CustomerListDto>>> GetPagedAsync(int page, int pageSize, string? search, CancellationToken ct = default);
    Task<Result<CustomerDto>> CreateAsync(CreateCustomerRequest request, CancellationToken ct = default);
    Task<Result<CustomerDto>> UpdateAsync(int id, UpdateCustomerRequest request, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);

    // Document operations
    Task<Result<IReadOnlyList<CustomerDocumentDto>>> GetDocumentsAsync(int customerId, CancellationToken ct = default);
    Task<Result<CustomerDocumentDto>> AddDocumentAsync(int customerId, CreateCustomerDocumentRequest request, CancellationToken ct = default);
    Task<Result> DeleteDocumentAsync(int customerId, int documentId, CancellationToken ct = default);
}
