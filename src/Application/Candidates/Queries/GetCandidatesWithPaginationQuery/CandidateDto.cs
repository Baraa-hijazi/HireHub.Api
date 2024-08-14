using HireHub.Api.Domain.Entities;

namespace HireHub.Api.Application.Candidates.Queries.GetCandidatesWithPaginationQuery;

public class CandidateDto
{
    public int Id { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Candidate, CandidateDto>();
        }
    }
}
