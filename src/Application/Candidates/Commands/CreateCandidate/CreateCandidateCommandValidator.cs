using HireHub.Api.Application.Common.Interfaces;

namespace HireHub.Api.Application.Candidates.Commands.CreateCandidate;

public class CreateCandidateCommandValidator : AbstractValidator<CreateCandidateCommand>
{
    private readonly IApplicationDbContext _context = null!;

    public CreateCandidateCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.FirstName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.LastName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Email)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueEmail)
            .WithMessage("'{PropertyName}' must be unique.")
            .WithErrorCode("Unique");

        RuleFor(v => v.PhoneNumber)
            .MaximumLength(20);

        RuleFor(v => v.LinkedInUrl)
            .MaximumLength(500);

        RuleFor(v => v.GitHubUrl)
            .MaximumLength(500);

        RuleFor(v => v.Notes)
            .MaximumLength(1000);
    }

    // For unit testing purposes
    public CreateCandidateCommandValidator()
    {
        RuleFor(v => v.FirstName)
            .NotEmpty()
            .NotEmpty()
            .MaximumLength(200);
    
        RuleFor(v => v.LastName)
            .NotEmpty()
            .MaximumLength(200);
    
        RuleFor(v => v.PhoneNumber)
            .MaximumLength(20);
    
        RuleFor(v => v.Email)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("'{PropertyName}' must be unique.")
            .WithErrorCode("Unique");
    
        RuleFor(v => v.LinkedInUrl)
            .MaximumLength(500);
    
        RuleFor(v => v.GitHubUrl)
            .MaximumLength(500);
    
        RuleFor(v => v.Notes)
            .MaximumLength(1000);
    }

    private async Task<bool> BeUniqueEmail(CreateCandidateCommand model, string email,
        CancellationToken cancellationToken) =>
        await _context.Candidates.AllAsync(c => c.Email != email, cancellationToken);
}
