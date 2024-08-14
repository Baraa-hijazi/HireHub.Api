using HireHub.Api.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HireHub.Api.Infrastructure.Data;

public static class InitializerExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        ApplicationDbContextInitializer initialiser =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private Task TrySeedAsync()
    {
        if (context.Candidates.Any())
        {
            return Task.CompletedTask;
        }

        var candidates = new List<Candidate>
        {
            new()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "example@test.com",
                PhoneNumber = "1234567890",
                CallTimeFrom = new TimeOnly(9, 0),
                CallTimeTo = new TimeOnly(17, 0),
                LinkedInUrl = "https://www.linkedin.com/in/johndoe",
                GitHubUrl = "https://www.github.com/johndoe"
            },
            new()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.test@example.com",
                PhoneNumber = "0987654321",
                CallTimeFrom = new TimeOnly(10, 0),
                CallTimeTo = new TimeOnly(18, 0),
                LinkedInUrl = "https://www.linkedin.com/in/janedoe",
                GitHubUrl = "https://www.github.com/janedoe"
            }
        };

        context.Candidates.AddRange(candidates);

        context.SaveChanges();

        return Task.CompletedTask;
    }
}
