using FleetErp.Application.Investors.Dtos;
using FleetErp.Application.Investors.Interfaces;
using FleetErp.Application.Reports.Dtos;
using FleetErp.Application.Reports.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FleetErp.Api.Pages.Admin.Reports;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IReportService _reportService;
    private readonly IInvestorService _investorService;

    public IndexModel(IReportService reportService, IInvestorService investorService)
    {
        _reportService = reportService;
        _investorService = investorService;
    }

    [BindProperty(SupportsGet = true)]
    public string ReportType { get; set; } = "revenue";

    [BindProperty(SupportsGet = true)]
    public DateTime StartDate { get; set; } = DateTime.Today.AddMonths(-1);

    [BindProperty(SupportsGet = true)]
    public DateTime EndDate { get; set; } = DateTime.Today;

    [BindProperty(SupportsGet = true)]
    public int? InvestorId { get; set; }

    public RevenueReportDto? RevenueReport { get; set; }
    public ExpenseReportDto? ExpenseReport { get; set; }
    public VehicleUtilizationReportDto? UtilizationReport { get; set; }
    public InvestorReportDto? InvestorReport { get; set; }
    public MaintenanceReportDto? MaintenanceReport { get; set; }
    public InsuranceReportDto? InsuranceReport { get; set; }
    public AuditReportDto? AuditReport { get; set; }

    public SelectList InvestorOptions { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());

    public async Task OnGetAsync(CancellationToken ct)
    {
        await LoadInvestorsAsync(ct);
        await LoadReportAsync(ct);
    }

    private async Task LoadInvestorsAsync(CancellationToken ct)
    {
        var result = await _investorService.GetPagedAsync(1, 1000, null, ct);
        if (result.IsSuccess)
        {
            InvestorOptions = new SelectList(result.Value!.Items, "Id", "FullName");
        }
    }

    private async Task LoadReportAsync(CancellationToken ct)
    {
        switch (ReportType.ToLower())
        {
            case "revenue":
                var revenueResult = await _reportService.GetRevenueReportAsync(StartDate, EndDate, InvestorId, ct);
                if (revenueResult.IsSuccess)
                {
                    RevenueReport = revenueResult.Value;
                }
                break;

            case "expense":
                var expenseResult = await _reportService.GetExpenseReportAsync(StartDate, EndDate, InvestorId, ct);
                if (expenseResult.IsSuccess)
                {
                    ExpenseReport = expenseResult.Value;
                }
                break;

            case "utilization":
                var utilizationResult = await _reportService.GetVehicleUtilizationReportAsync(StartDate, EndDate, InvestorId, ct);
                if (utilizationResult.IsSuccess)
                {
                    UtilizationReport = utilizationResult.Value;
                }
                break;

            case "investor":
                var investorResult = await _reportService.GetInvestorReportAsync(StartDate, EndDate, ct);
                if (investorResult.IsSuccess)
                {
                    InvestorReport = investorResult.Value;
                }
                break;

            case "maintenance":
                var maintenanceResult = await _reportService.GetMaintenanceReportAsync(StartDate, EndDate, null, ct);
                if (maintenanceResult.IsSuccess)
                {
                    MaintenanceReport = maintenanceResult.Value;
                }
                break;

            case "insurance":
                var insuranceResult = await _reportService.GetInsuranceReportAsync(ct);
                if (insuranceResult.IsSuccess)
                {
                    InsuranceReport = insuranceResult.Value;
                }
                break;

            case "audit":
                var auditResult = await _reportService.GetAuditReportAsync(StartDate, EndDate, ct);
                if (auditResult.IsSuccess)
                {
                    AuditReport = auditResult.Value;
                }
                break;
        }
    }
}
