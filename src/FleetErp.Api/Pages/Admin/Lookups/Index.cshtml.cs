using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Domain.Entities.Lookups;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Lookups;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ILookupRepository _lookupRepository;

    public IndexModel(ILookupRepository lookupRepository)
    {
        _lookupRepository = lookupRepository;
    }

    [BindProperty(SupportsGet = true)]
    public string? SelectedType { get; set; }

    public IReadOnlyList<string> AvailableLookupTypes { get; set; } = [];

    public IReadOnlyList<LookupItem> LookupItems { get; set; } = [];

    public async Task OnGetAsync(string? type, CancellationToken ct)
    {
        // Get all lookup types
        AvailableLookupTypes = new List<string>
        {
            FleetErp.Shared.Constants.LookupTypes.InvestorStatus,
            FleetErp.Shared.Constants.LookupTypes.DocumentType,
            FleetErp.Shared.Constants.LookupTypes.VehicleStatus,
            FleetErp.Shared.Constants.LookupTypes.DriverStatus,
            FleetErp.Shared.Constants.LookupTypes.ContractStatus,
            FleetErp.Shared.Constants.LookupTypes.PaymentMethod,
            FleetErp.Shared.Constants.LookupTypes.MaintenanceType,
            FleetErp.Shared.Constants.LookupTypes.InsuranceType,
        };

        SelectedType = type;

        if (!string.IsNullOrEmpty(type))
        {
            LookupItems = await _lookupRepository.GetByTypeAsync(type, activeOnly: false, ct);
        }
    }

    public async Task<IActionResult> OnPostAddAsync(
        string lookupType,
        string code,
        string name,
        int sortOrder,
        bool isActive,
        CancellationToken ct)
    {
        var item = new LookupItem
        {
            LookupType = lookupType,
            Code = code.ToUpperInvariant().Replace(" ", "_"),
            Name = name,
            SortOrder = sortOrder,
            IsActive = isActive,
            IsSystem = false
        };

        await _lookupRepository.AddAsync(item, ct);
        await _lookupRepository.SaveChangesAsync(ct);

        TempData["SuccessMessage"] = $"Lookup item '{name}' added successfully.";
        return RedirectToPage(new { type = lookupType });
    }

    public async Task<IActionResult> OnPostEditAsync(
        int id,
        string lookupType,
        string name,
        int sortOrder,
        bool isActive,
        CancellationToken ct)
    {
        var item = await _lookupRepository.GetByIdAsync(id, ct);

        if (item == null)
        {
            TempData["ErrorMessage"] = "Lookup item not found.";
            return RedirectToPage(new { type = lookupType });
        }

        if (item.IsSystem)
        {
            TempData["ErrorMessage"] = "System lookup items cannot be modified.";
            return RedirectToPage(new { type = lookupType });
        }

        item.Name = name;
        item.SortOrder = sortOrder;
        item.IsActive = isActive;

        _lookupRepository.Update(item);
        await _lookupRepository.SaveChangesAsync(ct);

        TempData["SuccessMessage"] = $"Lookup item '{name}' updated successfully.";
        return RedirectToPage(new { type = lookupType });
    }
}
