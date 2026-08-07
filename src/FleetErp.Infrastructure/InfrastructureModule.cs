using FleetErp.Application.Accounting.Interfaces;
using FleetErp.Application.Audit.Interfaces;
using FleetErp.Application.Common.Interfaces;
using FleetErp.Application.Investors.Interfaces;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Application.Maintenance.Interfaces;
using FleetErp.Application.Security.Interfaces;
using FleetErp.Application.Vehicles.Interfaces;
using FleetErp.Application.Customers.Interfaces;
using FleetErp.Application.Rentals.Interfaces;
using FleetErp.Application.Insurance.Interfaces;
using FleetErp.Application.Notifications.Interfaces;
using FleetErp.Application.Reports.Interfaces;
using FleetErp.Infrastructure.Authorization;
using FleetErp.Infrastructure.BackgroundServices;
using FleetErp.Infrastructure.Identity;
using FleetErp.Infrastructure.Persistence;
using FleetErp.Infrastructure.Persistence.Repositories;
using FleetErp.Infrastructure.Persistence.Repositories.Accounting;
using FleetErp.Infrastructure.Persistence.Repositories.Security;
using FleetErp.Infrastructure.Services;
using FleetErp.Infrastructure.Services.Accounting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FleetErp.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<FleetErpDbContext>(options =>
        {
            options.UseMySQL(connectionString!, mysql =>
            {
                mysql.MigrationsAssembly(typeof(FleetErpDbContext).Assembly.FullName);
            });
        });

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repositories
        services.AddScoped<ILookupRepository, LookupRepository>();
        services.AddScoped<AuditLogRepository>();

        // Security Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();

        // Investor Repositories
        services.AddScoped<IInvestorRepository, InvestorRepository>();
        services.AddScoped<IInvestorDocumentRepository, InvestorDocumentRepository>();

        // Vehicle Repositories
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IVehicleDocumentRepository, VehicleDocumentRepository>();

        // Customer Repositories
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerDocumentRepository, CustomerDocumentRepository>();

        // Rental Repositories
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<IRentalPaymentRepository, RentalPaymentRepository>();

        // Accounting Repositories
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        // Maintenance Repositories
        services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
        services.AddScoped<IMaintenanceDocumentRepository, MaintenanceDocumentRepository>();

        // Insurance Repositories
        services.AddScoped<IInsuranceCompanyRepository, InsuranceCompanyRepository>();
        services.AddScoped<IInsuranceRepository, InsuranceRepository>();
        services.AddScoped<IInsuranceDocumentRepository, InsuranceDocumentRepository>();

        // Notification Repositories
        services.AddScoped<INotificationRepository, NotificationRepository>();

        // Services
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IAuditLogger, AuditLogger>();

        // Security Services
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IMenuService, MenuService>();

        // Investor Services
        services.AddScoped<IInvestorService, InvestorService>();

        // Vehicle Services
        services.AddScoped<IVehicleService, VehicleService>();

        // Customer Services
        services.AddScoped<ICustomerService, CustomerService>();

        // Rental Services
        services.AddScoped<IRentalService, RentalService>();

        // Accounting Services
        services.AddScoped<ITransactionEngine, TransactionEngine>();
        services.AddScoped<IAccountingService, AccountingService>();

        // Maintenance Services
        services.AddScoped<IMaintenanceService, MaintenanceService>();

        // Insurance Services
        services.AddScoped<IInsuranceService, InsuranceService>();

        // Notification Services
        services.AddScoped<INotificationService, NotificationService>();

        // Report Services
        services.AddScoped<IReportService, ReportService>();

        // Authorization
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        // JWT Settings
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        // Database Seeder
        services.AddScoped<DatabaseSeeder>();

        // Background Services
        services.AddHostedService<NotificationGeneratorService>();

        return services;
    }
}
