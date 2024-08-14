using System.ComponentModel;
using HireHub.Api.Application.Common.Interfaces;
using HireHub.Api.Domain.Entities;

namespace HireHub.Api.Application.Candidates.Commands.UpdateCandidate;

public record UpdateCandidateCommand : IRequest
{
    public int Id { get; init; }

    [DefaultValue("Jon")]
    public string FirstName { get; set; } = null!;

    [DefaultValue("snow")]
    public string LastName { get; set; } = null!;

    [DefaultValue("updated@email.com")]
    public string Email { get; set; } = null!;

    [DefaultValue("0987654321")]
    public string? PhoneNumber { get; set; }

    [DefaultValue("08:00:00")]
    public TimeOnly? CallTimeFrom { get; set; }

    [DefaultValue("16:00:00")]
    public TimeOnly? CallTimeTo { get; set; }

    [DefaultValue("https://www.linkedin.com/in/updated")]
    public string? LinkedInUrl { get; set; }

    [DefaultValue("https://github.com/Baraa-hijazi/updated")]
    public string? GitHubUrl { get; set; }

    [DefaultValue("This is an updated sample note.")]
    public string? Notes { get; set; }
    
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateCandidateCommand, Candidate>();
        }
    }
}

public class UpdateCandidateCommandHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateCandidateCommand>
{
    public async Task Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = await context.Candidates.FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, candidate);

        mapper.Map(request, candidate);

        await context.SaveChangesAsync(cancellationToken);
    }
}
