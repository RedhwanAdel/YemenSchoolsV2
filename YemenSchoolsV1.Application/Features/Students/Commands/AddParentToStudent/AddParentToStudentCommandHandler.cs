using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Students.Commands.AddParentToStudent
{
    public class AddParentToStudentCommandHandler : ResponseHandler, IRequestHandler<AddParentToStudentCommand, Response<string>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IParentRepository _parentRepository;
        private readonly ILogger<AddParentToStudentCommandHandler> _logger;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AddParentToStudentCommandHandler(
            IStudentRepository studentRepository,
            IParentRepository parentRepository,
            ILogger<AddParentToStudentCommandHandler> logger,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentRepository = studentRepository;
            _parentRepository = parentRepository;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddParentToStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId);
            var parent = await _parentRepository.GetByIdAsync(request.ParentId);

            if (student == null || parent == null)
            {
                return NotFound<string>("الطالب أو ولي الأمر غير موجود.");
            }

            var newParentStudent = new ParentStudent
            {
                StudentId = request.StudentId,
                ParentId = request.ParentId,
                RelationType = request.RelationType
            };

            try
            {
                await _studentRepository.AddParentToStudentAsync(newParentStudent);
                return Success("تم ربط ولي الأمر بالطالب بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add parent to student relationship.");
                return BadRequest<string>("فشل ربط ولي الأمر بالطالب.");
            }
        }
    }
}
