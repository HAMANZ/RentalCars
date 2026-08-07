using FleetErp.Application.Customers.Interfaces;
using FleetErp.Application.Insurance.Interfaces;
using FleetErp.Application.Investors.Interfaces;
using FleetErp.Application.Maintenance.Interfaces;
using FleetErp.Application.Notifications.Dtos;
using FleetErp.Application.Notifications.Interfaces;
using FleetErp.Application.Rentals.Interfaces;
using FleetErp.Application.Security.Interfaces;
using FleetErp.Application.Vehicles.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IInvestorService _investorService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IVehicleService _vehicleService;
    private readonly ICustomerService _customerService;
    private readonly IRentalService _rentalService;
    private readonly IMaintenanceService _maintenanceService;
    private readonly IInsuranceService _insuranceService;
    private readonly INotificationService _notificationService;

    public IndexModel(
        IInvestorService investorService,
        IUserService userService,
        IRoleService roleService,
        IVehicleService vehicleService,
        ICustomerService customerService,
        IRentalService rentalService,
        IMaintenanceService maintenanceService,
        IInsuranceService insuranceService,
        INotificationService notificationService)
    {
        _investorService = investorService;
        _userService = userService;
        _roleService = roleService;
        _vehicleService = vehicleService;
        _customerService = customerService;
        _rentalService = rentalService;
        _maintenanceService = maintenanceService;
        _insuranceService = insuranceService;
        _notificationService = notificationService;
    }

    // Core stats
    public int TotalInvestors { get; set; }
    public int TotalUsers { get; set; }
    public int TotalVehicles { get; set; }
    public int AvailableVehicles { get; set; }
    public int TotalCustomers { get; set; }
    public int ActiveRentals { get; set; }
    public int TotalRoles { get; set; }

    // Maintenance & Insurance
    public int MaintenanceDue { get; set; }
    public int InsuranceExpiringSoon { get; set; }

    // Notifications
    public int UnreadNotifications { get; set; }
    public IReadOnlyList<NotificationDto> RecentNotifications { get; set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        // Get investor stats
        var investorsResult = await _investorService.GetPagedAsync(1, 1, null, ct);
        if (investorsResult.IsSuccess)
        {
            TotalInvestors = investorsResult.Value!.TotalCount;
        }

        // Get users stats
        var usersResult = await _userService.GetPagedAsync(1, 1, null, ct);
        if (usersResult.IsSuccess)
        {
            TotalUsers = usersResult.Value!.TotalCount;
        }

        // Get roles count
        var rolesResult = await _roleService.GetAllAsync(ct);
        if (rolesResult.IsSuccess)
        {
            TotalRoles = rolesResult.Value!.Count;
        }

        // Get vehicles stats
        var vehiclesResult = await _vehicleService.GetPagedAsync(1, 1, null, null, null, null, ct);
        if (vehiclesResult.IsSuccess)
        {
            TotalVehicles = vehiclesResult.Value!.TotalCount;
        }

        // Get available vehicles
        var availableVehiclesResult = await _vehicleService.GetAvailableAsync(ct);
        if (availableVehiclesResult.IsSuccess)
        {
            AvailableVehicles = availableVehiclesResult.Value!.Count;
        }

        // Get customers stats
        var customersResult = await _customerService.GetPagedAsync(1, 1, null, ct);
        if (customersResult.IsSuccess)
        {
            TotalCustomers = customersResult.Value!.TotalCount;
        }

        // Get active rentals
        var rentalsResult = await _rentalService.GetActiveRentalsAsync(ct);
        if (rentalsResult.IsSuccess)
        {
            ActiveRentals = rentalsResult.Value!.Count;
        }

        // Get maintenance due (upcoming 7 days)
        var maintenanceResult = await _maintenanceService.GetUpcomingAsync(7, ct);
        if (maintenanceResult.IsSuccess)
        {
            MaintenanceDue = maintenanceResult.Value!.Count;
        }

        // Get insurance expiring soon (30 days)
        var insuranceResult = await _insuranceService.GetExpiringAsync(30, ct);
        if (insuranceResult.IsSuccess)
        {
            InsuranceExpiringSoon = insuranceResult.Value!.Count;
        }

        // Get notifications
        var unreadCountResult = await _notificationService.GetUnreadCountAsync(ct);
        if (unreadCountResult.IsSuccess)
        {
            UnreadNotifications = unreadCountResult.Value;
        }

        var notificationsResult = await _notificationService.GetUnreadAsync(ct);
        if (notificationsResult.IsSuccess)
        {
            RecentNotifications = notificationsResult.Value!.Take(5).ToList();
        }
    }
}
