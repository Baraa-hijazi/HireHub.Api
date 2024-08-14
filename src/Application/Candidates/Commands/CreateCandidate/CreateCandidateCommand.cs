using HireHub.Api.Application.Common.Exceptions;
using HireHub.Api.Application.Common.Interfaces;
using HireHub.Api.Domain.Entities;

namespace HireHub.Api.Application.Candidates.Commands.CreateCandidate;

public record CreateCandidateCommand : IRequest<int>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public TimeOnly? CallTimeFrom { get; set; }

    public TimeOnly? CallTimeTo { get; set; }

    public string? LinkedInUrl { get; set; }

    public string? GitHubUrl { get; set; }

    public string? Notes { get; set; }
}

public class CreateCandidateCommandHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<CreateCandidateCommand, int>
{
    public async Task<int> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        // check if the candidate already exists
        var existingCandidate = await context.Candidates
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (existingCandidate != null)
            throw new DuplicateCandidateException("A candidate with this email already exists.");

        var candidate = mapper.Map<Candidate>(request);

        context.Candidates.Add(candidate);

        await context.SaveChangesAsync(cancellationToken);

        return candidate.Id;
    }
}
