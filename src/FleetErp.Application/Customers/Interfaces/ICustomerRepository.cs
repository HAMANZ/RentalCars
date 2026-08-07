using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Customers;

namespace FleetErp.Application.Customers.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Customer?> GetByIdWithDocumentsAsync(int id, CancellationToken ct = default);
    Task<PagedResult<Customer>> GetPagedAsync(int page, int pageSize, string? search, CancellationToken ct = default);
    Task<bool> EmailExistsAsync(string email, int? excludeCustomerId = null, CancellationToken ct = default);
    Task<bool> NationalIdExistsAsync(string nationalId, int? excludeCustomerId = null, CancellationToken ct = default);
    Task<bool> DrivingLicenseExistsAsync(string licenseNumber, int? excludeCustomerId = null, CancellationToken ct = default);
    Task AddAsync(Customer customer, CancellationToken ct = default);
    void Update(Customer customer);
    void Delete(Customer customer);
}
