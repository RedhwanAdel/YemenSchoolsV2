using MediatR;
using Microsoft.Extensions.Logging;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Students.Commands.AddParentToStudent
{
    public class AddParentToStudentCommandHandler : IRequestHandler<AddParentToStudentCommand, (bool Succeeded, string Message)>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IParentRepository _parentRepository;
        private readonly ILogger<AddParentToStudentCommandHandler> _logger;

        public AddParentToStudentCommandHandler(
            IStudentRepository studentRepository,
            IParentRepository parentRepository,
            ILogger<AddParentToStudentCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _parentRepository = parentRepository;
            _logger = logger;
        }

        public async Task<(bool Succeeded, string Message)> Handle(AddParentToStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId);
            var parent = await _parentRepository.GetByIdAsync(request.ParentId);

            if (student == null || parent == null)
            {
                return (false, "الطالب أو ولي الأمر غير موجود.");
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
                return (true, "تم ربط ولي الأمر بالطالب بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add parent to student relationship.");
                return (false, "فشل ربط ولي الأمر بالطالب.");
            }
        }
    }
}
