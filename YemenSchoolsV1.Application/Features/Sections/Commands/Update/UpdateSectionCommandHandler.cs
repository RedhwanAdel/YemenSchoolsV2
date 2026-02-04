using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Update
{
    public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Response<string>>
    {
        private readonly ISectionRepository _repository;

        public UpdateSectionCommandHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<string>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
             if (request.Dto == null)
                return new Response<string>("Section data is required.", false) { StatusCode = System.Net.HttpStatusCode.BadRequest };

            var section = new Section
            {
                Id = request.Id,
                Name = request.Dto.Name,
                AcademicYearId = request.Dto.AcademicYearId,
                SchoolGradeId = request.Dto.SchoolGradeId,
                Capacity = request.Dto.Capacity,
                ClassTeacherId = request.Dto.ClassTeacherId

            };

            var updated = await _repository.UpdateAsync(request.Id, section);
            if (updated == null)
                return new Response<string>("Section not found.", false) { StatusCode = System.Net.HttpStatusCode.NotFound };

            return new Response<string>("Section updated successfully.") { StatusCode = System.Net.HttpStatusCode.OK, Succeeded = true };
        }
    }
}
