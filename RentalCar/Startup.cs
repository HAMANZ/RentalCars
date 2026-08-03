using CorePush.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.RespositoryPattern;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.ServiceLayer.Interface;
using RentalCar.ServiceLayer.Implementation;
using RentalCar.DomainLayer.Models;
using RentalCar.DomainLayer.Model;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Messaging;
using ServiceLayer.Interface;
using RentalCar.conf.JwtConfig;
using EsnadTakaful.ServiceLayer.Interface;
using EsnadTakaful.ServiceLayer.Implementation;
using ServiceLayer.Implementation;

namespace RentalCar
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private AppSettings AppSettings { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Serilog setup
            Serilog.Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            AppSettings = new AppSettings();
            Configuration.Bind(AppSettings);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // DbContext (Scoped)
            services.AddDbContext<RentalCarDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            // Identity setup
            services.AddIdentity<EUser, IdentityRole>(options =>
            {
                options.Tokens.ProviderMap.Add("Default",
                    new TokenProviderDescriptor(typeof(IUserTwoFactorTokenProvider<EUser>)));
            })
            .AddEntityFrameworkStores<RentalCarDbContext>()
            .AddDefaultTokenProviders();

            // Custom token provider (Scoped)
            services.AddScoped<EUserCustomTokenProvider>();

            // UserManager & EUserManager (Scoped)
            services.AddScoped<UserManager<EUser>>();
            services.AddScoped<EUserManager>();

            // UsersServices (Scoped)
            services.AddScoped<IEUser, UsersServices>();

            // Repository pattern (Scoped)
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Other scoped services
            services.AddScoped<IAppLabel, AppLabelServices>();
            services.AddScoped<IAnnouncement, AnnouncementsServices>();
            services.AddScoped<ILookUps, LookUpservices>();
            services.AddScoped<ILookUpMultiLang, LookUpMultiLangServices>();
            services.AddScoped<ILookUpMedia, LookUpMediaServices>();
            services.AddScoped<ILookUpLogic, LookUpLogicservices>();
            services.AddScoped<IAdmin, AdminServices>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILoggerError, LoggerErrorServices>();
            services.AddScoped<ServiceLayer.Interface.IUser, UserServices>();
            services.AddScoped<IAppSettings, AppSettingsServices>();
            services.AddScoped<INotification, NotificationsServices>();
            services.AddScoped<ILanguage, LanguageServices>();
            services.AddScoped<ICity, CityServices>();
            services.AddScoped<ICountry, CountryServices>();
            services.AddScoped<IContactus, ContactServices>();
            services.AddScoped<IMessageTemplate, MessageTemplateServices>();

            // HttpClient (Scoped)
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44354") });

            // SignalR
            services.AddSignalR();
            services.AddSingleton<IDictionary<string, UserRoomConnection>>(opts => new Dictionary<string, UserRoomConnection>());

            // Firebase
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("private_key.json")
                });
            }

            // JWT Config
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParams);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParams;
            })
            .AddGoogle(google =>
            {
                google.ClientId = "728367718832-dijmi0ter0us3qs2okt87rs79qr830dd.apps.googleusercontent.com";
                google.ClientSecret = "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";
            })
            .AddFacebook(fb =>
            {
                fb.AppId = "399212072894098";
                fb.AppSecret = "77be9974013f61b8ca9cc34194536870";
            });

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentalCar API", Version = "v1" });
                c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.OperationFilter<AuthResponsesOperationFilter>();
            });

            // MVC + Razor runtime compilation
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            // Session & cache
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.WithOrigins("https://RentalCar.com", "http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            // MVC Compatibility
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseMiddleware<SwaggerBasicAuthMiddleware>();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentalCar v1");
                    c.RoutePrefix = "swagger";
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSession();
            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/Home/PageNotFound", "?code={0}");

            app.UseRouting();

            app.UseCors("AllowOrigin");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    "adminError",
                    "{controller=Admin}/{action=Error}");
            });
        }
    }

    internal class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                             .Union(context.MethodInfo.GetCustomAttributes(true));

            if (attributes.OfType<IAllowAnonymous>().Any()) return;

            var authAttributes = attributes.OfType<IAuthorizeData>();

            if (authAttributes.Any())
            {
                operation.Responses["401"] = new OpenApiResponse { Description = "Unauthorized" };
                if (authAttributes.Any(att => !string.IsNullOrWhiteSpace(att.Roles) || !string.IsNullOrWhiteSpace(att.Policy)))
                    operation.Responses["403"] = new OpenApiResponse { Description = "Forbidden" };

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "BearerAuth",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    }
                };
            }
        }
    }
}
