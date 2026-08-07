using FleetErp.Application.Common;
using FleetErp.Application.Insurance.Dtos;

namespace FleetErp.Application.Insurance.Interfaces;

public interface IInsuranceService
{
    // Insurance Records
    Task<Result<InsuranceRecordDto>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<PagedResult<InsuranceRecordListDto>>> GetPagedAsync(int page, int pageSize, string? search, int? vehicleId, int? statusId, int? insuranceTypeId, int? companyId, CancellationToken ct = default);
    Task<Result<IReadOnlyList<InsuranceRecordListDto>>> GetByVehicleIdAsync(int vehicleId, CancellationToken ct = default);
    Task<Result<IReadOnlyList<InsuranceRecordListDto>>> GetExpiringAsync(int days, CancellationToken ct = default);
    Task<Result<InsuranceRecordDto>> CreateAsync(CreateInsuranceRequest request, CancellationToken ct = default);
    Task<Result<InsuranceRecordDto>> UpdateAsync(int id, UpdateInsuranceRequest request, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);
    Task<Result<InsuranceRecordDto>> RenewAsync(int id, RenewInsuranceRequest request, CancellationToken ct = default);
    Task<Result<InsuranceRecordDto>> CancelAsync(int id, CancellationToken ct = default);

    // Insurance Companies
    Task<Result<InsuranceCompanyDto>> GetCompanyByIdAsync(int id, CancellationToken ct = default);
    Task<Result<PagedResult<InsuranceCompanyDto>>> GetCompaniesPagedAsync(int page, int pageSize, string? search, bool? activeOnly, CancellationToken ct = default);
    Task<Result<IReadOnlyList<InsuranceCompanyDto>>> GetAllCompaniesAsync(CancellationToken ct = default);
    Task<Result<IReadOnlyList<InsuranceCompanyDto>>> GetAllActiveCompaniesAsync(CancellationToken ct = default);
    Task<Result<InsuranceCompanyDto>> CreateCompanyAsync(CreateInsuranceCompanyRequest request, CancellationToken ct = default);
    Task<Result<InsuranceCompanyDto>> UpdateCompanyAsync(int id, UpdateInsuranceCompanyRequest request, CancellationToken ct = default);
    Task<Result> DeleteCompanyAsync(int id, CancellationToken ct = default);
}
