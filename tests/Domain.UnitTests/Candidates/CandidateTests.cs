using HireHub.Api.Domain.Entities;
using NUnit.Framework;

namespace HireHub.Api.Domain.UnitTests.Candidates
{
    public class CandidatesTests
    {
        [Test]
        public void Candidate_Creation_ShouldBeSuccessful()
        {
            var candidate = new Candidate();

            Assert.That(candidate, Is.Not.Null);
        }

        [Test]
        public void Candidate_ShouldHaveCorrectProperties()
        {
            var candidate = new Candidate
            {
                Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com"
            };

            Assert.Multiple(() =>
            {
                Assert.That(candidate.Id, Is.EqualTo(1));
                Assert.That(candidate.FirstName + " " + candidate.LastName, Is.EqualTo("John Doe"));
                Assert.That(candidate.Email, Is.EqualTo("john.doe@example.com"));
            });
        }
    }
}
