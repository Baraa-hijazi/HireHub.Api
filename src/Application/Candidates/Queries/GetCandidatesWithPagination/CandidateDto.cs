using HireHub.Api.Domain.Entities;

namespace HireHub.Api.Application.Candidates.Queries.GetCandidatesWithPagination;

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
