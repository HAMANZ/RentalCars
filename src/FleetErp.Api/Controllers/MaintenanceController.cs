using FleetErp.Api.Common;
using FleetErp.Application.Maintenance.Dtos;
using FleetErp.Application.Maintenance.Interfaces;
using FleetErp.Infrastructure.Authorization;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FleetErp.Api.Controllers;

public class MaintenanceController : ApiControllerBase
{
    private readonly IMaintenanceService _maintenanceService;

    public MaintenanceController(IMaintenanceService maintenanceService)
    {
        _maintenanceService = maintenanceService;
    }

    /// <summary>
    /// Get paginated list of maintenance records
    /// </summary>
    [HttpGet]
    [HasPermission(Permissions.Maintenance.View)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        [FromQuery] int? vehicleId = null,
        [FromQuery] int? statusId = null,
        [FromQuery] int? maintenanceTypeId = null,
        CancellationToken ct = default)
    {
        var result = await _maintenanceService.GetPagedAsync(page, pageSize, search, vehicleId, statusId, maintenanceTypeId, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get maintenance record by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [HasPermission(Permissions.Maintenance.View)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _maintenanceService.GetByIdAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get maintenance records for a vehicle
    /// </summary>
    [HttpGet("vehicle/{vehicleId:int}")]
    [HasPermission(Permissions.Maintenance.View)]
    public async Task<IActionResult> GetByVehicle(int vehicleId, CancellationToken ct)
    {
        var result = await _maintenanceService.GetByVehicleIdAsync(vehicleId, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Create a new maintenance record
    /// </summary>
    [HttpPost]
    [HasPermission(Permissions.Maintenance.Create)]
    public async Task<IActionResult> Create([FromBody] CreateMaintenanceRequest request, CancellationToken ct)
    {
        var result = await _maintenanceService.CreateAsync(request, ct);
        return result.ToCreatedResult(this, nameof(GetById), new { id = result.Value?.Id });
    }

    /// <summary>
    /// Update an existing maintenance record
    /// </summary>
    [HttpPut("{id:int}")]
    [HasPermission(Permissions.Maintenance.Edit)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMaintenanceRequest request, CancellationToken ct)
    {
        var result = await _maintenanceService.UpdateAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete a maintenance record (soft delete)
    /// </summary>
    [HttpDelete("{id:int}")]
    [HasPermission(Permissions.Maintenance.Edit)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _maintenanceService.DeleteAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Start a scheduled maintenance
    /// </summary>
    [HttpPost("{id:int}/start")]
    [HasPermission(Permissions.Maintenance.Edit)]
    public async Task<IActionResult> Start(int id, CancellationToken ct)
    {
        var result = await _maintenanceService.StartAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Complete a maintenance record
    /// </summary>
    [HttpPost("{id:int}/complete")]
    [HasPermission(Permissions.Maintenance.Edit)]
    public async Task<IActionResult> Complete(int id, [FromBody] CompleteMaintenanceRequest request, CancellationToken ct)
    {
        var result = await _maintenanceService.CompleteAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Cancel a maintenance record
    /// </summary>
    [HttpPost("{id:int}/cancel")]
    [HasPermission(Permissions.Maintenance.Edit)]
    public async Task<IActionResult> Cancel(int id, CancellationToken ct)
    {
        var result = await _maintenanceService.CancelAsync(id, ct);
        return result.ToActionResult(this);
    }
}
