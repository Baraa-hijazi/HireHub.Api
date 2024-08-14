namespace HireHub.Api.Application.Candidates.Commands.UpdateCandidate;

public class UpdateCandidateCommandValidator : AbstractValidator<UpdateCandidateCommand>
{
    public UpdateCandidateCommandValidator()
    {
        RuleFor(v => v.FirstName)
            .MaximumLength(200);

        RuleFor(v => v.LastName)
            .MaximumLength(200);

        RuleFor(v => v.Email)
            .MaximumLength(200);

        RuleFor(v => v.PhoneNumber)
            .MaximumLength(20);

        RuleFor(v => v.LinkedInUrl)
            .MaximumLength(500);

        RuleFor(v => v.GitHubUrl)
            .MaximumLength(500);

        RuleFor(v => v.Notes)
            .MaximumLength(1000);
    }
}
