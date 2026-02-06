using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Students.Commands.RemoveParentFromStudent
{
    public class RemoveParentFromStudentCommandHandler : ResponseHandler, IRequestHandler<RemoveParentFromStudentCommand, Response<string>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<RemoveParentFromStudentCommandHandler> _logger;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public RemoveParentFromStudentCommandHandler(
            IStudentRepository studentRepository,
            ILogger<RemoveParentFromStudentCommandHandler> logger,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(RemoveParentFromStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _studentRepository.RemoveParentFromStudentAsync(request.StudentId, request.ParentId);
                return Success("تم إزالة علاقة ولي الأمر بالطالب بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to remove parent from student relationship.");
                return BadRequest<string>("فشل إزالة علاقة ولي الأمر بالطالب.");
            }
        }
    }
}
