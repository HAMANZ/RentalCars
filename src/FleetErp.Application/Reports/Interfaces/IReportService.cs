using FleetErp.Application.Common;
using FleetErp.Application.Reports.Dtos;

namespace FleetErp.Application.Reports.Interfaces;

public interface IReportService
{
    Task<Result<RevenueReportDto>> GetRevenueReportAsync(DateTime startDate, DateTime endDate, int? investorId = null, CancellationToken ct = default);
    Task<Result<ExpenseReportDto>> GetExpenseReportAsync(DateTime startDate, DateTime endDate, int? investorId = null, CancellationToken ct = default);
    Task<Result<VehicleUtilizationReportDto>> GetVehicleUtilizationReportAsync(DateTime startDate, DateTime endDate, int? investorId = null, CancellationToken ct = default);
    Task<Result<InvestorReportDto>> GetInvestorReportAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default);
    Task<Result<MaintenanceReportDto>> GetMaintenanceReportAsync(DateTime startDate, DateTime endDate, int? vehicleId = null, CancellationToken ct = default);
    Task<Result<InsuranceReportDto>> GetInsuranceReportAsync(CancellationToken ct = default);
    Task<Result<AuditReportDto>> GetAuditReportAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default);
}
