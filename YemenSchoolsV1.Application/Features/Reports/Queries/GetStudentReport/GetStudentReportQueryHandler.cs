using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Reports.Queries.GetStudentReport
{
    public class GetStudentReportQueryHandler : IRequestHandler<GetStudentReportQuery, Response<FileResponse>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentReportService _reportService;

        public GetStudentReportQueryHandler(IStudentRepository studentRepository, IStudentReportService reportService)
        {
            _studentRepository = studentRepository;
            _reportService = reportService;
        }

        public async Task<Response<FileResponse>> Handle(GetStudentReportQuery request, CancellationToken cancellationToken)
        {
            // 1. Get Student Logic
            var student = await _studentRepository.GetStudentWithDetailsAsync(request.StudentId);
            if (student == null)
                return new Response<FileResponse>("Student not found") { StatusCode = System.Net.HttpStatusCode.NotFound, Succeeded = false };

            // 2. Aggregate Marks Logic
            var groupedMarks = student.Marks
                .GroupBy(m => new { m.SectionSubject.GradeSubject.Subject.Name })
                .Select(g =>
                {
                    int totalScore = (int)g.Sum(m => m.Score);
                    return new StudentSubjectReportDto
                    {
                        SubjectName = g.Key.Name,
                        Score = totalScore,
                        Grade = totalScore >= 90 ? "ممتاز" :
                                totalScore >= 80 ? "جيد جدًا" :
                                totalScore >= 70 ? "جيد" :
                                totalScore >= 60 ? "مقبول" : "ضعيف"
                    };
                })
                .ToList();

            // 3. Map to DTO
            var dto = new StudentReportDto
            {
                StudentId = student.Id,
                StudentNameAr = student.NameAr,
                StudentNameEn = student.NameEn,
                SchoolName = student.School.NameAr,
                SchoolLogoUrl = student.School.Logo,
                GradeName = student.CurrentSection.SchoolGrade.StageGrade.Grade.Name,
                SectionName = student.CurrentSection.Name,
                StageName = student.CurrentSection.SchoolGrade.StageGrade.Stage.Name,
                Subjects = groupedMarks,
                TotalAttendanceDays = student.AttendanceDetails.Count(ad => ad.Status == AttendanceStatus.Present),
                TotalAbsenceDays = student.AttendanceDetails.Count(ad => ad.Status != AttendanceStatus.Present),
                AttendancePercentage = student.AttendanceDetails.Any() ?
                                       (double)student.AttendanceDetails.Count(ad => ad.Status == AttendanceStatus.Present)
                                       / student.AttendanceDetails.Count * 100 : 0
            };

            // 4. Generate PDF
            var pdfBytes = _reportService.GenerateStudentReport(dto);

            // 5. Return FileResponse
            var fileResponse = new FileResponse(pdfBytes, "application/pdf", $"StudentReport_{student.NameAr}.pdf");
            return new Response<FileResponse>(fileResponse);
        }
    }
}
