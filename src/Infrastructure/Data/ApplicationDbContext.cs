﻿using System.Reflection;
using HireHub.Api.Application.Common.Interfaces;
using HireHub.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HireHub.Api.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Candidate> Candidates => Set<Candidate>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
