using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace RentalCar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Seed the test administrator account before the app starts serving requests.
            try
            {
                IdentitySeeder.SeedAdminAsync(host.Services).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Startup] Admin seeding failed: " + ex.Message);
            }

            // Seed sample cars and their related detail records (idempotent).
            try
            {
                CarDataSeeder.SeedAsync(host.Services).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Startup] Car data seeding failed: " + ex.Message);
                var inner = ex.InnerException;
                while (inner != null)
                {
                    Console.WriteLine("   INNER: " + inner.Message);
                    inner = inner.InnerException;
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    

                    webBuilder.UseStartup<Startup>();
                });



    }
}
