using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Attendance.Commands.CreateDailyAttendance
{
    public class CreateDailyAttendanceCommandHandler : IRequestHandler<CreateDailyAttendanceCommand, Response<Guid>>
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ISectionRepository _sectionRepository;

        public CreateDailyAttendanceCommandHandler(IAttendanceRepository attendanceRepository, ISectionRepository sectionRepository)
        {
            _attendanceRepository = attendanceRepository;
            _sectionRepository = sectionRepository;
        }

        public async Task<Response<Guid>> Handle(CreateDailyAttendanceCommand request, CancellationToken cancellationToken)
        {
            var existingAttendance = await _attendanceRepository.GetAttendanceByDateAndSectionAsync(request.Date, request.SectionId);
            if (existingAttendance != null)
            {
                return new Response<Guid>("Attendance record for this section and date already exists.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var section = await _sectionRepository.GetSectionByIdAsync(request.SectionId);
            if (section == null || section.ClassTeacherId != request.ClassTeacherId)
            {
                return new Response<Guid>("Teacher is not authorized to take attendance for this section.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                };
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

            return new Response<Guid>(result.Id, "Attendance created successfully")
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true
            };
        }
    }
}
