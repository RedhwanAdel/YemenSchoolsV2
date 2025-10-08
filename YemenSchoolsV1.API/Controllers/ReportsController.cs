using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.API.Controllers
{
    public class ReportsController : AppControllerBase
    {
        private readonly IStudentReportService _reportService;
        private readonly IStudentRepository _studentRepository;
        private readonly ISchoolReportService _schoolReportService;
        private readonly ISchoolRepositry _schoolService;

        public ReportsController(
            IStudentReportService reportService,
            IStudentRepository studentRepository,
            ISchoolReportService _schoolReportService,
            ISchoolRepositry schoolService)
        {
            _reportService = reportService;
            _studentRepository = studentRepository;
            this._schoolReportService = _schoolReportService;
            _schoolService = schoolService;
        }

        [HttpPost("student/{id}")]
        public async Task<IActionResult> GetStudentReport(Guid id)
        {
            // 1️⃣ جلب بيانات الطالب مع الدرجات والحضور من قاعدة البيانات (كما هو)
            var student = await _studentRepository.GetStudentWithDetailsAsync(id);
            if (student == null)
                return NotFound();

            // ⭐ التعديل الجديد: تجميع الدرجات حسب المادة وحساب المجموع والتقييم النهائي
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

            // 2️⃣ تحويل بيانات الطالب إلى DTO
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
                // استخدام الدرجات المجمعة هنا
                Subjects = groupedMarks,
                TotalAttendanceDays = student.AttendanceDetails.Count(ad => ad.Status == AttendanceStatus.Present),
                TotalAbsenceDays = student.AttendanceDetails.Count(ad => ad.Status != AttendanceStatus.Present),
                AttendancePercentage = student.AttendanceDetails.Any() ?
                                       (double)student.AttendanceDetails.Count(ad => ad.Status == AttendanceStatus.Present)
                                       / student.AttendanceDetails.Count * 100 : 0
            };

            // 3️⃣ توليد PDF باستخدام الخدمة
            var pdfBytes = _reportService.GenerateStudentReport(dto);

            // 4️⃣ إعادة الملف كـ PDF
            return File(pdfBytes, "application/pdf", $"StudentReport_{student.NameAr}.pdf");
        }

        [HttpPost("school/{schoolId:guid}")]
        public async Task<IActionResult> GetSchoolReport(Guid schoolId)
        {
            // 1️⃣ الحصول على بيانات المدرسة
            var schoolDto = await _schoolService.GetSchoolReportAsync(schoolId);

            if (schoolDto == null)
                return NotFound(new { Message = "المدرسة غير موجودة." });

            // 2️⃣ توليد ملف PDF باستخدام QuestPDF
            var pdfBytes = _schoolReportService.GenerateSchoolReport(schoolDto);

            // 3️⃣ إعادة الملف كنتيجة
            var fileName = $"SchoolReport_{schoolDto.NameEn ?? "School"}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}
