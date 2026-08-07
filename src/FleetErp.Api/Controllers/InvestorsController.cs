using FleetErp.Api.Common;
using FleetErp.Application.Investors.Dtos;
using FleetErp.Application.Investors.Interfaces;
using FleetErp.Infrastructure.Authorization;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetErp.Api.Controllers;

public class InvestorsController : ApiControllerBase
{
    private readonly IInvestorService _investorService;

    public InvestorsController(IInvestorService investorService)
    {
        _investorService = investorService;
    }

    /// <summary>
    /// Get paginated list of investors
    /// </summary>
    [HttpGet]
    [HasPermission(Permissions.Investors.View)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        CancellationToken ct = default)
    {
        var result = await _investorService.GetPagedAsync(page, pageSize, search, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get investor by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [HasPermission(Permissions.Investors.View)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _investorService.GetByIdAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Create a new investor
    /// </summary>
    [HttpPost]
    [HasPermission(Permissions.Investors.Create)]
    public async Task<IActionResult> Create([FromBody] CreateInvestorRequest request, CancellationToken ct)
    {
        var result = await _investorService.CreateAsync(request, ct);
        return result.ToCreatedResult(this, nameof(GetById), new { id = result.Value?.Id });
    }

    /// <summary>
    /// Update an existing investor
    /// </summary>
    [HttpPut("{id:int}")]
    [HasPermission(Permissions.Investors.Edit)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInvestorRequest request, CancellationToken ct)
    {
        var result = await _investorService.UpdateAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete an investor (soft delete)
    /// </summary>
    [HttpDelete("{id:int}")]
    [HasPermission(Permissions.Investors.Delete)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _investorService.DeleteAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get documents for an investor
    /// </summary>
    [HttpGet("{id:int}/documents")]
    [HasPermission(Permissions.Investors.View)]
    public async Task<IActionResult> GetDocuments(int id, CancellationToken ct)
    {
        var result = await _investorService.GetDocumentsAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Add a document to an investor
    /// </summary>
    [HttpPost("{id:int}/documents")]
    [HasPermission(Permissions.Investors.Edit)]
    public async Task<IActionResult> AddDocument(int id, [FromBody] CreateInvestorDocumentRequest request, CancellationToken ct)
    {
        var result = await _investorService.AddDocumentAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete a document from an investor
    /// </summary>
    [HttpDelete("{id:int}/documents/{documentId:int}")]
    [HasPermission(Permissions.Investors.Edit)]
    public async Task<IActionResult> DeleteDocument(int id, int documentId, CancellationToken ct)
    {
        var result = await _investorService.DeleteDocumentAsync(id, documentId, ct);
        return result.ToActionResult(this);
    }
}
