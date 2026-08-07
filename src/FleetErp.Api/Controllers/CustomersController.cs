using FleetErp.Api.Common;
using FleetErp.Application.Customers.Dtos;
using FleetErp.Application.Customers.Interfaces;
using FleetErp.Infrastructure.Authorization;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FleetErp.Api.Controllers;

public class CustomersController : ApiControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>
    /// Get paginated list of customers
    /// </summary>
    [HttpGet]
    [HasPermission(Permissions.Customers.View)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        CancellationToken ct = default)
    {
        var result = await _customerService.GetPagedAsync(page, pageSize, search, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get customer by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [HasPermission(Permissions.Customers.View)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _customerService.GetByIdAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Create a new customer
    /// </summary>
    [HttpPost]
    [HasPermission(Permissions.Customers.Create)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request, CancellationToken ct)
    {
        var result = await _customerService.CreateAsync(request, ct);
        return result.ToCreatedResult(this, nameof(GetById), new { id = result.Value?.Id });
    }

    /// <summary>
    /// Update an existing customer
    /// </summary>
    [HttpPut("{id:int}")]
    [HasPermission(Permissions.Customers.Edit)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerRequest request, CancellationToken ct)
    {
        var result = await _customerService.UpdateAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete a customer (soft delete)
    /// </summary>
    [HttpDelete("{id:int}")]
    [HasPermission(Permissions.Customers.Delete)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _customerService.DeleteAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get documents for a customer
    /// </summary>
    [HttpGet("{id:int}/documents")]
    [HasPermission(Permissions.Customers.View)]
    public async Task<IActionResult> GetDocuments(int id, CancellationToken ct)
    {
        var result = await _customerService.GetDocumentsAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Add a document to a customer
    /// </summary>
    [HttpPost("{id:int}/documents")]
    [HasPermission(Permissions.Customers.Edit)]
    public async Task<IActionResult> AddDocument(int id, [FromBody] CreateCustomerDocumentRequest request, CancellationToken ct)
    {
        var result = await _customerService.AddDocumentAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete a document from a customer
    /// </summary>
    [HttpDelete("{id:int}/documents/{documentId:int}")]
    [HasPermission(Permissions.Customers.Edit)]
    public async Task<IActionResult> DeleteDocument(int id, int documentId, CancellationToken ct)
    {
        var result = await _customerService.DeleteDocumentAsync(id, documentId, ct);
        return result.ToActionResult(this);
    }
}
