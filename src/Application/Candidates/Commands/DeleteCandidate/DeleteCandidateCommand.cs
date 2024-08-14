using HireHub.Api.Application.Common.Interfaces;

namespace HireHub.Api.Application.Candidates.Commands.DeleteCandidate;

public record DeleteCandidateCommand(int Id) : IRequest;

public class DeleteCandidateCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteCandidateCommand>
{
    public async Task Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Candidates.FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        context.Candidates.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
