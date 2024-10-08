﻿namespace HireHub.Api.Application.FunctionalTests;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        SqliteTestDatabase database = new();

        await database.InitialiseAsync();

        return database;
    }
}
