using FleetErp.Application.Audit.Interfaces;
using FleetErp.Application.Common;
using FleetErp.Application.Common.Interfaces;
using FleetErp.Application.Insurance.Dtos;
using FleetErp.Application.Insurance.Interfaces;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Domain.Entities.Insurance;
using FleetErp.Shared.Constants;

namespace FleetErp.Infrastructure.Services;

public class InsuranceService : IInsuranceService
{
    private readonly IInsuranceRepository _insuranceRepository;
    private readonly IInsuranceCompanyRepository _companyRepository;
    private readonly ILookupRepository _lookupRepository;
    private readonly IAuditLogger _auditLogger;
    private readonly IUnitOfWork _unitOfWork;

    public InsuranceService(
        IInsuranceRepository insuranceRepository,
        IInsuranceCompanyRepository companyRepository,
        ILookupRepository lookupRepository,
        IAuditLogger auditLogger,
        IUnitOfWork unitOfWork)
    {
        _insuranceRepository = insuranceRepository;
        _companyRepository = companyRepository;
        _lookupRepository = lookupRepository;
        _auditLogger = auditLogger;
        _unitOfWork = unitOfWork;
    }

    #region Insurance Records

    public async Task<Result<InsuranceRecordDto>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var record = await _insuranceRepository.GetByIdWithDocumentsAsync(id, ct);
        if (record is null)
        {
            return Result<InsuranceRecordDto>.NotFound("Insurance record not found");
        }

        return Result<InsuranceRecordDto>.Success(MapToDto(record));
    }

    public async Task<Result<PagedResult<InsuranceRecordListDto>>> GetPagedAsync(int page, int pageSize, string? search, int? vehicleId, int? statusId, int? insuranceTypeId, int? companyId, CancellationToken ct = default)
    {
        var result = await _insuranceRepository.GetPagedAsync(page, pageSize, search, vehicleId, statusId, insuranceTypeId, companyId, ct);

        var dtos = result.Items.Select(MapToListDto).ToList();
        return Result<PagedResult<InsuranceRecordListDto>>.Success(
            new PagedResult<InsuranceRecordListDto> { Items = dtos, Page = result.Page, PageSize = result.PageSize, TotalCount = result.TotalCount });
    }

    public async Task<Result<IReadOnlyList<InsuranceRecordListDto>>> GetByVehicleIdAsync(int vehicleId, CancellationToken ct = default)
    {
        var records = await _insuranceRepository.GetByVehicleIdAsync(vehicleId, ct);
        var dtos = records.Select(MapToListDto).ToList();
        return Result<IReadOnlyList<InsuranceRecordListDto>>.Success(dtos);
    }

    public async Task<Result<IReadOnlyList<InsuranceRecordListDto>>> GetExpiringAsync(int days, CancellationToken ct = default)
    {
        var records = await _insuranceRepository.GetExpiringAsync(days, ct);
        var dtos = records.Select(MapToListDto).ToList();
        return Result<IReadOnlyList<InsuranceRecordListDto>>.Success(dtos);
    }

    public async Task<Result<InsuranceRecordDto>> CreateAsync(CreateInsuranceRequest request, CancellationToken ct = default)
    {
        // Check if policy number already exists
        if (await _insuranceRepository.ExistsByPolicyNumberAsync(request.PolicyNumber, null, ct))
        {
            return Result<InsuranceRecordDto>.Invalid("A policy with this number already exists");
        }

        var record = new InsuranceRecord
        {
            VehicleId = request.VehicleId,
            InsuranceCompanyId = request.InsuranceCompanyId,
            InsuranceTypeId = request.InsuranceTypeId,
            StatusId = request.StatusId,
            PolicyNumber = request.PolicyNumber,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Premium = request.Premium,
            CoverageAmount = request.CoverageAmount,
            Deductible = request.Deductible,
            CoverageDetails = request.CoverageDetails,
            Notes = request.Notes,
            RenewalReminderSent = false
        };

        await _insuranceRepository.AddAsync(record, ct);
        _auditLogger.Log(AuditActions.InsuranceCreated, nameof(InsuranceRecord), record.Id,
            userId: null, $"Insurance policy {request.PolicyNumber} created for vehicle ID {request.VehicleId}");

        await _unitOfWork.SaveChangesAsync(ct);

        var created = await _insuranceRepository.GetByIdAsync(record.Id, ct);
        return Result<InsuranceRecordDto>.Success(MapToDto(created!));
    }

    public async Task<Result<InsuranceRecordDto>> UpdateAsync(int id, UpdateInsuranceRequest request, CancellationToken ct = default)
    {
        var record = await _insuranceRepository.GetByIdAsync(id, ct);
        if (record is null)
        {
            return Result<InsuranceRecordDto>.NotFound("Insurance record not found");
        }

        // Cannot update cancelled or expired records
        if (record.Status.Code == "CANCELLED" || record.Status.Code == "EXPIRED")
        {
            return Result<InsuranceRecordDto>.Invalid("Cannot update a cancelled or expired insurance record");
        }

        // Check if policy number already exists (excluding current record)
        if (await _insuranceRepository.ExistsByPolicyNumberAsync(request.PolicyNumber, id, ct))
        {
            return Result<InsuranceRecordDto>.Invalid("A policy with this number already exists");
        }

        record.InsuranceCompanyId = request.InsuranceCompanyId;
        record.InsuranceTypeId = request.InsuranceTypeId;
        record.StatusId = request.StatusId;
        record.PolicyNumber = request.PolicyNumber;
        record.StartDate = request.StartDate;
        record.EndDate = request.EndDate;
        record.Premium = request.Premium;
        record.CoverageAmount = request.CoverageAmount;
        record.Deductible = request.Deductible;
        record.CoverageDetails = request.CoverageDetails;
        record.Notes = request.Notes;

        _insuranceRepository.Update(record);
        _auditLogger.Log(AuditActions.InsuranceUpdated, nameof(InsuranceRecord), record.Id,
            userId: null, $"Insurance policy {record.PolicyNumber} updated");

        await _unitOfWork.SaveChangesAsync(ct);

        var updated = await _insuranceRepository.GetByIdAsync(record.Id, ct);
        return Result<InsuranceRecordDto>.Success(MapToDto(updated!));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var record = await _insuranceRepository.GetByIdAsync(id, ct);
        if (record is null)
        {
            return Result.NotFound("Insurance record not found");
        }

        _insuranceRepository.Delete(record);
        _auditLogger.Log(AuditActions.InsuranceDeleted, nameof(InsuranceRecord), record.Id,
            userId: null, $"Insurance policy {record.PolicyNumber} deleted");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result<InsuranceRecordDto>> RenewAsync(int id, RenewInsuranceRequest request, CancellationToken ct = default)
    {
        var record = await _insuranceRepository.GetByIdAsync(id, ct);
        if (record is null)
        {
            return Result<InsuranceRecordDto>.NotFound("Insurance record not found");
        }

        if (record.Status.Code == "CANCELLED")
        {
            return Result<InsuranceRecordDto>.Invalid("Cannot renew a cancelled insurance record");
        }

        // Get active status
        var activeStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.InsuranceStatus, "ACTIVE", ct);
        if (activeStatus is null)
        {
            return Result<InsuranceRecordDto>.Invalid("Insurance status 'ACTIVE' not found");
        }

        // Get expired status for the old record
        var expiredStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.InsuranceStatus, "EXPIRED", ct);
        if (expiredStatus is null)
        {
            return Result<InsuranceRecordDto>.Invalid("Insurance status 'EXPIRED' not found");
        }

        // Mark the old record as expired
        record.StatusId = expiredStatus.Id;
        _insuranceRepository.Update(record);

        // Create a new renewal record
        var newRecord = new InsuranceRecord
        {
            VehicleId = record.VehicleId,
            InsuranceCompanyId = record.InsuranceCompanyId,
            InsuranceTypeId = record.InsuranceTypeId,
            StatusId = activeStatus.Id,
            PolicyNumber = record.PolicyNumber, // Keep the same policy number
            StartDate = request.NewStartDate,
            EndDate = request.NewEndDate,
            Premium = request.NewPremium,
            CoverageAmount = request.NewCoverageAmount ?? record.CoverageAmount,
            Deductible = request.NewDeductible ?? record.Deductible,
            CoverageDetails = record.CoverageDetails,
            Notes = request.Notes,
            RenewalReminderSent = false
        };

        await _insuranceRepository.AddAsync(newRecord, ct);
        _auditLogger.Log(AuditActions.InsuranceRenewed, nameof(InsuranceRecord), record.Id,
            userId: null, $"Insurance policy {record.PolicyNumber} renewed. Old record ID: {record.Id}, New record ID: {newRecord.Id}");

        await _unitOfWork.SaveChangesAsync(ct);

        var created = await _insuranceRepository.GetByIdAsync(newRecord.Id, ct);
        return Result<InsuranceRecordDto>.Success(MapToDto(created!));
    }

    public async Task<Result<InsuranceRecordDto>> CancelAsync(int id, CancellationToken ct = default)
    {
        var record = await _insuranceRepository.GetByIdAsync(id, ct);
        if (record is null)
        {
            return Result<InsuranceRecordDto>.NotFound("Insurance record not found");
        }

        if (record.Status.Code == "CANCELLED" || record.Status.Code == "EXPIRED")
        {
            return Result<InsuranceRecordDto>.Invalid("Insurance is already cancelled or expired");
        }

        // Get cancelled status
        var cancelledStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.InsuranceStatus, "CANCELLED", ct);
        if (cancelledStatus is null)
        {
            return Result<InsuranceRecordDto>.Invalid("Insurance status 'CANCELLED' not found");
        }

        record.StatusId = cancelledStatus.Id;

        _insuranceRepository.Update(record);
        _auditLogger.Log(AuditActions.InsuranceCancelled, nameof(InsuranceRecord), record.Id,
            userId: null, $"Insurance policy {record.PolicyNumber} cancelled");

        await _unitOfWork.SaveChangesAsync(ct);

        var updated = await _insuranceRepository.GetByIdAsync(record.Id, ct);
        return Result<InsuranceRecordDto>.Success(MapToDto(updated!));
    }

    #endregion

    #region Insurance Companies

    public async Task<Result<InsuranceCompanyDto>> GetCompanyByIdAsync(int id, CancellationToken ct = default)
    {
        var company = await _companyRepository.GetByIdAsync(id, ct);
        if (company is null)
        {
            return Result<InsuranceCompanyDto>.NotFound("Insurance company not found");
        }

        return Result<InsuranceCompanyDto>.Success(MapToCompanyDto(company));
    }

    public async Task<Result<PagedResult<InsuranceCompanyDto>>> GetCompaniesPagedAsync(int page, int pageSize, string? search, bool? activeOnly, CancellationToken ct = default)
    {
        var result = await _companyRepository.GetPagedAsync(page, pageSize, search, activeOnly, ct);

        var dtos = result.Items.Select(MapToCompanyDto).ToList();
        return Result<PagedResult<InsuranceCompanyDto>>.Success(
            new PagedResult<InsuranceCompanyDto> { Items = dtos, Page = result.Page, PageSize = result.PageSize, TotalCount = result.TotalCount });
    }

    public async Task<Result<IReadOnlyList<InsuranceCompanyDto>>> GetAllCompaniesAsync(CancellationToken ct = default)
    {
        var companies = await _companyRepository.GetAllAsync(ct);
        var dtos = companies.Select(MapToCompanyDto).ToList();
        return Result<IReadOnlyList<InsuranceCompanyDto>>.Success(dtos);
    }

    public async Task<Result<IReadOnlyList<InsuranceCompanyDto>>> GetAllActiveCompaniesAsync(CancellationToken ct = default)
    {
        var companies = await _companyRepository.GetAllActiveAsync(ct);
        var dtos = companies.Select(MapToCompanyDto).ToList();
        return Result<IReadOnlyList<InsuranceCompanyDto>>.Success(dtos);
    }

    public async Task<Result<InsuranceCompanyDto>> CreateCompanyAsync(CreateInsuranceCompanyRequest request, CancellationToken ct = default)
    {
        // Check if name already exists
        if (await _companyRepository.ExistsByNameAsync(request.Name, null, ct))
        {
            return Result<InsuranceCompanyDto>.Invalid("An insurance company with this name already exists");
        }

        var company = new InsuranceCompany
        {
            Name = request.Name,
            Phone = request.Phone,
            Email = request.Email,
            Address = request.Address,
            ContactPerson = request.ContactPerson,
            IsActive = request.IsActive
        };

        await _companyRepository.AddAsync(company, ct);
        _auditLogger.Log(AuditActions.InsuranceCompanyCreated, nameof(InsuranceCompany), company.Id,
            userId: null, $"Insurance company '{request.Name}' created");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result<InsuranceCompanyDto>.Success(MapToCompanyDto(company));
    }

    public async Task<Result<InsuranceCompanyDto>> UpdateCompanyAsync(int id, UpdateInsuranceCompanyRequest request, CancellationToken ct = default)
    {
        var company = await _companyRepository.GetByIdAsync(id, ct);
        if (company is null)
        {
            return Result<InsuranceCompanyDto>.NotFound("Insurance company not found");
        }

        // Check if name already exists (excluding current company)
        if (await _companyRepository.ExistsByNameAsync(request.Name, id, ct))
        {
            return Result<InsuranceCompanyDto>.Invalid("An insurance company with this name already exists");
        }

        company.Name = request.Name;
        company.Phone = request.Phone;
        company.Email = request.Email;
        company.Address = request.Address;
        company.ContactPerson = request.ContactPerson;
        company.IsActive = request.IsActive;

        _companyRepository.Update(company);
        _auditLogger.Log(AuditActions.InsuranceCompanyUpdated, nameof(InsuranceCompany), company.Id,
            userId: null, $"Insurance company '{company.Name}' updated");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result<InsuranceCompanyDto>.Success(MapToCompanyDto(company));
    }

    public async Task<Result> DeleteCompanyAsync(int id, CancellationToken ct = default)
    {
        var company = await _companyRepository.GetByIdAsync(id, ct);
        if (company is null)
        {
            return Result.NotFound("Insurance company not found");
        }

        _companyRepository.Delete(company);
        _auditLogger.Log(AuditActions.InsuranceCompanyDeleted, nameof(InsuranceCompany), company.Id,
            userId: null, $"Insurance company '{company.Name}' deleted");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    #endregion

    #region Mapping Methods

    private static InsuranceRecordDto MapToDto(InsuranceRecord record)
    {
        return new InsuranceRecordDto(
            record.Id,
            record.VehicleId,
            record.Vehicle.PlateNumber,
            $"{record.Vehicle.Make} {record.Vehicle.Model} ({record.Vehicle.Year})",
            record.InsuranceCompanyId,
            record.InsuranceCompany.Name,
            record.InsuranceTypeId,
            record.InsuranceType.Name,
            record.StatusId,
            record.Status.Name,
            record.PolicyNumber,
            record.StartDate,
            record.EndDate,
            record.Premium,
            record.CoverageAmount,
            record.Deductible,
            record.CoverageDetails,
            record.Notes,
            record.RenewalReminderSent,
            record.CreatedAt,
            record.UpdatedAt,
            record.Documents.Count
        );
    }

    private static InsuranceRecordListDto MapToListDto(InsuranceRecord record)
    {
        var daysUntilExpiry = (record.EndDate.Date - DateTime.UtcNow.Date).Days;

        return new InsuranceRecordListDto(
            record.Id,
            record.Vehicle.PlateNumber,
            $"{record.Vehicle.Make} {record.Vehicle.Model}",
            record.InsuranceCompany.Name,
            record.InsuranceType.Name,
            record.Status.Name,
            record.PolicyNumber,
            record.StartDate,
            record.EndDate,
            record.Premium,
            daysUntilExpiry
        );
    }

    private static InsuranceCompanyDto MapToCompanyDto(InsuranceCompany company)
    {
        return new InsuranceCompanyDto(
            company.Id,
            company.Name,
            company.Phone,
            company.Email,
            company.Address,
            company.ContactPerson,
            company.IsActive,
            company.CreatedAt,
            company.UpdatedAt
        );
    }

    #endregion
}
