using MediatR;
using Microsoft.Extensions.Logging;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Students.Commands.RemoveParentFromStudent
{
    public class RemoveParentFromStudentCommandHandler : IRequestHandler<RemoveParentFromStudentCommand, (bool Succeeded, string Message)>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<RemoveParentFromStudentCommandHandler> _logger;

        public RemoveParentFromStudentCommandHandler(
            IStudentRepository studentRepository,
            ILogger<RemoveParentFromStudentCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task<(bool Succeeded, string Message)> Handle(RemoveParentFromStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _studentRepository.RemoveParentFromStudentAsync(request.StudentId, request.ParentId);
                return (true, "تم إزالة علاقة ولي الأمر بالطالب بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to remove parent from student relationship.");
                return (false, "فشل إزالة علاقة ولي الأمر بالطالب.");
            }
        }
    }
}
