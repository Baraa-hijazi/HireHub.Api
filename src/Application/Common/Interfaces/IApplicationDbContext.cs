using HireHub.Api.Domain.Entities;

namespace HireHub.Api.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Candidate> Candidates { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
