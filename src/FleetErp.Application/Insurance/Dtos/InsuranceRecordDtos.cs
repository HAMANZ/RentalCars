namespace FleetErp.Application.Insurance.Dtos;

public record InsuranceRecordDto(
    int Id,
    int VehicleId,
    string VehiclePlateNumber,
    string VehicleDescription,
    int InsuranceCompanyId,
    string InsuranceCompanyName,
    int InsuranceTypeId,
    string InsuranceTypeName,
    int StatusId,
    string StatusName,
    string PolicyNumber,
    DateTime StartDate,
    DateTime EndDate,
    decimal Premium,
    decimal? CoverageAmount,
    decimal? Deductible,
    string? CoverageDetails,
    string? Notes,
    bool RenewalReminderSent,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int DocumentCount
);

public record InsuranceRecordListDto(
    int Id,
    string VehiclePlateNumber,
    string VehicleDescription,
    string InsuranceCompanyName,
    string InsuranceTypeName,
    string StatusName,
    string PolicyNumber,
    DateTime StartDate,
    DateTime EndDate,
    decimal Premium,
    int DaysUntilExpiry
);

public record CreateInsuranceRequest(
    int VehicleId,
    int InsuranceCompanyId,
    int InsuranceTypeId,
    int StatusId,
    string PolicyNumber,
    DateTime StartDate,
    DateTime EndDate,
    decimal Premium,
    decimal? CoverageAmount,
    decimal? Deductible,
    string? CoverageDetails,
    string? Notes
);

public record UpdateInsuranceRequest(
    int InsuranceCompanyId,
    int InsuranceTypeId,
    int StatusId,
    string PolicyNumber,
    DateTime StartDate,
    DateTime EndDate,
    decimal Premium,
    decimal? CoverageAmount,
    decimal? Deductible,
    string? CoverageDetails,
    string? Notes
);

public record RenewInsuranceRequest(
    DateTime NewStartDate,
    DateTime NewEndDate,
    decimal NewPremium,
    decimal? NewCoverageAmount,
    decimal? NewDeductible,
    string? Notes
);
