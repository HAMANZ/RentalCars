using FleetErp.Api.Common;
using FleetErp.Application.Insurance.Dtos;
using FleetErp.Application.Insurance.Interfaces;
using FleetErp.Infrastructure.Authorization;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FleetErp.Api.Controllers;

public class InsuranceController : ApiControllerBase
{
    private readonly IInsuranceService _insuranceService;

    public InsuranceController(IInsuranceService insuranceService)
    {
        _insuranceService = insuranceService;
    }

    #region Insurance Records

    /// <summary>
    /// Get paginated list of insurance records
    /// </summary>
    [HttpGet]
    [HasPermission(Permissions.Insurance.View)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        [FromQuery] int? vehicleId = null,
        [FromQuery] int? statusId = null,
        [FromQuery] int? insuranceTypeId = null,
        [FromQuery] int? companyId = null,
        CancellationToken ct = default)
    {
        var result = await _insuranceService.GetPagedAsync(page, pageSize, search, vehicleId, statusId, insuranceTypeId, companyId, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get insurance record by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [HasPermission(Permissions.Insurance.View)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _insuranceService.GetByIdAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get insurance records for a vehicle
    /// </summary>
    [HttpGet("vehicle/{vehicleId:int}")]
    [HasPermission(Permissions.Insurance.View)]
    public async Task<IActionResult> GetByVehicle(int vehicleId, CancellationToken ct)
    {
        var result = await _insuranceService.GetByVehicleIdAsync(vehicleId, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get insurance records expiring within specified days
    /// </summary>
    [HttpGet("expiring/{days:int}")]
    [HasPermission(Permissions.Insurance.View)]
    public async Task<IActionResult> GetExpiring(int days, CancellationToken ct)
    {
        var result = await _insuranceService.GetExpiringAsync(days, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Create a new insurance record
    /// </summary>
    [HttpPost]
    [HasPermission(Permissions.Insurance.Create)]
    public async Task<IActionResult> Create([FromBody] CreateInsuranceRequest request, CancellationToken ct)
    {
        var result = await _insuranceService.CreateAsync(request, ct);
        return result.ToCreatedResult(this, nameof(GetById), new { id = result.Value?.Id });
    }

    /// <summary>
    /// Update an existing insurance record
    /// </summary>
    [HttpPut("{id:int}")]
    [HasPermission(Permissions.Insurance.Edit)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInsuranceRequest request, CancellationToken ct)
    {
        var result = await _insuranceService.UpdateAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete an insurance record (soft delete)
    /// </summary>
    [HttpDelete("{id:int}")]
    [HasPermission(Permissions.Insurance.Edit)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _insuranceService.DeleteAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Renew an insurance record
    /// </summary>
    [HttpPost("{id:int}/renew")]
    [HasPermission(Permissions.Insurance.Edit)]
    public async Task<IActionResult> Renew(int id, [FromBody] RenewInsuranceRequest request, CancellationToken ct)
    {
        var result = await _insuranceService.RenewAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Cancel an insurance record
    /// </summary>
    [HttpPost("{id:int}/cancel")]
    [HasPermission(Permissions.Insurance.Edit)]
    public async Task<IActionResult> Cancel(int id, CancellationToken ct)
    {
        var result = await _insuranceService.CancelAsync(id, ct);
        return result.ToActionResult(this);
    }

    #endregion

    #region Insurance Companies

    /// <summary>
    /// Get paginated list of insurance companies
    /// </summary>
    [HttpGet("companies")]
    [HasPermission(Permissions.Insurance.View)]
    public async Task<IActionResult> GetCompanies(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        [FromQuery] bool? activeOnly = null,
        CancellationToken ct = default)
    {
        var result = await _insuranceService.GetCompaniesPagedAsync(page, pageSize, search, activeOnly, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get all active insurance companies (for dropdowns)
    /// </summary>
    [HttpGet("companies/active")]
    [HasPermission(Permissions.Insurance.View)]
    public async Task<IActionResult> GetActiveCompanies(CancellationToken ct)
    {
        var result = await _insuranceService.GetAllActiveCompaniesAsync(ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get insurance company by ID
    /// </summary>
    [HttpGet("companies/{id:int}")]
    [HasPermission(Permissions.Insurance.View)]
    public async Task<IActionResult> GetCompanyById(int id, CancellationToken ct)
    {
        var result = await _insuranceService.GetCompanyByIdAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Create a new insurance company
    /// </summary>
    [HttpPost("companies")]
    [HasPermission(Permissions.Insurance.Create)]
    public async Task<IActionResult> CreateCompany([FromBody] CreateInsuranceCompanyRequest request, CancellationToken ct)
    {
        var result = await _insuranceService.CreateCompanyAsync(request, ct);
        return result.ToCreatedResult(this, nameof(GetCompanyById), new { id = result.Value?.Id });
    }

    /// <summary>
    /// Update an existing insurance company
    /// </summary>
    [HttpPut("companies/{id:int}")]
    [HasPermission(Permissions.Insurance.Edit)]
    public async Task<IActionResult> UpdateCompany(int id, [FromBody] UpdateInsuranceCompanyRequest request, CancellationToken ct)
    {
        var result = await _insuranceService.UpdateCompanyAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete an insurance company (soft delete)
    /// </summary>
    [HttpDelete("companies/{id:int}")]
    [HasPermission(Permissions.Insurance.Edit)]
    public async Task<IActionResult> DeleteCompany(int id, CancellationToken ct)
    {
        var result = await _insuranceService.DeleteCompanyAsync(id, ct);
        return result.ToActionResult(this);
    }

    #endregion
}
