using FleetErp.Api.Common;
using FleetErp.Application.Vehicles.Dtos;
using FleetErp.Application.Vehicles.Interfaces;
using FleetErp.Infrastructure.Authorization;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FleetErp.Api.Controllers;

public class VehiclesController : ApiControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    /// <summary>
    /// Get paginated list of vehicles
    /// </summary>
    [HttpGet]
    [HasPermission(Permissions.Vehicles.View)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        [FromQuery] int? investorId = null,
        [FromQuery] int? statusId = null,
        [FromQuery] int? vehicleTypeId = null,
        CancellationToken ct = default)
    {
        var result = await _vehicleService.GetPagedAsync(page, pageSize, search, investorId, statusId, vehicleTypeId, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get vehicle by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [HasPermission(Permissions.Vehicles.View)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _vehicleService.GetByIdAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Create a new vehicle
    /// </summary>
    [HttpPost]
    [HasPermission(Permissions.Vehicles.Create)]
    public async Task<IActionResult> Create([FromBody] CreateVehicleRequest request, CancellationToken ct)
    {
        var result = await _vehicleService.CreateAsync(request, ct);
        return result.ToCreatedResult(this, nameof(GetById), new { id = result.Value?.Id });
    }

    /// <summary>
    /// Update an existing vehicle
    /// </summary>
    [HttpPut("{id:int}")]
    [HasPermission(Permissions.Vehicles.Edit)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateVehicleRequest request, CancellationToken ct)
    {
        var result = await _vehicleService.UpdateAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete a vehicle (soft delete)
    /// </summary>
    [HttpDelete("{id:int}")]
    [HasPermission(Permissions.Vehicles.Delete)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _vehicleService.DeleteAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get documents for a vehicle
    /// </summary>
    [HttpGet("{id:int}/documents")]
    [HasPermission(Permissions.Vehicles.View)]
    public async Task<IActionResult> GetDocuments(int id, CancellationToken ct)
    {
        var result = await _vehicleService.GetDocumentsAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Add a document to a vehicle
    /// </summary>
    [HttpPost("{id:int}/documents")]
    [HasPermission(Permissions.Vehicles.Edit)]
    public async Task<IActionResult> AddDocument(int id, [FromBody] CreateVehicleDocumentRequest request, CancellationToken ct)
    {
        var result = await _vehicleService.AddDocumentAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete a document from a vehicle
    /// </summary>
    [HttpDelete("{id:int}/documents/{documentId:int}")]
    [HasPermission(Permissions.Vehicles.Edit)]
    public async Task<IActionResult> DeleteDocument(int id, int documentId, CancellationToken ct)
    {
        var result = await _vehicleService.DeleteDocumentAsync(id, documentId, ct);
        return result.ToActionResult(this);
    }
}
