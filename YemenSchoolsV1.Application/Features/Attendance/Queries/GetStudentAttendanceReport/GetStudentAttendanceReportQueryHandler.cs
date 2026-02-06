using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Attendance;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Attendance.Queries.GetStudentAttendanceReport
{
    public class GetStudentAttendanceReportQueryHandler : ResponseHandler, IRequestHandler<GetStudentAttendanceReportQuery, Response<List<AttendanceDetailDto>>>
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetStudentAttendanceReportQueryHandler(IAttendanceRepository attendanceRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _attendanceRepository = attendanceRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<List<AttendanceDetailDto>>> Handle(GetStudentAttendanceReportQuery request, CancellationToken cancellationToken)
        {
            var details = await _attendanceRepository.GetAll()
                .Include(a => a.AttendanceDetails)
                .Where(a => a.AttendanceDetails.Any(ad => ad.StudentId == request.StudentId))
                .SelectMany(a => a.AttendanceDetails)
                .Where(ad => ad.StudentId == request.StudentId)
                .ToListAsync(cancellationToken);

            var result = details.Select(d => new AttendanceDetailDto
            {
                Id = d.Id,
                AttendanceId = d.AttendanceId,
                StudentId = d.StudentId,
                Status = d.Status,
                Notes = d.Notes,
                CreatedAt = d.CreatedAt
            }).ToList();

            return Success(result);
        }
    }
}
