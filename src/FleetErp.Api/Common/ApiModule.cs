using FleetErp.Application.Common.Interfaces;
using FleetErp.Application.Security.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace FleetErp.Api.Common;

public static class ApiModule
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Register ICurrentUser implementation
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();

        // FluentValidation
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

        return services;
    }
}
