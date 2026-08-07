using FleetErp.Application.Audit.Interfaces;
using FleetErp.Application.Common;
using FleetErp.Application.Common.Interfaces;
using FleetErp.Application.Customers.Dtos;
using FleetErp.Application.Customers.Interfaces;
using FleetErp.Domain.Entities.Customers;
using FleetErp.Shared.Constants;

namespace FleetErp.Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerDocumentRepository _documentRepository;
    private readonly IAuditLogger _auditLogger;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(
        ICustomerRepository customerRepository,
        ICustomerDocumentRepository documentRepository,
        IAuditLogger auditLogger,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _documentRepository = documentRepository;
        _auditLogger = auditLogger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CustomerDto>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var customer = await _customerRepository.GetByIdWithDocumentsAsync(id, ct);
        if (customer is null)
        {
            return Result<CustomerDto>.NotFound("Customer not found");
        }

        return Result<CustomerDto>.Success(MapToDto(customer));
    }

    public async Task<Result<PagedResult<CustomerListDto>>> GetPagedAsync(int page, int pageSize, string? search, CancellationToken ct = default)
    {
        var result = await _customerRepository.GetPagedAsync(page, pageSize, search, ct);

        var dtos = result.Items.Select(MapToListDto).ToList();
        return Result<PagedResult<CustomerListDto>>.Success(
            new PagedResult<CustomerListDto> { Items = dtos, Page = result.Page, PageSize = result.PageSize, TotalCount = result.TotalCount });
    }

    public async Task<Result<CustomerDto>> CreateAsync(CreateCustomerRequest request, CancellationToken ct = default)
    {
        // Check for duplicate email
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            if (await _customerRepository.EmailExistsAsync(request.Email, null, ct))
            {
                return Result<CustomerDto>.Conflict("A customer with this email already exists");
            }
        }

        // Check for duplicate national ID
        if (!string.IsNullOrWhiteSpace(request.NationalId))
        {
            if (await _customerRepository.NationalIdExistsAsync(request.NationalId, null, ct))
            {
                return Result<CustomerDto>.Conflict("A customer with this national ID already exists");
            }
        }

        // Check for duplicate driving license
        if (!string.IsNullOrWhiteSpace(request.DrivingLicenseNumber))
        {
            if (await _customerRepository.DrivingLicenseExistsAsync(request.DrivingLicenseNumber, null, ct))
            {
                return Result<CustomerDto>.Conflict("A customer with this driving license already exists");
            }
        }

        var customer = new Customer
        {
            FullName = request.FullName,
            Phone = request.Phone,
            Email = request.Email?.ToLowerInvariant(),
            NationalId = request.NationalId,
            DrivingLicenseNumber = request.DrivingLicenseNumber,
            DrivingLicenseExpiry = request.DrivingLicenseExpiry,
            Address = request.Address,
            Notes = request.Notes,
            StatusId = request.StatusId
        };

        await _customerRepository.AddAsync(customer, ct);
        _auditLogger.Log(AuditActions.CustomerCreated, nameof(Customer), customer.Id,
            userId: null, $"Customer '{customer.FullName}' created");

        await _unitOfWork.SaveChangesAsync(ct);

        // Reload with status
        var created = await _customerRepository.GetByIdAsync(customer.Id, ct);
        return Result<CustomerDto>.Success(MapToDto(created!));
    }

    public async Task<Result<CustomerDto>> UpdateAsync(int id, UpdateCustomerRequest request, CancellationToken ct = default)
    {
        var customer = await _customerRepository.GetByIdAsync(id, ct);
        if (customer is null)
        {
            return Result<CustomerDto>.NotFound("Customer not found");
        }

        // Check for duplicate email
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            if (await _customerRepository.EmailExistsAsync(request.Email, id, ct))
            {
                return Result<CustomerDto>.Conflict("A customer with this email already exists");
            }
        }

        // Check for duplicate national ID
        if (!string.IsNullOrWhiteSpace(request.NationalId))
        {
            if (await _customerRepository.NationalIdExistsAsync(request.NationalId, id, ct))
            {
                return Result<CustomerDto>.Conflict("A customer with this national ID already exists");
            }
        }

        // Check for duplicate driving license
        if (!string.IsNullOrWhiteSpace(request.DrivingLicenseNumber))
        {
            if (await _customerRepository.DrivingLicenseExistsAsync(request.DrivingLicenseNumber, id, ct))
            {
                return Result<CustomerDto>.Conflict("A customer with this driving license already exists");
            }
        }

        customer.FullName = request.FullName;
        customer.Phone = request.Phone;
        customer.Email = request.Email?.ToLowerInvariant();
        customer.NationalId = request.NationalId;
        customer.DrivingLicenseNumber = request.DrivingLicenseNumber;
        customer.DrivingLicenseExpiry = request.DrivingLicenseExpiry;
        customer.Address = request.Address;
        customer.Notes = request.Notes;
        customer.StatusId = request.StatusId;

        _customerRepository.Update(customer);
        _auditLogger.Log(AuditActions.CustomerUpdated, nameof(Customer), customer.Id,
            userId: null, $"Customer '{customer.FullName}' updated");

        await _unitOfWork.SaveChangesAsync(ct);

        var updated = await _customerRepository.GetByIdAsync(customer.Id, ct);
        return Result<CustomerDto>.Success(MapToDto(updated!));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var customer = await _customerRepository.GetByIdAsync(id, ct);
        if (customer is null)
        {
            return Result.NotFound("Customer not found");
        }

        _customerRepository.Delete(customer);
        _auditLogger.Log(AuditActions.CustomerDeleted, nameof(Customer), customer.Id,
            userId: null, $"Customer '{customer.FullName}' deleted");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result<IReadOnlyList<CustomerDocumentDto>>> GetDocumentsAsync(int customerId, CancellationToken ct = default)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId, ct);
        if (customer is null)
        {
            return Result<IReadOnlyList<CustomerDocumentDto>>.NotFound("Customer not found");
        }

        var documents = await _documentRepository.GetByCustomerIdAsync(customerId, ct);
        var dtos = documents.Select(MapDocumentToDto).ToList();

        return Result<IReadOnlyList<CustomerDocumentDto>>.Success(dtos);
    }

    public async Task<Result<CustomerDocumentDto>> AddDocumentAsync(int customerId, CreateCustomerDocumentRequest request, CancellationToken ct = default)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId, ct);
        if (customer is null)
        {
            return Result<CustomerDocumentDto>.NotFound("Customer not found");
        }

        var document = new CustomerDocument
        {
            CustomerId = customerId,
            DocumentTypeId = request.DocumentTypeId,
            FilePath = request.FilePath,
            ExpiresAt = request.ExpiresAt
        };

        await _documentRepository.AddAsync(document, ct);
        _auditLogger.Log(AuditActions.CustomerDocumentAdded, nameof(CustomerDocument), document.Id,
            userId: null, $"Document added to customer '{customer.FullName}'");

        await _unitOfWork.SaveChangesAsync(ct);

        var created = await _documentRepository.GetByIdAsync(document.Id, ct);
        return Result<CustomerDocumentDto>.Success(MapDocumentToDto(created!));
    }

    public async Task<Result> DeleteDocumentAsync(int customerId, int documentId, CancellationToken ct = default)
    {
        var document = await _documentRepository.GetByIdAsync(documentId, ct);
        if (document is null || document.CustomerId != customerId)
        {
            return Result.NotFound("Document not found");
        }

        _documentRepository.Delete(document);
        _auditLogger.Log(AuditActions.CustomerDocumentDeleted, nameof(CustomerDocument), documentId,
            userId: null, $"Document deleted from customer ID {customerId}");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    private static CustomerDto MapToDto(Customer customer)
    {
        return new CustomerDto(
            customer.Id,
            customer.FullName,
            customer.Phone,
            customer.Email,
            customer.NationalId,
            customer.DrivingLicenseNumber,
            customer.DrivingLicenseExpiry,
            customer.Address,
            customer.Notes,
            customer.StatusId,
            customer.Status.Name,
            customer.CreatedAt,
            customer.UpdatedAt,
            customer.Documents.Count,
            0 // RentalCount - would need to be loaded separately
        );
    }

    private static CustomerListDto MapToListDto(Customer customer)
    {
        return new CustomerListDto(
            customer.Id,
            customer.FullName,
            customer.Phone,
            customer.Email,
            customer.NationalId,
            customer.Status.Name,
            0 // RentalCount
        );
    }

    private static CustomerDocumentDto MapDocumentToDto(CustomerDocument document)
    {
        return new CustomerDocumentDto(
            document.Id,
            document.CustomerId,
            document.DocumentTypeId,
            document.DocumentType.Name,
            document.FilePath,
            document.ExpiresAt,
            document.IsExpired,
            document.CreatedAt
        );
    }
}
