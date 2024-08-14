using HireHub.Api.Application.Candidates.Queries.DTOs;
using HireHub.Api.Application.Common.Interfaces;

namespace HireHub.Api.Application.Candidates.Queries.GetCandidate;

public record GetCandidateQuery(int Id) : IRequest<CandidateDto>;

public class GetCandidateQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetCandidateQuery, CandidateDto>
{
    public async Task<CandidateDto> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Candidates.FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        return mapper.Map<CandidateDto>(entity);
    }
}
