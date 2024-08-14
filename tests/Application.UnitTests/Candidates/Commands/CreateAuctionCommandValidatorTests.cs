using FluentAssertions;
using FluentValidation.TestHelper;
using HireHub.Api.Application.Candidates.Commands.CreateCandidate;
using NUnit.Framework;

namespace HireHub.Api.Application.UnitTests.Candidates.Commands;

public class CreateCandidateCommandValidatorTests
{
    private readonly CreateCandidateCommand _command;
    private readonly CreateCandidateCommandValidator _validator = new();

    public CreateCandidateCommandValidatorTests()
    {
        _command = new CreateCandidateCommand
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "example@test.com",
            PhoneNumber = "1234567890",
            CallTimeFrom = new TimeOnly(9, 0),
            CallTimeTo = new TimeOnly(17, 0),
            LinkedInUrl = "https://www.linkedin.com/in/johndoe",
            GitHubUrl = "https://www.github.com/johndoe",
            Notes = "notes sample"
        };
    }

    [Test]
    public async Task ShouldBeValid()
    {
        var result = await _validator.TestValidateAsync(_command);

        result.IsValid.Should().BeTrue();
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ShouldHaveErrorWhenNameIsNull()
    {
        var model = new CreateCandidateCommand { FirstName = null! };

        var result = await _validator.TestValidateAsync(model);

        result.Errors.Count.Should().NotBe(0);
    }
}
