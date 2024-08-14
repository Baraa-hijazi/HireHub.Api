using System.ComponentModel;
using HireHub.Api.Application.Common.Exceptions;
using HireHub.Api.Application.Common.Interfaces;
using HireHub.Api.Domain.Entities;

namespace HireHub.Api.Application.Candidates.Commands.CreateCandidate;

public record CreateCandidateCommand : IRequest<int>
{
    [DefaultValue("John")]
    public string FirstName { get; set; } = null!;

    [DefaultValue("Doe")]
    public string LastName { get; set; } = null!;

    [DefaultValue("Sample@email.com")]
    public string Email { get; set; } = null!;

    [DefaultValue("1234567890")]
    public string? PhoneNumber { get; set; }

    [DefaultValue("09:00:00")]
    public TimeOnly? CallTimeFrom { get; set; }

    [DefaultValue("17:00:00")]
    public TimeOnly? CallTimeTo { get; set; }

    [DefaultValue("https://www.linkedin.com/in/johndoe")]
    public string? LinkedInUrl { get; set; }

    [DefaultValue("https://github.com/Baraa-hijazi/HireHub.Api")]
    public string? GitHubUrl { get; set; }

    [DefaultValue("This is a sample note.")]
    public string? Notes { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateCandidateCommand, Candidate>();
        }
    }
}

public class CreateCandidateCommandHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<CreateCandidateCommand, int>
{
    public async Task<int> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = mapper.Map<Candidate>(request);

        context.Candidates.Add(candidate);

        await context.SaveChangesAsync(cancellationToken);

        return candidate.Id;
    }
}
