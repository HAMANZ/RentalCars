using System.Linq.Expressions;
using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Accounting;
using FleetErp.Domain.Entities.Audit;
using FleetErp.Domain.Entities.Lookups;
using FleetErp.Domain.Entities.Investors;
using FleetErp.Domain.Entities.Maintenance;
using FleetErp.Domain.Entities.Security;
using FleetErp.Domain.Entities.Vehicles;
using FleetErp.Domain.Entities.Customers;
using FleetErp.Domain.Entities.Rentals;
using FleetErp.Domain.Entities.Insurance;
using FleetErp.Domain.Entities.Notifications;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence;

public class FleetErpDbContext : DbContext
{
    public FleetErpDbContext(DbContextOptions<FleetErpDbContext> options) : base(options)
    {
    }

    // Lookup & Audit (Phase 0)
    public DbSet<LookupItem> LookupItems => Set<LookupItem>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    // Security (Phase 1)
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Menu> Menus => Set<Menu>();

    // Investors (Phase 2)
    public DbSet<Investor> Investors => Set<Investor>();
    public DbSet<InvestorDocument> InvestorDocuments => Set<InvestorDocument>();

    // Vehicles (Phase 3)
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<VehicleDocument> VehicleDocuments => Set<VehicleDocument>();

    // Customers (Phase 4)
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerDocument> CustomerDocuments => Set<CustomerDocument>();

    // Rentals (Phase 6)
    public DbSet<Rental> Rentals => Set<Rental>();
    public DbSet<RentalPayment> RentalPayments => Set<RentalPayment>();

    // Accounting (Phase 9)
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    // Maintenance (Phase 10)
    public DbSet<MaintenanceRecord> MaintenanceRecords => Set<MaintenanceRecord>();
    public DbSet<MaintenanceDocument> MaintenanceDocuments => Set<MaintenanceDocument>();

    // Insurance (Phase 11)
    public DbSet<InsuranceCompany> InsuranceCompanies => Set<InsuranceCompany>();
    public DbSet<InsuranceRecord> InsuranceRecords => Set<InsuranceRecord>();
    public DbSet<InsuranceDocument> InsuranceDocuments => Set<InsuranceDocument>();

    // Notifications (Phase 12)
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FleetErpDbContext).Assembly);

        // Global query filter for soft delete on all entities inheriting BaseEntity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(BuildSoftDeleteFilter(entityType.ClrType));
            }
        }
    }

    private static LambdaExpression BuildSoftDeleteFilter(Type entityType)
    {
        var parameter = System.Linq.Expressions.Expression.Parameter(entityType, "e");
        var property = System.Linq.Expressions.Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
        var condition = System.Linq.Expressions.Expression.Equal(property, System.Linq.Expressions.Expression.Constant(false));
        return System.Linq.Expressions.Expression.Lambda(condition, parameter);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Set audit fields on tracked entities
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
