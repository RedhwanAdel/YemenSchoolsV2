using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using YemenSchoolsV1.Application.Contracts;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Persistence.Reports
{
    public class StudentReportService : IStudentReportService
    {
        public StudentReportService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GenerateStudentReport(StudentReportDto dto)
        {
            // حساب المجموع الكلي والتقدير العام
            int totalMaxScore = dto.Subjects.Count * 100;
            int totalObtainedScore = dto.Subjects.Sum(s => s.Score);
            double overallPercentage = totalMaxScore > 0 ? (double)totalObtainedScore / totalMaxScore * 100 : 0;

            string overallGrade = overallPercentage >= 90 ? "ممتاز" :
                                  overallPercentage >= 80 ? "جيد جدًا" :
                                  overallPercentage >= 70 ? "جيد" :
                                  overallPercentage >= 60 ? "مقبول" : "ضعيف / راسب";

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(11).FontColor(Colors.Black));

                    // إطار خارجي للتقرير
                    page.Content().Border(3).BorderColor(Colors.Blue.Medium).Padding(15).Column(col =>
                    {
                        // 1. رأس التقرير (البيانات الرئيسية)
                        col.Item().Background(Colors.Blue.Lighten5).Padding(8).Column(headerCol =>
                        {
                            // عنوان التقرير واسم المدرسة
                            headerCol.Item().Text($"تقرير درجات الطالب ({dto.SchoolName})")
                                .FontSize(18).Bold().FontColor(Colors.Blue.Darken2).AlignCenter();

                            // ⭐⭐ التصحيح: تطبيق PaddingVertical على Item() قبل LineHorizontal()
                            headerCol.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Blue.Medium);

                            // تفاصيل الطالب في صف واحد
                            headerCol.Item().Row(row =>
                            {
                                row.RelativeItem(3).Text($"الطالب: {dto.StudentNameAr}").SemiBold().AlignRight();
                                row.RelativeItem(2).Text($"الصف: {dto.GradeName}").AlignRight();
                                row.RelativeItem(2).Text($"الشعبة: {dto.SectionName}").AlignRight();
                            });
                            headerCol.Item().PaddingTop(3).Text($"المرحلة التعليمية: {dto.StageName}").AlignRight();
                        });

                        col.Item().PaddingVertical(10); // فاصل

                        // 2. جدول الدرجات (نتائج المواد)
                        col.Item().Text("نتائج المواد الدراسية").FontSize(14).Bold().FontColor(Colors.Blue.Darken2).AlignRight();
                        col.Item().PaddingTop(5).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3); // المادة
                                columns.RelativeColumn(1.5f); // الدرجة (المجموع)
                                columns.RelativeColumn(1.5f); // التقييم
                            });

                            // رأس الجدول
                            table.Header(header =>
                            {
                                header.Cell().Border(1).Background(Colors.Blue.Lighten4).Padding(5).Text("المادة").Bold().AlignRight();
                                header.Cell().Border(1).Background(Colors.Blue.Lighten4).Padding(5).Text("الدرجة (من 100)").Bold().AlignCenter();
                                header.Cell().Border(1).Background(Colors.Blue.Lighten4).Padding(5).Text("التقييم الخاص بالمادة").Bold().AlignCenter();
                            });

                            // صفوف البيانات
                            foreach (var subject in dto.Subjects)
                            {
                                table.Cell().Border(1).Padding(5).Text(subject.SubjectName).AlignRight();
                                table.Cell().Border(1).Padding(5).Text(subject.Score.ToString()).AlignCenter();
                                table.Cell().Border(1).Padding(5).Text(subject.Grade).AlignCenter();
                            }
                        });

                        col.Item().PaddingVertical(10); // فاصل

                        // 3. ملخص النتيجة النهائية (المجموع والتقدير العام)
                        col.Item().Text("ملخص النتيجة العامة").FontSize(14).Bold().FontColor(Colors.Blue.Darken2).AlignRight();
                        col.Item().PaddingTop(5).Background(Colors.Yellow.Lighten5).Border(1).Padding(8).Row(summaryRow =>
                        {
                            summaryRow.RelativeItem(2).Text($"المجموع الكلي المحصل: {totalObtainedScore} / {totalMaxScore}")
                                .FontSize(12).Bold().AlignRight();

                            summaryRow.RelativeItem(2).Text($"النسبة المئوية: {overallPercentage:F2}%")
                                .FontSize(12).Bold().AlignRight();

                            summaryRow.RelativeItem(2).Text($"التقدير العام: {overallGrade}")
                                .FontSize(12).Bold().FontColor(overallPercentage >= 60 ? Colors.Green.Medium : Colors.Red.Medium).AlignRight();
                        });

                        col.Item().PaddingVertical(10); // فاصل

                        // 4. ملخص الحضور والغياب
                        col.Item().Text("بيانات الحضور والغياب").FontSize(14).Bold().FontColor(Colors.Blue.Darken2).AlignRight();
                        col.Item().PaddingTop(5).Border(1).Padding(8).Row(row =>
                        {
                            row.RelativeItem().Text($"إجمالي أيام الحضور: {dto.TotalAttendanceDays} أيام").FontSize(11).AlignRight();
                            row.RelativeItem().Text($"إجمالي أيام الغياب: {dto.TotalAbsenceDays} أيام").FontSize(11).FontColor(Colors.Red.Medium).AlignRight();
                            row.RelativeItem().Text($"نسبة الحضور: {dto.AttendancePercentage:F0}%").FontSize(11).Bold().AlignRight();
                        });

                        // 5. الخاتمة والتوقيعات
                        col.Item().PaddingTop(50).Row(signRow =>
                        {
                            signRow.RelativeItem().Text($"توقيع ولي الأمر: ............................").FontSize(10).AlignRight();
                            signRow.RelativeItem().Text($"توقيع مدير المدرسة: ............................").FontSize(10).AlignRight();
                        });

                        // تذييل الصفحة 
                        page.Footer().AlignRight().Text(x =>
                        {
                            x.DefaultTextStyle(style => style.FontSize(9).FontColor(Colors.Grey.Medium));
                            x.Span("الصفحة ");
                            x.CurrentPageNumber();
                            x.Span(" من ");
                            x.TotalPages();
                        });
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
