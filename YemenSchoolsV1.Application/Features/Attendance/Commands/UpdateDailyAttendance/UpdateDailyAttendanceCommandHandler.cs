using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Attendance.Commands.UpdateDailyAttendance
{
    public class UpdateDailyAttendanceCommandHandler : IRequestHandler<UpdateDailyAttendanceCommand, Response<string>>
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public UpdateDailyAttendanceCommandHandler(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<Response<string>> Handle(UpdateDailyAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIdAsync(request.AttendanceId);
            if (attendance == null)
            {
                return new Response<string>("Attendance record not found.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            foreach (var detail in attendance.AttendanceDetails)
            {
                if (request.NewStudentStatuses.TryGetValue(detail.StudentId, out var newStatus))
                {
                    detail.Status = newStatus;
                }
            }

            await _attendanceRepository.UpdateAttendanceDetailsAsync(attendance.AttendanceDetails.ToList());

            return new Response<string>("Attendance updated successfully.", true)
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
