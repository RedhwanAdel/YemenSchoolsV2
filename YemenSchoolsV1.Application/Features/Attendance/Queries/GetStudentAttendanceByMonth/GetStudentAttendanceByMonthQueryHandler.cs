using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Attendance;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Attendance.Queries.GetStudentAttendanceByMonth
{
    public class GetStudentAttendanceByMonthQueryHandler : ResponseHandler, IRequestHandler<GetStudentAttendanceByMonthQuery, Response<List<AttendanceDetailDto>>>
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetStudentAttendanceByMonthQueryHandler(IAttendanceRepository attendanceRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _attendanceRepository = attendanceRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<List<AttendanceDetailDto>>> Handle(GetStudentAttendanceByMonthQuery request, CancellationToken cancellationToken)
        {
            var startDate = new DateTime(request.Year, request.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var details = await _attendanceRepository.GetAll()
                .Include(a => a.AttendanceDetails)
                .SelectMany(a => a.AttendanceDetails)
                .Where(ad => ad.StudentId == request.StudentId &&
                             ad.CreatedAt.Date >= startDate.Date &&
                             ad.CreatedAt.Date <= endDate.Date)
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
