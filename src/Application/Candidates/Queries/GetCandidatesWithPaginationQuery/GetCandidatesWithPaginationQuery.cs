using HireHub.Api.Application.Common.Interfaces;
using HireHub.Api.Application.Common.Mappings;
using HireHub.Api.Application.Common.Models;

namespace HireHub.Api.Application.Candidates.Queries.GetCandidatesWithPaginationQuery;

public record GetCandidatesWithPaginationQuery : IRequest<PaginatedList<CandidateDto>>
{
    public string? Name { get; set; }

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class GetCandidatesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetCandidatesWithPaginationQuery, PaginatedList<CandidateDto>>
{
    public async Task<PaginatedList<CandidateDto>> Handle(GetCandidatesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Candidates.AsQueryable();

        if (request.Name != null)
        {
            query = query.Where(x => x.FirstName.Contains(request.Name) || x.LastName.Contains(request.Name));
        }

        var paginatedList = await query
            .OrderBy(x => x.FirstName)
            .ProjectTo<CandidateDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return paginatedList;
    }
}
