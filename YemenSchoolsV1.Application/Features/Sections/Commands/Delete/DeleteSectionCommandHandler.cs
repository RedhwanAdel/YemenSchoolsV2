using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Delete
{
    public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, Response<bool>>
    {
        private readonly ISectionRepository _repository;

        public DeleteSectionCommandHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<bool>> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(request.Id);
            if (!deleted)
                return new Response<bool>("Section not found.", false) { StatusCode = System.Net.HttpStatusCode.NotFound };

            return new Response<bool>(true, "Section deleted successfully.") { StatusCode = System.Net.HttpStatusCode.OK, Succeeded = true };
        }
    }
}
