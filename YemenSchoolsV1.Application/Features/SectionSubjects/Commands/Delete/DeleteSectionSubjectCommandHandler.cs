using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Commands.Delete
{
    public class DeleteSectionSubjectCommandHandler : IRequestHandler<DeleteSectionSubjectCommand, Response<bool>>
    {
        private readonly ISectionSubjectRepository _repository;

        public DeleteSectionSubjectCommandHandler(ISectionSubjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<bool>> Handle(DeleteSectionSubjectCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(request.Id);
            if (!deleted)
                return new Response<bool>("SectionSubject not found.", false) { StatusCode = System.Net.HttpStatusCode.NotFound };

            return new Response<bool>(true, "SectionSubject deleted successfully.");
        }
    }
}
