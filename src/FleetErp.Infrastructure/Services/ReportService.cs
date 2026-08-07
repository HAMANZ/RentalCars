using FleetErp.Application.Common;
using FleetErp.Application.Reports.Dtos;
using FleetErp.Application.Reports.Interfaces;
using FleetErp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FleetErp.Infrastructure.Services;

public class ReportService : IReportService
{
    private readonly FleetErpDbContext _context;

    public ReportService(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<Result<RevenueReportDto>> GetRevenueReportAsync(
        DateTime startDate,
        DateTime endDate,
        int? investorId = null,
        CancellationToken ct = default)
    {
        var rentalsQuery = _context.Rentals
            .Include(r => r.Vehicle)
            .Where(r => r.StartDate >= startDate && r.StartDate <= endDate);

        if (investorId.HasValue)
        {
            rentalsQuery = rentalsQuery.Where(r => r.Vehicle.InvestorId == investorId.Value);
        }

        var rentals = await rentalsQuery.ToListAsync(ct);

        var totalRevenue = rentals.Sum(r => r.TotalAmount);
        var totalPaid = rentals.Sum(r => r.PaidAmount);
        var totalRentals = rentals.Count;
        var completedRentals = rentals.Count(r => r.Status.Code == "COMPLETED");
        var activeRentals = rentals.Count(r => r.Status.Code == "ACTIVE" || r.Status.Code == "RESERVED");

        var revenueByVehicle = rentals
            .GroupBy(r => new { r.VehicleId, r.Vehicle.PlateNumber, VehicleName = $"{r.Vehicle.Make} {r.Vehicle.Model}" })
            .Select(g => new RevenueByVehicleDto(
                g.Key.VehicleId,
                g.Key.PlateNumber,
                g.Key.VehicleName,
                g.Count(),
                g.Sum(r => r.TotalAmount),
                g.Sum(r => r.PaidAmount)
            ))
            .OrderByDescending(r => r.TotalRevenue)
            .ToList();

        var revenueByMonth = rentals
            .GroupBy(r => new { r.StartDate.Year, r.StartDate.Month })
            .Select(g => new RevenueByMonthDto(
                g.Key.Year,
                g.Key.Month,
                CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                g.Sum(r => r.TotalAmount),
                g.Count()
            ))
            .OrderBy(r => r.Year)
            .ThenBy(r => r.Month)
            .ToList();

        return Result<RevenueReportDto>.Success(new RevenueReportDto(
            startDate,
            endDate,
            totalRevenue,
            totalPaid,
            totalRevenue - totalPaid,
            totalRentals,
            completedRentals,
            activeRentals,
            revenueByVehicle,
            revenueByMonth
        ));
    }

    public async Task<Result<ExpenseReportDto>> GetExpenseReportAsync(
        DateTime startDate,
        DateTime endDate,
        int? investorId = null,
        CancellationToken ct = default)
    {
        var maintenanceQuery = _context.MaintenanceRecords
            .Include(m => m.Vehicle)
            .Include(m => m.MaintenanceType)
            .Where(m => m.ScheduledDate >= startDate && m.ScheduledDate <= endDate);

        var insuranceQuery = _context.InsuranceRecords
            .Include(i => i.Vehicle)
            .Where(i => i.StartDate >= startDate && i.StartDate <= endDate);

        if (investorId.HasValue)
        {
            maintenanceQuery = maintenanceQuery.Where(m => m.Vehicle.InvestorId == investorId.Value);
            insuranceQuery = insuranceQuery.Where(i => i.Vehicle.InvestorId == investorId.Value);
        }

        var maintenance = await maintenanceQuery.ToListAsync(ct);
        var insurance = await insuranceQuery.ToListAsync(ct);

        var totalMaintenanceCost = maintenance.Sum(m => m.Cost);
        var totalInsuranceCost = insurance.Sum(i => i.Premium);

        // Group expenses by vehicle
        var maintenanceByVehicle = maintenance
            .GroupBy(m => new { m.VehicleId, m.Vehicle.PlateNumber, VehicleName = $"{m.Vehicle.Make} {m.Vehicle.Model}" })
            .ToDictionary(
                g => g.Key.VehicleId,
                g => new { g.Key.PlateNumber, g.Key.VehicleName, MaintenanceCost = g.Sum(m => m.Cost) }
            );

        var insuranceByVehicle = insurance
            .GroupBy(i => new { i.VehicleId, i.Vehicle.PlateNumber, VehicleName = $"{i.Vehicle.Make} {i.Vehicle.Model}" })
            .ToDictionary(
                g => g.Key.VehicleId,
                g => new { g.Key.PlateNumber, g.Key.VehicleName, InsuranceCost = g.Sum(i => i.Premium) }
            );

        var allVehicleIds = maintenanceByVehicle.Keys.Union(insuranceByVehicle.Keys).ToList();
        var expensesByVehicle = allVehicleIds
            .Select(vehicleId =>
            {
                var maint = maintenanceByVehicle.GetValueOrDefault(vehicleId);
                var ins = insuranceByVehicle.GetValueOrDefault(vehicleId);
                var plateNumber = maint?.PlateNumber ?? ins?.PlateNumber ?? "Unknown";
                var vehicleName = maint?.VehicleName ?? ins?.VehicleName ?? "Unknown";
                var maintCost = maint?.MaintenanceCost ?? 0;
                var insCost = ins?.InsuranceCost ?? 0;

                return new ExpenseByVehicleDto(vehicleId, plateNumber, vehicleName, maintCost, insCost, maintCost + insCost);
            })
            .OrderByDescending(e => e.TotalCost)
            .ToList();

        var expensesByCategory = maintenance
            .GroupBy(m => m.MaintenanceType.Name)
            .Select(g => new ExpenseByCategoryDto(g.Key, g.Sum(m => m.Cost), g.Count()))
            .OrderByDescending(e => e.Amount)
            .ToList();

        // Add insurance as a category
        if (insurance.Count > 0)
        {
            expensesByCategory.Add(new ExpenseByCategoryDto("Insurance", totalInsuranceCost, insurance.Count));
        }

        return Result<ExpenseReportDto>.Success(new ExpenseReportDto(
            startDate,
            endDate,
            totalMaintenanceCost,
            totalInsuranceCost,
            totalMaintenanceCost + totalInsuranceCost,
            maintenance.Count,
            insurance.Count,
            expensesByVehicle,
            expensesByCategory.OrderByDescending(e => e.Amount).ToList()
        ));
    }

    public async Task<Result<VehicleUtilizationReportDto>> GetVehicleUtilizationReportAsync(
        DateTime startDate,
        DateTime endDate,
        int? investorId = null,
        CancellationToken ct = default)
    {
        var totalDays = (endDate - startDate).Days + 1;

        var vehiclesQuery = _context.Vehicles
            .Include(v => v.Investor)
            .AsQueryable();

        if (investorId.HasValue)
        {
            vehiclesQuery = vehiclesQuery.Where(v => v.InvestorId == investorId.Value);
        }

        var vehicles = await vehiclesQuery.ToListAsync(ct);

        var rentals = await _context.Rentals
            .Where(r => r.StartDate <= endDate && r.EndDate >= startDate)
            .ToListAsync(ct);

        var vehicleUtilization = vehicles.Select(v =>
        {
            var vehicleRentals = rentals.Where(r => r.VehicleId == v.Id).ToList();
            var daysRented = 0;
            var revenue = 0m;

            foreach (var rental in vehicleRentals)
            {
                var rentalStart = rental.StartDate > startDate ? rental.StartDate : startDate;
                var rentalEnd = rental.EndDate < endDate ? rental.EndDate : endDate;
                daysRented += (rentalEnd - rentalStart).Days + 1;
                revenue += rental.TotalAmount;
            }

            var utilizationRate = totalDays > 0 ? (decimal)daysRented / totalDays * 100 : 0;

            return new VehicleUtilizationDto(
                v.Id,
                v.PlateNumber,
                $"{v.Make} {v.Model}",
                v.Investor.FullName,
                daysRented,
                totalDays,
                Math.Round(utilizationRate, 2),
                revenue
            );
        })
        .OrderByDescending(v => v.UtilizationRate)
        .ToList();

        var vehiclesWithRentals = vehicleUtilization.Count(v => v.DaysRented > 0);
        var avgUtilization = vehicleUtilization.Count > 0 ? vehicleUtilization.Average(v => v.UtilizationRate) : 0;

        return Result<VehicleUtilizationReportDto>.Success(new VehicleUtilizationReportDto(
            startDate,
            endDate,
            vehicles.Count,
            vehiclesWithRentals,
            Math.Round(avgUtilization, 2),
            vehicleUtilization
        ));
    }

    public async Task<Result<InvestorReportDto>> GetInvestorReportAsync(
        DateTime startDate,
        DateTime endDate,
        CancellationToken ct = default)
    {
        var investors = await _context.Investors.ToListAsync(ct);

        var vehicles = await _context.Vehicles.ToListAsync(ct);
        var vehiclesByInvestor = vehicles.GroupBy(v => v.InvestorId).ToDictionary(g => g.Key, g => g.ToList());

        var rentals = await _context.Rentals
            .Include(r => r.Vehicle)
            .Include(r => r.Status)
            .Where(r => r.StartDate >= startDate && r.StartDate <= endDate)
            .ToListAsync(ct);

        var maintenance = await _context.MaintenanceRecords
            .Include(m => m.Vehicle)
            .Where(m => m.ScheduledDate >= startDate && m.ScheduledDate <= endDate)
            .ToListAsync(ct);

        var insurance = await _context.InsuranceRecords
            .Include(i => i.Vehicle)
            .Where(i => i.StartDate >= startDate && i.StartDate <= endDate)
            .ToListAsync(ct);

        var investorSummaries = investors.Select(investor =>
        {
            var investorVehicles = vehiclesByInvestor.GetValueOrDefault(investor.Id, []);
            var investorVehicleIds = investorVehicles.Select(v => v.Id).ToList();
            var investorRentals = rentals.Where(r => investorVehicleIds.Contains(r.VehicleId)).ToList();
            var investorMaintenance = maintenance.Where(m => investorVehicleIds.Contains(m.VehicleId)).ToList();
            var investorInsurance = insurance.Where(i => investorVehicleIds.Contains(i.VehicleId)).ToList();

            var totalRevenue = investorRentals.Sum(r => r.TotalAmount);
            var totalExpenses = investorMaintenance.Sum(m => m.Cost) + investorInsurance.Sum(i => i.Premium);
            var activeRentals = investorRentals.Count(r => r.Status.Code == "ACTIVE" || r.Status.Code == "RESERVED");

            return new InvestorSummaryDto(
                investor.Id,
                investor.FullName,
                investorVehicles.Count,
                activeRentals,
                totalRevenue,
                totalExpenses,
                totalRevenue - totalExpenses
            );
        })
        .OrderByDescending(i => i.NetIncome)
        .ToList();

        return Result<InvestorReportDto>.Success(new InvestorReportDto(
            startDate,
            endDate,
            investorSummaries
        ));
    }

    public async Task<Result<MaintenanceReportDto>> GetMaintenanceReportAsync(
        DateTime startDate,
        DateTime endDate,
        int? vehicleId = null,
        CancellationToken ct = default)
    {
        var query = _context.MaintenanceRecords
            .Include(m => m.Vehicle)
            .Include(m => m.MaintenanceType)
            .Include(m => m.Status)
            .Where(m => m.ScheduledDate >= startDate && m.ScheduledDate <= endDate);

        if (vehicleId.HasValue)
        {
            query = query.Where(m => m.VehicleId == vehicleId.Value);
        }

        var records = await query.ToListAsync(ct);

        var totalCost = records.Sum(m => m.Cost);
        var completedRecords = records.Count(m => m.Status.Code == "COMPLETED");
        var pendingRecords = records.Count(m => m.Status.Code == "SCHEDULED" || m.Status.Code == "IN_PROGRESS");

        var byType = records
            .GroupBy(m => m.MaintenanceType.Name)
            .Select(g => new MaintenanceByTypeDto(g.Key, g.Count(), g.Sum(m => m.Cost)))
            .OrderByDescending(t => t.TotalCost)
            .ToList();

        var byVehicle = records
            .GroupBy(m => new { m.VehicleId, m.Vehicle.PlateNumber, VehicleName = $"{m.Vehicle.Make} {m.Vehicle.Model}" })
            .Select(g => new MaintenanceByVehicleDto(
                g.Key.VehicleId,
                g.Key.PlateNumber,
                g.Key.VehicleName,
                g.Count(),
                g.Sum(m => m.Cost),
                g.Max(m => m.CompletedDate)
            ))
            .OrderByDescending(v => v.TotalCost)
            .ToList();

        return Result<MaintenanceReportDto>.Success(new MaintenanceReportDto(
            startDate,
            endDate,
            records.Count,
            completedRecords,
            pendingRecords,
            totalCost,
            byType,
            byVehicle
        ));
    }

    public async Task<Result<InsuranceReportDto>> GetInsuranceReportAsync(CancellationToken ct = default)
    {
        var today = DateTime.UtcNow.Date;
        var thirtyDaysFromNow = today.AddDays(30);

        var records = await _context.InsuranceRecords
            .Include(i => i.Vehicle)
            .Include(i => i.InsuranceCompany)
            .Include(i => i.Status)
            .ToListAsync(ct);

        var activePolicies = records.Count(i => i.Status.Code == "ACTIVE");
        var expiringSoon = records.Count(i => i.EndDate >= today && i.EndDate <= thirtyDaysFromNow);
        var expired = records.Count(i => i.EndDate < today);
        var totalPremiums = records.Sum(i => i.Premium);

        var byCompany = records
            .GroupBy(i => new { i.InsuranceCompanyId, i.InsuranceCompany.Name })
            .Select(g => new InsuranceByCompanyDto(
                g.Key.InsuranceCompanyId,
                g.Key.Name,
                g.Count(),
                g.Sum(i => i.Premium)
            ))
            .OrderByDescending(c => c.PolicyCount)
            .ToList();

        var expiringPolicies = records
            .Where(i => i.EndDate >= today && i.EndDate <= thirtyDaysFromNow)
            .OrderBy(i => i.EndDate)
            .Select(i => new InsuranceExpiryDto(
                i.Id,
                i.VehicleId,
                i.Vehicle.PlateNumber,
                $"{i.Vehicle.Make} {i.Vehicle.Model}",
                i.InsuranceCompany.Name,
                i.PolicyNumber,
                i.EndDate,
                (i.EndDate - today).Days
            ))
            .ToList();

        return Result<InsuranceReportDto>.Success(new InsuranceReportDto(
            today,
            records.Count,
            activePolicies,
            expiringSoon,
            expired,
            totalPremiums,
            byCompany,
            expiringPolicies
        ));
    }

    public async Task<Result<AuditReportDto>> GetAuditReportAsync(
        DateTime startDate,
        DateTime endDate,
        CancellationToken ct = default)
    {
        var auditLogs = await _context.AuditLogs
            .Where(a => a.Timestamp >= startDate && a.Timestamp <= endDate)
            .ToListAsync(ct);

        var byAction = auditLogs
            .GroupBy(a => a.Action)
            .Select(g => new AuditByActionDto(g.Key, g.Count()))
            .OrderByDescending(a => a.Count)
            .ToList();

        var byEntity = auditLogs
            .GroupBy(a => a.EntityType)
            .Select(g => new AuditByEntityDto(g.Key, g.Count()))
            .OrderByDescending(e => e.Count)
            .ToList();

        var byUser = auditLogs
            .GroupBy(a => new { a.UserId, a.UserName })
            .Select(g => new AuditByUserDto(g.Key.UserId, g.Key.UserName, g.Count()))
            .OrderByDescending(u => u.ActionCount)
            .ToList();

        return Result<AuditReportDto>.Success(new AuditReportDto(
            startDate,
            endDate,
            auditLogs.Count,
            byAction,
            byEntity,
            byUser
        ));
    }
}
