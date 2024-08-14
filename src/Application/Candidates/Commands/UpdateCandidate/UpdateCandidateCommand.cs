using HireHub.Api.Application.Common.Interfaces;

namespace HireHub.Api.Application.Candidates.Commands.UpdateCandidate;

public record UpdateCandidateCommand : IRequest
{
    public int Id { get; init; }

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

public class UpdateCandidateCommandHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateCandidateCommand>
{
    public async Task Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Candidates.FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        mapper.Map(request, entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
