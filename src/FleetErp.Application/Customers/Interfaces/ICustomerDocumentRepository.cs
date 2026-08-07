using FleetErp.Domain.Entities.Customers;

namespace FleetErp.Application.Customers.Interfaces;

public interface ICustomerDocumentRepository
{
    Task<CustomerDocument?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<CustomerDocument>> GetByCustomerIdAsync(int customerId, CancellationToken ct = default);
    Task AddAsync(CustomerDocument document, CancellationToken ct = default);
    void Delete(CustomerDocument document);
}
