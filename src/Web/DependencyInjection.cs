namespace HireHub.Api.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddEndpointsApiExplorer();

        services.AddOpenApiDocument((configure, _) => { configure.Title = "HireHub.Api API"; });

        return services;
    }
}
