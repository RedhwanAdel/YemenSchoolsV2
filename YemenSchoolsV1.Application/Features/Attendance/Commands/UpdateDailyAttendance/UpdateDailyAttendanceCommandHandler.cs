using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Attendance.Commands.UpdateDailyAttendance
{
    public class UpdateDailyAttendanceCommandHandler : ResponseHandler, IRequestHandler<UpdateDailyAttendanceCommand, Response<string>>
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public UpdateDailyAttendanceCommandHandler(IAttendanceRepository attendanceRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _attendanceRepository = attendanceRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(UpdateDailyAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIdAsync(request.AttendanceId);
            if (attendance == null)
            {
                return NotFound<string>("Attendance record not found.");
            }

            foreach (var detail in attendance.AttendanceDetails)
            {
                if (request.NewStudentStatuses.TryGetValue(detail.StudentId, out var newStatus))
                {
                    detail.Status = newStatus;
                }
            }

            await _attendanceRepository.UpdateAttendanceDetailsAsync(attendance.AttendanceDetails.ToList());

            return Success("Attendance updated successfully.");
        }
    }
}
