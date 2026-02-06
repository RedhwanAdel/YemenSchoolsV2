using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Attendance.Commands.CreateDailyAttendance
{
    public class CreateDailyAttendanceCommandHandler : ResponseHandler, IRequestHandler<CreateDailyAttendanceCommand, Response<Guid>>
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public CreateDailyAttendanceCommandHandler(IAttendanceRepository attendanceRepository, ISectionRepository sectionRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _attendanceRepository = attendanceRepository;
            _sectionRepository = sectionRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<Guid>> Handle(CreateDailyAttendanceCommand request, CancellationToken cancellationToken)
        {
            var existingAttendance = await _attendanceRepository.GetAttendanceByDateAndSectionAsync(request.Date, request.SectionId);
            if (existingAttendance != null)
            {
                return BadRequest<Guid>("Attendance record for this section and date already exists.");
            }

            var section = await _sectionRepository.GetSectionByIdAsync(request.SectionId);
            if (section == null || section.ClassTeacherId != request.ClassTeacherId)
            {
                return Unauthorized<Guid>("Teacher is not authorized to take attendance for this section.");
            }

            var attendance = new YemenSchoolsV1.Domain.Entities.Attendance
            {
                Date = request.Date,
                SectionId = request.SectionId,
                ClassTeacherId = request.ClassTeacherId,
                AcademicYearId = section.AcademicYearId
            };

            var attendanceDetails = request.StudentStatuses.Select(s => new AttendanceDetail
            {
                StudentId = s.Key,
                Status = s.Value
            }).ToList();

            attendance.AttendanceDetails = attendanceDetails;

            var result = await _attendanceRepository.CreateAttendanceAsync(attendance);

            return Created(result.Id);
        }
    }
}
