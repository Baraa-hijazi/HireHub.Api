using FluentAssertions;
using HireHub.Api.Application.Common.Exceptions;
using NUnit.Framework;

namespace HireHub.Api.Application.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Test]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {
        IDictionary<string, string[]> actual = new ValidationException().Errors;

        actual.Keys.Should().BeEquivalentTo([]);
    }
}
