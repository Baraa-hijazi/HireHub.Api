using HireHub.Api.Domain.Entities;

namespace HireHub.Api.Application.Candidates.Queries.DTOs;

public class CandidateDto
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

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Candidate, CandidateDto>();
        }
    }
}
