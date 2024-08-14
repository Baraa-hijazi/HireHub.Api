using HireHub.Api.Application.Common.Interfaces;
using HireHub.Api.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HireHub.Api.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddExceptionHandler<CustomExceptionHandler>();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        services.AddOpenApiDocument((configure, _) => { configure.Title = "HireHub.Api API"; });

        return services;
    }
}
