using FleetErp.Application.Security.Interfaces;
using FleetErp.Domain.Entities.Accounting;
using FleetErp.Domain.Entities.Lookups;
using FleetErp.Domain.Entities.Security;
using FleetErp.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FleetErp.Infrastructure.Persistence;

public class DatabaseSeeder
{
    private readonly FleetErpDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(
        FleetErpDbContext context,
        IPasswordHasher passwordHasher,
        ILogger<DatabaseSeeder> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task SeedAsync(CancellationToken ct = default)
    {
        await SeedPermissionsAsync(ct);
        await SeedRolesAsync(ct);
        await SeedAdminUserAsync(ct);
        await SeedMenusAsync(ct);
        await SeedLookupItemsAsync(ct);
        await SeedSystemAccountsAsync(ct);
    }

    private async Task SeedPermissionsAsync(CancellationToken ct)
    {
        var existingCodes = await _context.Permissions
            .Select(p => p.Code)
            .ToHashSetAsync(ct);

        var newPermissions = new List<Permission>();

        foreach (var (code, description, module) in Permissions.GetAll())
        {
            if (!existingCodes.Contains(code))
            {
                newPermissions.Add(new Permission
                {
                    Code = code,
                    Description = description,
                    Module = module
                });
            }
        }

        if (newPermissions.Count > 0)
        {
            await _context.Permissions.AddRangeAsync(newPermissions, ct);
            await _context.SaveChangesAsync(ct);
            _logger.LogInformation("Seeded {Count} permissions", newPermissions.Count);
        }
    }

    private async Task SeedRolesAsync(CancellationToken ct)
    {
        // Seed Administrator role with all permissions
        var adminRole = await _context.Roles
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Name == "Administrator", ct);

        if (adminRole == null)
        {
            adminRole = new Role
            {
                Name = "Administrator",
                Description = "Full system access",
                IsSystem = true
            };
            await _context.Roles.AddAsync(adminRole, ct);
            await _context.SaveChangesAsync(ct);
            _logger.LogInformation("Created Administrator role");
        }

        // Assign all permissions to admin role
        var allPermissions = await _context.Permissions.ToListAsync(ct);
        var existingPermissionIds = adminRole.RolePermissions.Select(rp => rp.PermissionId).ToHashSet();

        foreach (var permission in allPermissions)
        {
            if (!existingPermissionIds.Contains(permission.Id))
            {
                adminRole.RolePermissions.Add(new RolePermission
                {
                    RoleId = adminRole.Id,
                    PermissionId = permission.Id
                });
            }
        }

        await _context.SaveChangesAsync(ct);

        // Seed Operator role with limited permissions
        var operatorRole = await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == "Operator", ct);

        if (operatorRole == null)
        {
            operatorRole = new Role
            {
                Name = "Operator",
                Description = "Day-to-day operations access",
                IsSystem = true
            };
            await _context.Roles.AddAsync(operatorRole, ct);
            await _context.SaveChangesAsync(ct);

            // Assign view permissions to operator
            var viewPermissions = allPermissions
                .Where(p => p.Code.EndsWith(".View"))
                .ToList();

            foreach (var permission in viewPermissions)
            {
                operatorRole.RolePermissions.Add(new RolePermission
                {
                    RoleId = operatorRole.Id,
                    PermissionId = permission.Id
                });
            }

            await _context.SaveChangesAsync(ct);
            _logger.LogInformation("Created Operator role");
        }

        // Seed Viewer role with read-only permissions
        var viewerRole = await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == "Viewer", ct);

        if (viewerRole == null)
        {
            viewerRole = new Role
            {
                Name = "Viewer",
                Description = "Read-only access",
                IsSystem = true
            };
            await _context.Roles.AddAsync(viewerRole, ct);
            await _context.SaveChangesAsync(ct);

            // Assign only view permissions
            var viewOnlyPermissions = allPermissions
                .Where(p => p.Code.EndsWith(".View"))
                .ToList();

            foreach (var permission in viewOnlyPermissions)
            {
                viewerRole.RolePermissions.Add(new RolePermission
                {
                    RoleId = viewerRole.Id,
                    PermissionId = permission.Id
                });
            }

            await _context.SaveChangesAsync(ct);
            _logger.LogInformation("Created Viewer role");
        }
    }

    private async Task SeedAdminUserAsync(CancellationToken ct)
    {
        var adminEmail = "admin@fleeterp.com";

        var existingAdmin = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == adminEmail, ct);

        if (existingAdmin != null)
        {
            return;
        }

        var adminRole = await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == "Administrator", ct);

        if (adminRole == null)
        {
            _logger.LogWarning("Administrator role not found, cannot create admin user");
            return;
        }

        var adminUser = new User
        {
            FullName = "System Administrator",
            Email = adminEmail,
            PasswordHash = _passwordHasher.Hash("Admin@123"),
            IsActive = true
        };

        await _context.Users.AddAsync(adminUser, ct);
        await _context.SaveChangesAsync(ct);

        adminUser.UserRoles.Add(new UserRole
        {
            UserId = adminUser.Id,
            RoleId = adminRole.Id
        });

        await _context.SaveChangesAsync(ct);
        _logger.LogInformation("Created admin user: {Email}", adminEmail);
    }

    private async Task SeedMenusAsync(CancellationToken ct)
    {
        var existingMenus = await _context.Menus.AnyAsync(ct);
        if (existingMenus)
        {
            return;
        }

        // Get permission IDs for menu items
        var permissions = await _context.Permissions.ToDictionaryAsync(p => p.Code, p => p.Id, ct);

        var menus = new List<Menu>
        {
            // Dashboard - no permission required
            new()
            {
                Name = "Dashboard",
                Icon = "dashboard",
                Route = "/dashboard",
                SortOrder = 1
            },

            // Investors
            new()
            {
                Name = "Investors",
                Icon = "people",
                Route = "/investors",
                SortOrder = 2,
                PermissionId = permissions.GetValueOrDefault(Permissions.Investors.View)
            },

            // Fleet Management
            new()
            {
                Name = "Fleet",
                Icon = "directions_car",
                Route = null,
                SortOrder = 3,
                PermissionId = permissions.GetValueOrDefault(Permissions.Vehicles.View),
                Children = new List<Menu>
                {
                    new()
                    {
                        Name = "Vehicles",
                        Icon = "directions_car",
                        Route = "/fleet/vehicles",
                        SortOrder = 1,
                        PermissionId = permissions.GetValueOrDefault(Permissions.Vehicles.View)
                    },
                    new()
                    {
                        Name = "Drivers",
                        Icon = "person",
                        Route = "/fleet/drivers",
                        SortOrder = 2,
                        PermissionId = permissions.GetValueOrDefault(Permissions.Drivers.View)
                    },
                    new()
                    {
                        Name = "Maintenance",
                        Icon = "build",
                        Route = "/fleet/maintenance",
                        SortOrder = 3,
                        PermissionId = permissions.GetValueOrDefault(Permissions.Maintenance.View)
                    },
                    new()
                    {
                        Name = "Insurance",
                        Icon = "security",
                        Route = "/fleet/insurance",
                        SortOrder = 4,
                        PermissionId = permissions.GetValueOrDefault(Permissions.Insurance.View)
                    }
                }
            },

            // Contracts
            new()
            {
                Name = "Contracts",
                Icon = "description",
                Route = "/contracts",
                SortOrder = 4,
                PermissionId = permissions.GetValueOrDefault(Permissions.Contracts.View)
            },

            // Finance
            new()
            {
                Name = "Finance",
                Icon = "attach_money",
                Route = null,
                SortOrder = 5,
                PermissionId = permissions.GetValueOrDefault(Permissions.Invoices.View),
                Children = new List<Menu>
                {
                    new()
                    {
                        Name = "Invoices",
                        Icon = "receipt",
                        Route = "/finance/invoices",
                        SortOrder = 1,
                        PermissionId = permissions.GetValueOrDefault(Permissions.Invoices.View)
                    },
                    new()
                    {
                        Name = "Payments",
                        Icon = "payment",
                        Route = "/finance/payments",
                        SortOrder = 2,
                        PermissionId = permissions.GetValueOrDefault(Permissions.Payments.View)
                    }
                }
            },

            // Reports
            new()
            {
                Name = "Reports",
                Icon = "bar_chart",
                Route = "/reports",
                SortOrder = 6,
                PermissionId = permissions.GetValueOrDefault(Permissions.Reports.View)
            },

            // Settings
            new()
            {
                Name = "Settings",
                Icon = "settings",
                Route = null,
                SortOrder = 7,
                PermissionId = permissions.GetValueOrDefault(Permissions.Settings.View),
                Children = new List<Menu>
                {
                    new()
                    {
                        Name = "Users",
                        Icon = "people",
                        Route = "/settings/users",
                        SortOrder = 1,
                        PermissionId = permissions.GetValueOrDefault(Permissions.Users.View)
                    },
                    new()
                    {
                        Name = "Roles",
                        Icon = "admin_panel_settings",
                        Route = "/settings/roles",
                        SortOrder = 2,
                        PermissionId = permissions.GetValueOrDefault(Permissions.Roles.View)
                    },
                    new()
                    {
                        Name = "Lookups",
                        Icon = "list",
                        Route = "/settings/lookups",
                        SortOrder = 3,
                        PermissionId = permissions.GetValueOrDefault(Permissions.Lookups.Manage)
                    }
                }
            }
        };

        await _context.Menus.AddRangeAsync(menus, ct);
        await _context.SaveChangesAsync(ct);
        _logger.LogInformation("Seeded navigation menus");
    }

    private async Task SeedLookupItemsAsync(CancellationToken ct)
    {
        var existingLookups = await _context.LookupItems
            .Select(l => new { l.LookupType, l.Code })
            .ToListAsync(ct);

        var existingKeys = existingLookups
            .Select(l => $"{l.LookupType}:{l.Code}")
            .ToHashSet();

        var newLookups = new List<LookupItem>();

        // Investor Status lookups
        var investorStatuses = new[]
        {
            (Code: "ACTIVE", Name: "Active", SortOrder: 1),
            (Code: "INACTIVE", Name: "Inactive", SortOrder: 2),
            (Code: "SUSPENDED", Name: "Suspended", SortOrder: 3),
            (Code: "PENDING", Name: "Pending Approval", SortOrder: 4)
        };

        foreach (var status in investorStatuses)
        {
            var key = $"{LookupTypes.InvestorStatus}:{status.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.InvestorStatus,
                    Code = status.Code,
                    Name = status.Name,
                    SortOrder = status.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Document Type lookups
        var documentTypes = new[]
        {
            (Code: "ID_CARD", Name: "ID Card", SortOrder: 1),
            (Code: "PASSPORT", Name: "Passport", SortOrder: 2),
            (Code: "CONTRACT", Name: "Contract", SortOrder: 3),
            (Code: "POWER_OF_ATTORNEY", Name: "Power of Attorney", SortOrder: 4),
            (Code: "BANK_STATEMENT", Name: "Bank Statement", SortOrder: 5),
            (Code: "OTHER", Name: "Other", SortOrder: 99)
        };

        foreach (var docType in documentTypes)
        {
            var key = $"{LookupTypes.DocumentType}:{docType.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.DocumentType,
                    Code = docType.Code,
                    Name = docType.Name,
                    SortOrder = docType.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Vehicle Status lookups
        var vehicleStatuses = new[]
        {
            (Code: "AVAILABLE", Name: "Available", SortOrder: 1),
            (Code: "RENTED", Name: "Rented", SortOrder: 2),
            (Code: "MAINTENANCE", Name: "In Maintenance", SortOrder: 3),
            (Code: "RESERVED", Name: "Reserved", SortOrder: 4),
            (Code: "SOLD", Name: "Sold", SortOrder: 5),
            (Code: "INACTIVE", Name: "Inactive", SortOrder: 6)
        };

        foreach (var status in vehicleStatuses)
        {
            var key = $"{LookupTypes.VehicleStatus}:{status.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.VehicleStatus,
                    Code = status.Code,
                    Name = status.Name,
                    SortOrder = status.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Vehicle Type lookups
        var vehicleTypes = new[]
        {
            (Code: "SEDAN", Name: "Sedan", SortOrder: 1),
            (Code: "SUV", Name: "SUV", SortOrder: 2),
            (Code: "HATCHBACK", Name: "Hatchback", SortOrder: 3),
            (Code: "COUPE", Name: "Coupe", SortOrder: 4),
            (Code: "TRUCK", Name: "Truck", SortOrder: 5),
            (Code: "VAN", Name: "Van", SortOrder: 6),
            (Code: "MOTORCYCLE", Name: "Motorcycle", SortOrder: 7),
            (Code: "BUS", Name: "Bus", SortOrder: 8)
        };

        foreach (var vType in vehicleTypes)
        {
            var key = $"{LookupTypes.VehicleType}:{vType.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.VehicleType,
                    Code = vType.Code,
                    Name = vType.Name,
                    SortOrder = vType.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Fuel Type lookups
        var fuelTypes = new[]
        {
            (Code: "GASOLINE", Name: "Gasoline", SortOrder: 1),
            (Code: "DIESEL", Name: "Diesel", SortOrder: 2),
            (Code: "ELECTRIC", Name: "Electric", SortOrder: 3),
            (Code: "HYBRID", Name: "Hybrid", SortOrder: 4),
            (Code: "LPG", Name: "LPG", SortOrder: 5)
        };

        foreach (var fType in fuelTypes)
        {
            var key = $"{LookupTypes.FuelType}:{fType.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.FuelType,
                    Code = fType.Code,
                    Name = fType.Name,
                    SortOrder = fType.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Transmission Type lookups
        var transmissionTypes = new[]
        {
            (Code: "AUTOMATIC", Name: "Automatic", SortOrder: 1),
            (Code: "MANUAL", Name: "Manual", SortOrder: 2),
            (Code: "CVT", Name: "CVT", SortOrder: 3),
            (Code: "SEMI_AUTO", Name: "Semi-Automatic", SortOrder: 4)
        };

        foreach (var tType in transmissionTypes)
        {
            var key = $"{LookupTypes.TransmissionType}:{tType.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.TransmissionType,
                    Code = tType.Code,
                    Name = tType.Name,
                    SortOrder = tType.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Customer Status lookups
        var customerStatuses = new[]
        {
            (Code: "ACTIVE", Name: "Active", SortOrder: 1),
            (Code: "SUSPENDED", Name: "Suspended", SortOrder: 2),
            (Code: "BLACKLISTED", Name: "Blacklisted", SortOrder: 3)
        };

        foreach (var status in customerStatuses)
        {
            var key = $"{LookupTypes.CustomerStatus}:{status.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.CustomerStatus,
                    Code = status.Code,
                    Name = status.Name,
                    SortOrder = status.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Rental Status lookups
        var rentalStatuses = new[]
        {
            (Code: "RESERVED", Name: "Reserved", SortOrder: 1),
            (Code: "ACTIVE", Name: "Active", SortOrder: 2),
            (Code: "COMPLETED", Name: "Completed", SortOrder: 3),
            (Code: "CANCELLED", Name: "Cancelled", SortOrder: 4)
        };

        foreach (var status in rentalStatuses)
        {
            var key = $"{LookupTypes.RentalStatus}:{status.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.RentalStatus,
                    Code = status.Code,
                    Name = status.Name,
                    SortOrder = status.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Payment Status lookups
        var paymentStatuses = new[]
        {
            (Code: "UNPAID", Name: "Unpaid", SortOrder: 1),
            (Code: "PARTIAL", Name: "Partial Payment", SortOrder: 2),
            (Code: "PAID", Name: "Fully Paid", SortOrder: 3)
        };

        foreach (var status in paymentStatuses)
        {
            var key = $"{LookupTypes.PaymentStatus}:{status.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.PaymentStatus,
                    Code = status.Code,
                    Name = status.Name,
                    SortOrder = status.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Payment Method lookups
        var paymentMethods = new[]
        {
            (Code: "CASH", Name: "Cash", SortOrder: 1),
            (Code: "CARD", Name: "Credit/Debit Card", SortOrder: 2),
            (Code: "BANK_TRANSFER", Name: "Bank Transfer", SortOrder: 3),
            (Code: "CHECK", Name: "Check", SortOrder: 4)
        };

        foreach (var method in paymentMethods)
        {
            var key = $"{LookupTypes.PaymentMethod}:{method.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.PaymentMethod,
                    Code = method.Code,
                    Name = method.Name,
                    SortOrder = method.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Account Type lookups
        var accountTypes = new[]
        {
            (Code: "INVESTOR", Name: "Investor Account", SortOrder: 1),
            (Code: "CUSTOMER", Name: "Customer Account", SortOrder: 2),
            (Code: "VEHICLE", Name: "Vehicle Operational Account", SortOrder: 3),
            (Code: "COMPANY", Name: "Company Account", SortOrder: 4),
            (Code: "CASHBOX", Name: "Cashbox Account", SortOrder: 5),
            (Code: "BANK", Name: "Bank Account", SortOrder: 6)
        };

        foreach (var aType in accountTypes)
        {
            var key = $"{LookupTypes.AccountType}:{aType.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.AccountType,
                    Code = aType.Code,
                    Name = aType.Name,
                    SortOrder = aType.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Maintenance Type lookups
        var maintenanceTypes = new[]
        {
            (Code: "OIL_CHANGE", Name: "Oil Change", SortOrder: 1),
            (Code: "TIRE_CHANGE", Name: "Tire Change/Rotation", SortOrder: 2),
            (Code: "BRAKE_SERVICE", Name: "Brake Service", SortOrder: 3),
            (Code: "ENGINE_SERVICE", Name: "Engine Service", SortOrder: 4),
            (Code: "TRANSMISSION", Name: "Transmission Service", SortOrder: 5),
            (Code: "AC_SERVICE", Name: "A/C Service", SortOrder: 6),
            (Code: "ELECTRICAL", Name: "Electrical Repair", SortOrder: 7),
            (Code: "BODY_WORK", Name: "Body Work", SortOrder: 8),
            (Code: "INSPECTION", Name: "Inspection", SortOrder: 9),
            (Code: "GENERAL", Name: "General Service", SortOrder: 10)
        };

        foreach (var mType in maintenanceTypes)
        {
            var key = $"{LookupTypes.MaintenanceType}:{mType.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.MaintenanceType,
                    Code = mType.Code,
                    Name = mType.Name,
                    SortOrder = mType.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Maintenance Status lookups
        var maintenanceStatuses = new[]
        {
            (Code: "SCHEDULED", Name: "Scheduled", SortOrder: 1),
            (Code: "IN_PROGRESS", Name: "In Progress", SortOrder: 2),
            (Code: "COMPLETED", Name: "Completed", SortOrder: 3),
            (Code: "CANCELLED", Name: "Cancelled", SortOrder: 4)
        };

        foreach (var mStatus in maintenanceStatuses)
        {
            var key = $"{LookupTypes.MaintenanceStatus}:{mStatus.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.MaintenanceStatus,
                    Code = mStatus.Code,
                    Name = mStatus.Name,
                    SortOrder = mStatus.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Transaction Type lookups
        var transactionTypes = new[]
        {
            (Code: "RENTAL_CREATED", Name: "Rental Created", SortOrder: 1),
            (Code: "PAYMENT_RECEIVED", Name: "Payment Received", SortOrder: 2),
            (Code: "PAYMENT_VOIDED", Name: "Payment Voided", SortOrder: 3),
            (Code: "MAINTENANCE_EXPENSE", Name: "Maintenance Expense", SortOrder: 4),
            (Code: "INVESTOR_WITHDRAWAL", Name: "Investor Withdrawal", SortOrder: 5),
            (Code: "BANK_TRANSFER", Name: "Bank Transfer", SortOrder: 6),
            (Code: "ADJUSTMENT", Name: "Adjustment", SortOrder: 7)
        };

        foreach (var tType in transactionTypes)
        {
            var key = $"{LookupTypes.TransactionType}:{tType.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.TransactionType,
                    Code = tType.Code,
                    Name = tType.Name,
                    SortOrder = tType.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Insurance Type lookups
        var insuranceTypes = new[]
        {
            (Code: "COMPREHENSIVE", Name: "Comprehensive", SortOrder: 1),
            (Code: "THIRD_PARTY", Name: "Third Party", SortOrder: 2),
            (Code: "COLLISION", Name: "Collision", SortOrder: 3),
            (Code: "LIABILITY", Name: "Liability Only", SortOrder: 4),
            (Code: "FULL_COVERAGE", Name: "Full Coverage", SortOrder: 5)
        };

        foreach (var iType in insuranceTypes)
        {
            var key = $"{LookupTypes.InsuranceType}:{iType.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.InsuranceType,
                    Code = iType.Code,
                    Name = iType.Name,
                    SortOrder = iType.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Insurance Status lookups
        var insuranceStatuses = new[]
        {
            (Code: "ACTIVE", Name: "Active", SortOrder: 1),
            (Code: "PENDING", Name: "Pending Activation", SortOrder: 2),
            (Code: "EXPIRED", Name: "Expired", SortOrder: 3),
            (Code: "CANCELLED", Name: "Cancelled", SortOrder: 4)
        };

        foreach (var iStatus in insuranceStatuses)
        {
            var key = $"{LookupTypes.InsuranceStatus}:{iStatus.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.InsuranceStatus,
                    Code = iStatus.Code,
                    Name = iStatus.Name,
                    SortOrder = iStatus.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Notification Type lookups
        var notificationTypes = new[]
        {
            (Code: "INSURANCE_EXPIRY", Name: "Insurance Expiry", SortOrder: 1),
            (Code: "MAINTENANCE_DUE", Name: "Maintenance Due", SortOrder: 2),
            (Code: "OIL_CHANGE_DUE", Name: "Oil Change Due", SortOrder: 3),
            (Code: "LICENSE_EXPIRY", Name: "License Expiry", SortOrder: 4),
            (Code: "CONTRACT_EXPIRY", Name: "Contract Expiry", SortOrder: 5),
            (Code: "LATE_PAYMENT", Name: "Late Payment", SortOrder: 6),
            (Code: "INVOICE_DUE", Name: "Invoice Due", SortOrder: 7),
            (Code: "VEHICLE_INSPECTION", Name: "Vehicle Inspection Due", SortOrder: 8),
            (Code: "SYSTEM", Name: "System Notification", SortOrder: 99)
        };

        foreach (var nType in notificationTypes)
        {
            var key = $"{LookupTypes.NotificationType}:{nType.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.NotificationType,
                    Code = nType.Code,
                    Name = nType.Name,
                    SortOrder = nType.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        // Notification Status lookups
        var notificationStatuses = new[]
        {
            (Code: "PENDING", Name: "Pending", SortOrder: 1),
            (Code: "READ", Name: "Read", SortOrder: 2),
            (Code: "DISMISSED", Name: "Dismissed", SortOrder: 3),
            (Code: "ACTIONED", Name: "Actioned", SortOrder: 4)
        };

        foreach (var nStatus in notificationStatuses)
        {
            var key = $"{LookupTypes.NotificationStatus}:{nStatus.Code}";
            if (!existingKeys.Contains(key))
            {
                newLookups.Add(new LookupItem
                {
                    LookupType = LookupTypes.NotificationStatus,
                    Code = nStatus.Code,
                    Name = nStatus.Name,
                    SortOrder = nStatus.SortOrder,
                    IsActive = true,
                    IsSystem = true
                });
            }
        }

        if (newLookups.Count > 0)
        {
            await _context.LookupItems.AddRangeAsync(newLookups, ct);
            await _context.SaveChangesAsync(ct);
            _logger.LogInformation("Seeded {Count} lookup items", newLookups.Count);
        }
    }

    private async Task SeedSystemAccountsAsync(CancellationToken ct)
    {
        // Get account type lookups
        var cashboxType = await _context.LookupItems
            .FirstOrDefaultAsync(l => l.LookupType == LookupTypes.AccountType && l.Code == "CASHBOX", ct);
        var companyType = await _context.LookupItems
            .FirstOrDefaultAsync(l => l.LookupType == LookupTypes.AccountType && l.Code == "COMPANY", ct);

        if (cashboxType == null || companyType == null)
        {
            _logger.LogWarning("Account type lookups not found, cannot seed system accounts");
            return;
        }

        var newAccounts = new List<Account>();

        // Seed Cashbox account
        var cashboxExists = await _context.Accounts.AnyAsync(a => a.Code == "CASHBOX-MAIN", ct);
        if (!cashboxExists)
        {
            newAccounts.Add(new Account
            {
                AccountTypeId = cashboxType.Id,
                OwnerType = AccountOwnerTypes.Cashbox,
                OwnerId = null,
                Code = "CASHBOX-MAIN",
                Name = "Main Cashbox",
                Currency = "USD",
                IsActive = true
            });
        }

        // Seed Company Revenue account
        var revenueExists = await _context.Accounts.AnyAsync(a => a.Code == "COMPANY-REVENUE", ct);
        if (!revenueExists)
        {
            newAccounts.Add(new Account
            {
                AccountTypeId = companyType.Id,
                OwnerType = AccountOwnerTypes.Company,
                OwnerId = null,
                Code = "COMPANY-REVENUE",
                Name = "Company Revenue",
                Currency = "USD",
                IsActive = true
            });
        }

        // Seed Company Expense account
        var expenseExists = await _context.Accounts.AnyAsync(a => a.Code == "COMPANY-EXPENSE", ct);
        if (!expenseExists)
        {
            newAccounts.Add(new Account
            {
                AccountTypeId = companyType.Id,
                OwnerType = AccountOwnerTypes.Company,
                OwnerId = null,
                Code = "COMPANY-EXPENSE",
                Name = "Company Expense",
                Currency = "USD",
                IsActive = true
            });
        }

        if (newAccounts.Count > 0)
        {
            await _context.Accounts.AddRangeAsync(newAccounts, ct);
            await _context.SaveChangesAsync(ct);
            _logger.LogInformation("Seeded {Count} system accounts", newAccounts.Count);
        }
    }
}
