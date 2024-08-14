using System.Data;
using System.Data.Common;
using HireHub.Api.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HireHub.Api.Application.FunctionalTests;

public class SqliteTestDatabase : ITestDatabase
{
    private readonly SqliteConnection _connection;

    public SqliteTestDatabase()
    {
        const string connectionString = "DataSource=:memory:";
        _connection = new SqliteConnection(connectionString);
    }

    public async Task InitialiseAsync()
    {
        if (_connection.State == ConnectionState.Open)
        {
            await _connection.CloseAsync();
        }

        await _connection.OpenAsync();

        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        ApplicationDbContext context = new(options);

        await context.Database.MigrateAsync();
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await InitialiseAsync();
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}
