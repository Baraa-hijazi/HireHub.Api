using HireHub.Api.Application.Common.Models;
using HireHub.Api.Application.Candidates.Commands.CreateCandidate;
using HireHub.Api.Application.Candidates.Commands.DeleteCandidate;
using HireHub.Api.Application.Candidates.Commands.UpdateCandidate;
using HireHub.Api.Application.Candidates.Queries.DTOs;
using HireHub.Api.Application.Candidates.Queries.GetCandidate;
using HireHub.Api.Application.Candidates.Queries.GetCandidatesWithPagination;

namespace HireHub.Api.Web.Endpoints;

public class Candidates : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetCandidate, "{id}")
            .MapGet(GetCandidatesWithPagination)
            .MapPost(CreateCandidates)
            .MapPut(UpdateCandidates, "{id}")
            .MapDelete(DeleteCandidates, "{id}");
    }

    private Task<CandidateDto> GetCandidate(ISender sender, int id)
    {
        return sender.Send(new GetCandidateQuery(id));
    }

    private Task<PaginatedList<CandidateDto>> GetCandidatesWithPagination(ISender sender,
        [AsParameters] GetCandidatesWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    private Task<int> CreateCandidates(ISender sender, CreateCandidateCommand command)
    {
        return sender.Send(command);
    }

    private async Task<IResult> UpdateCandidates(ISender sender, int id, UpdateCandidateCommand command)
    {
        if (id != command.Id)
            return Results.BadRequest("Id mismatch");

        await sender.Send(command);

        return Results.NoContent();
    }

    private async Task<IResult> DeleteCandidates(ISender sender, int id)
    {
        await sender.Send(new DeleteCandidateCommand(id));

        return Results.NoContent();
    }
}
