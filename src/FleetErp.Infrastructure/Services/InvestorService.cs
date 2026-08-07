using FleetErp.Application.Audit.Interfaces;
using FleetErp.Application.Common;
using FleetErp.Application.Common.Interfaces;
using FleetErp.Application.Investors.Dtos;
using FleetErp.Application.Investors.Interfaces;
using FleetErp.Domain.Entities.Investors;
using FleetErp.Shared.Constants;

namespace FleetErp.Infrastructure.Services;

public class InvestorService : IInvestorService
{
    private readonly IInvestorRepository _investorRepository;
    private readonly IInvestorDocumentRepository _documentRepository;
    private readonly IAuditLogger _auditLogger;
    private readonly IUnitOfWork _unitOfWork;

    public InvestorService(
        IInvestorRepository investorRepository,
        IInvestorDocumentRepository documentRepository,
        IAuditLogger auditLogger,
        IUnitOfWork unitOfWork)
    {
        _investorRepository = investorRepository;
        _documentRepository = documentRepository;
        _auditLogger = auditLogger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<InvestorDto>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var investor = await _investorRepository.GetByIdWithDocumentsAsync(id, ct);
        if (investor == null)
        {
            return Result<InvestorDto>.NotFound("Investor not found");
        }

        return Result<InvestorDto>.Success(MapToDto(investor));
    }

    public async Task<Result<PagedResult<InvestorListDto>>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default)
    {
        var pagedResult = await _investorRepository.GetPagedAsync(page, pageSize, search, ct);

        var dtos = pagedResult.Items.Select(MapToListDto).ToList();

        return Result<PagedResult<InvestorListDto>>.Success(new PagedResult<InvestorListDto>
        {
            Items = dtos,
            Page = pagedResult.Page,
            PageSize = pagedResult.PageSize,
            TotalCount = pagedResult.TotalCount
        });
    }

    public async Task<Result<InvestorDto>> CreateAsync(CreateInvestorRequest request, CancellationToken ct = default)
    {
        // Check for duplicate email if provided
        if (!string.IsNullOrEmpty(request.Email))
        {
            if (await _investorRepository.EmailExistsAsync(request.Email, null, ct))
            {
                return Result<InvestorDto>.Conflict("An investor with this email already exists");
            }
        }

        // Check for duplicate national ID if provided
        if (!string.IsNullOrEmpty(request.NationalId))
        {
            if (await _investorRepository.NationalIdExistsAsync(request.NationalId, null, ct))
            {
                return Result<InvestorDto>.Conflict("An investor with this national ID already exists");
            }
        }

        var investor = new Investor
        {
            FullName = request.FullName,
            Phone = request.Phone,
            Email = request.Email?.ToLowerInvariant(),
            NationalId = request.NationalId,
            StatusId = request.StatusId
        };

        await _investorRepository.AddAsync(investor, ct);

        _auditLogger.Log(AuditActions.InvestorCreated, nameof(Investor), investor.Id, investor.Id.ToString(), $"Investor {investor.FullName} created");

        await _unitOfWork.SaveChangesAsync(ct);

        // Reload with status
        var createdInvestor = await _investorRepository.GetByIdWithDocumentsAsync(investor.Id, ct);
        return Result<InvestorDto>.Success(MapToDto(createdInvestor!));
    }

    public async Task<Result<InvestorDto>> UpdateAsync(int id, UpdateInvestorRequest request, CancellationToken ct = default)
    {
        var investor = await _investorRepository.GetByIdAsync(id, ct);
        if (investor == null)
        {
            return Result<InvestorDto>.NotFound("Investor not found");
        }

        // Check for duplicate email if being changed
        if (!string.IsNullOrEmpty(request.Email))
        {
            if (!string.Equals(investor.Email, request.Email, StringComparison.OrdinalIgnoreCase))
            {
                if (await _investorRepository.EmailExistsAsync(request.Email, id, ct))
                {
                    return Result<InvestorDto>.Conflict("An investor with this email already exists");
                }
            }
        }

        // Check for duplicate national ID if being changed
        if (!string.IsNullOrEmpty(request.NationalId))
        {
            if (!string.Equals(investor.NationalId, request.NationalId, StringComparison.OrdinalIgnoreCase))
            {
                if (await _investorRepository.NationalIdExistsAsync(request.NationalId, id, ct))
                {
                    return Result<InvestorDto>.Conflict("An investor with this national ID already exists");
                }
            }
        }

        investor.FullName = request.FullName;
        investor.Phone = request.Phone;
        investor.Email = request.Email?.ToLowerInvariant();
        investor.NationalId = request.NationalId;
        investor.StatusId = request.StatusId;

        _investorRepository.Update(investor);

        _auditLogger.Log(AuditActions.InvestorUpdated, nameof(Investor), investor.Id, investor.Id.ToString(), $"Investor {investor.FullName} updated");

        await _unitOfWork.SaveChangesAsync(ct);

        // Reload with status and documents
        var updatedInvestor = await _investorRepository.GetByIdWithDocumentsAsync(investor.Id, ct);
        return Result<InvestorDto>.Success(MapToDto(updatedInvestor!));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var investor = await _investorRepository.GetByIdAsync(id, ct);
        if (investor == null)
        {
            return Result.NotFound("Investor not found");
        }

        _investorRepository.Delete(investor);

        _auditLogger.Log(AuditActions.InvestorDeleted, nameof(Investor), investor.Id, investor.Id.ToString(), $"Investor {investor.FullName} deleted");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result<IReadOnlyList<InvestorDocumentDto>>> GetDocumentsAsync(int investorId, CancellationToken ct = default)
    {
        var investor = await _investorRepository.GetByIdAsync(investorId, ct);
        if (investor == null)
        {
            return Result<IReadOnlyList<InvestorDocumentDto>>.NotFound("Investor not found");
        }

        var documents = await _documentRepository.GetByInvestorIdAsync(investorId, ct);
        var dtos = documents.Select(MapDocumentToDto).ToList();

        return Result<IReadOnlyList<InvestorDocumentDto>>.Success(dtos);
    }

    public async Task<Result<InvestorDocumentDto>> AddDocumentAsync(int investorId, CreateInvestorDocumentRequest request, CancellationToken ct = default)
    {
        var investor = await _investorRepository.GetByIdAsync(investorId, ct);
        if (investor == null)
        {
            return Result<InvestorDocumentDto>.NotFound("Investor not found");
        }

        var document = new InvestorDocument
        {
            InvestorId = investorId,
            DocumentTypeId = request.DocumentTypeId,
            FilePath = request.FilePath,
            ExpiresAt = request.ExpiresAt
        };

        await _documentRepository.AddAsync(document, ct);

        _auditLogger.Log(AuditActions.InvestorDocumentAdded, nameof(InvestorDocument), document.Id, investorId.ToString(), $"Document added to investor {investor.FullName}");

        await _unitOfWork.SaveChangesAsync(ct);

        // Reload with document type
        var createdDocument = await _documentRepository.GetByIdAsync(document.Id, ct);
        return Result<InvestorDocumentDto>.Success(MapDocumentToDto(createdDocument!));
    }

    public async Task<Result> DeleteDocumentAsync(int investorId, int documentId, CancellationToken ct = default)
    {
        var investor = await _investorRepository.GetByIdAsync(investorId, ct);
        if (investor == null)
        {
            return Result.NotFound("Investor not found");
        }

        var document = await _documentRepository.GetByIdAsync(documentId, ct);
        if (document == null || document.InvestorId != investorId)
        {
            return Result.NotFound("Document not found");
        }

        _documentRepository.Delete(document);

        _auditLogger.Log(AuditActions.InvestorDocumentDeleted, nameof(InvestorDocument), document.Id, investorId.ToString(), $"Document removed from investor {investor.FullName}");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    private static InvestorDto MapToDto(Investor investor)
    {
        return new InvestorDto(
            investor.Id,
            investor.FullName,
            investor.Phone,
            investor.Email,
            investor.NationalId,
            investor.StatusId,
            investor.Status.Name,
            investor.AccountId,
            investor.CreatedAt,
            investor.Documents.Count
        );
    }

    private static InvestorListDto MapToListDto(Investor investor)
    {
        return new InvestorListDto(
            investor.Id,
            investor.FullName,
            investor.Phone,
            investor.Email,
            investor.Status.Name,
            investor.Documents.Count
        );
    }

    private static InvestorDocumentDto MapDocumentToDto(InvestorDocument document)
    {
        return new InvestorDocumentDto(
            document.Id,
            document.InvestorId,
            document.DocumentTypeId,
            document.DocumentType.Name,
            document.FilePath,
            document.ExpiresAt,
            document.IsExpired,
            document.CreatedAt
        );
    }
}
