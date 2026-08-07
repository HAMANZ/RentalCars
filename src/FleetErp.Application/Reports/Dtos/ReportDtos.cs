namespace FleetErp.Application.Reports.Dtos;

public record RevenueReportDto(
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalRentalRevenue,
    decimal TotalPaymentsReceived,
    decimal OutstandingBalance,
    int TotalRentals,
    int CompletedRentals,
    int ActiveRentals,
    IReadOnlyList<RevenueByVehicleDto> RevenueByVehicle,
    IReadOnlyList<RevenueByMonthDto> RevenueByMonth
);

public record RevenueByVehicleDto(
    int VehicleId,
    string PlateNumber,
    string VehicleName,
    int RentalCount,
    decimal TotalRevenue,
    decimal PaidAmount
);

public record RevenueByMonthDto(
    int Year,
    int Month,
    string MonthName,
    decimal Revenue,
    int RentalCount
);

public record ExpenseReportDto(
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalMaintenanceCost,
    decimal TotalInsuranceCost,
    decimal TotalExpenses,
    int MaintenanceCount,
    int InsuranceRecordCount,
    IReadOnlyList<ExpenseByVehicleDto> ExpensesByVehicle,
    IReadOnlyList<ExpenseByCategoryDto> ExpensesByCategory
);

public record ExpenseByVehicleDto(
    int VehicleId,
    string PlateNumber,
    string VehicleName,
    decimal MaintenanceCost,
    decimal InsuranceCost,
    decimal TotalCost
);

public record ExpenseByCategoryDto(
    string Category,
    decimal Amount,
    int Count
);

public record VehicleUtilizationReportDto(
    DateTime StartDate,
    DateTime EndDate,
    int TotalVehicles,
    int VehiclesWithRentals,
    decimal AverageUtilizationRate,
    IReadOnlyList<VehicleUtilizationDto> Vehicles
);

public record VehicleUtilizationDto(
    int VehicleId,
    string PlateNumber,
    string VehicleName,
    string InvestorName,
    int DaysRented,
    int TotalDays,
    decimal UtilizationRate,
    decimal Revenue
);

public record InvestorReportDto(
    DateTime StartDate,
    DateTime EndDate,
    IReadOnlyList<InvestorSummaryDto> Investors
);

public record InvestorSummaryDto(
    int InvestorId,
    string InvestorName,
    int VehicleCount,
    int ActiveRentals,
    decimal TotalRevenue,
    decimal TotalExpenses,
    decimal NetIncome
);

public record MaintenanceReportDto(
    DateTime StartDate,
    DateTime EndDate,
    int TotalRecords,
    int CompletedRecords,
    int PendingRecords,
    decimal TotalCost,
    IReadOnlyList<MaintenanceByTypeDto> ByType,
    IReadOnlyList<MaintenanceByVehicleDto> ByVehicle
);

public record MaintenanceByTypeDto(
    string MaintenanceType,
    int Count,
    decimal TotalCost
);

public record MaintenanceByVehicleDto(
    int VehicleId,
    string PlateNumber,
    string VehicleName,
    int MaintenanceCount,
    decimal TotalCost,
    DateTime? LastMaintenanceDate
);

public record InsuranceReportDto(
    DateTime ReportDate,
    int TotalPolicies,
    int ActivePolicies,
    int ExpiringSoon,
    int Expired,
    decimal TotalPremiums,
    IReadOnlyList<InsuranceByCompanyDto> ByCompany,
    IReadOnlyList<InsuranceExpiryDto> ExpiringPolicies
);

public record InsuranceByCompanyDto(
    int CompanyId,
    string CompanyName,
    int PolicyCount,
    decimal TotalPremiums
);

public record InsuranceExpiryDto(
    int RecordId,
    int VehicleId,
    string PlateNumber,
    string VehicleName,
    string CompanyName,
    string PolicyNumber,
    DateTime ExpiryDate,
    int DaysUntilExpiry
);

public record AuditReportDto(
    DateTime StartDate,
    DateTime EndDate,
    int TotalActions,
    IReadOnlyList<AuditByActionDto> ByAction,
    IReadOnlyList<AuditByEntityDto> ByEntity,
    IReadOnlyList<AuditByUserDto> ByUser
);

public record AuditByActionDto(
    string Action,
    int Count
);

public record AuditByEntityDto(
    string EntityType,
    int Count
);

public record AuditByUserDto(
    string? UserId,
    string? UserName,
    int ActionCount
);

public record ReportFilterRequest(
    DateTime? StartDate,
    DateTime? EndDate,
    int? InvestorId,
    int? VehicleId
);
