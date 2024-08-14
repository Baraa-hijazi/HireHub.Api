namespace HireHub.Api.Domain.Entities;

public class Candidate : BaseAuditableEntity
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    // Time interval when it’s better to call (in case a call is needed)
    public TimeOnly? CallTimeFrom { get; set; }

    public TimeOnly? CallTimeTo { get; set; }

    public string? LinkedInUrl { get; set; }

    public string? GitHubUrl { get; set; }

    public string? Notes { get; set; }
}
