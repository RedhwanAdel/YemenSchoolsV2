using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Attendance;
using Microsoft.EntityFrameworkCore;

namespace YemenSchoolsV1.Application.Features.Attendance.Queries.GetStudentAttendanceReport
{
    public class GetStudentAttendanceReportQueryHandler : IRequestHandler<GetStudentAttendanceReportQuery, Response<List<AttendanceDetailDto>>>
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public GetStudentAttendanceReportQueryHandler(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
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

            return new Response<List<AttendanceDetailDto>>(result, "Attendance report retrieved successfully")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
        }
    }
}
