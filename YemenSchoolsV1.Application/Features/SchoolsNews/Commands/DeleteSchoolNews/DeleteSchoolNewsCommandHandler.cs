using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Commands.DeleteSchoolNews
{
    public class DeleteSchoolNewsCommandHandler : ResponseHandler, IRequestHandler<DeleteSchoolNewsCommand, Response<bool>>
    {
        private readonly ISchoolNewsRepository _schoolNewsRepository;

        public DeleteSchoolNewsCommandHandler(ISchoolNewsRepository schoolNewsRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _schoolNewsRepository = schoolNewsRepository;
        }

        public async Task<Response<bool>> Handle(DeleteSchoolNewsCommand request, CancellationToken cancellationToken)
        {
            var newsEntity = await _schoolNewsRepository.GetByIdAsync(request.Id);
            if (newsEntity == null)
            {
                return NotFound<bool>();
            }

            var deleted = await _schoolNewsRepository.DeleteAsync(request.Id);
            if (!deleted)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
