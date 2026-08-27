using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RentalCar.DomainLayer.Models;

namespace RentalCar
{
    /// <summary>
    /// Seeds a ready-to-use administrator account so the login page can be tested.
    /// The design-time HasData seed inserts an EUser with no password hash and no
    /// role, which cannot sign in. This runtime seeder creates a proper account
    /// (hashed password + role) through Identity's managers.
    /// </summary>
    public static class IdentitySeeder
    {
        // Test admin credentials.
        public const string AdminEmail = "admin@rentalcar.com";
        public const string AdminPassword = "Admin@123";
        public const string AdminRole = "Adminstrator"; // matches the role seeded in RentalCarDbContext

        public static async Task SeedAdminAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var provider = scope.ServiceProvider;

            var userManager = provider.GetRequiredService<EUserManager>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            // Ensure the admin role exists.
            if (!await roleManager.RoleExistsAsync(AdminRole))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = AdminRole,
                    NormalizedName = AdminRole.ToUpperInvariant()
                });
            }

            // Create the admin user only if it does not already exist.
            var existing = await userManager.FindByEmailAsync(AdminEmail);
            if (existing == null)
            {
                var admin = new EUser
                {
                    UserName = AdminEmail,
                    Email = AdminEmail,
                    EmailConfirmed = true,
                    FullName_ar = "مدير النظام",
                    FullName = "System Administrator",
                    Profile = "",
                    FToken = "",
                    GenderId = 1, // Male (seeded in RentalCarDbContext)
                    Is_deleted = false,
                    Created_at = DateTime.UtcNow,
                    Updated_at = DateTime.UtcNow,
                    Created_by = 1,
                    Updated_by = 1
                };

                var result = await userManager.CreateAsync(admin, AdminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, AdminRole);
                }
                else
                {
                    Console.WriteLine("[IdentitySeeder] Failed to create admin user:");
                    foreach (var error in result.Errors)
                        Console.WriteLine($"  - {error.Code}: {error.Description}");
                }
            }
            else if (!await userManager.IsInRoleAsync(existing, AdminRole))
            {
                // Account exists but is missing the admin role; add it.
                await userManager.AddToRoleAsync(existing, AdminRole);
            }
        }
    }
}
