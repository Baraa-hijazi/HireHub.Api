using HireHub.Api.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HireHub.Api.Application.FunctionalTests;

[SetUpFixture]
public class Testing
{
    private static ITestDatabase s_database = null!;
    private static CustomWebApplicationFactory s_factory = null!;
    private static IServiceScopeFactory s_scopeFactory = null!;

    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        s_database = await TestDatabaseFactory.CreateAsync();

        s_factory = new CustomWebApplicationFactory(s_database.GetConnection());

        s_scopeFactory = s_factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using IServiceScope scope = s_scopeFactory.CreateScope();

        ISender mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using IServiceScope scope = s_scopeFactory.CreateScope();

        ISender mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static async Task ResetState()
    {
        try
        {
            await s_database.ResetAsync();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
        using IServiceScope scope = s_scopeFactory.CreateScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        using IServiceScope scope = s_scopeFactory.CreateScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using IServiceScope scope = s_scopeFactory.CreateScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    [OneTimeTearDown]
    public async Task RunAfterAnyTests()
    {
        await s_database.DisposeAsync();
        await s_factory.DisposeAsync();
    }
}
