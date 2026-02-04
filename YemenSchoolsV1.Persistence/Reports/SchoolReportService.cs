using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using YemenSchoolsV1.Application.Contracts;
using YemenSchoolsV1.Application.Dto;
namespace YemenSchoolsV1.Persistence.Reports
{
    public class SchoolReportService : ISchoolReportService
    {
        public SchoolReportService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GenerateSchoolReport(SchoolReportDto dto)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(11).FontColor(Colors.Black));

                    page.Content().Border(3).BorderColor(Colors.Blue.Medium).Padding(15).Column(col =>
                    {
                        // 🏫 رأس التقرير
                        col.Item().Background(Colors.Blue.Lighten5).Padding(10).Column(header =>
                        {
                            header.Item().Text($"تقرير المدرسة: {dto.NameAr}")
                                .FontSize(18).Bold().FontColor(Colors.Blue.Darken2).AlignCenter();

                            header.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Blue.Medium);

                            header.Item().Row(row =>
                            {
                                row.RelativeItem(2).Text($"الرمز البريدي: {dto.PostalCode ?? "-"}").AlignRight();
                                row.RelativeItem(2).Text($"المرحلة: {(SchoolLevelName(dto.SchoolLevel))}").AlignRight();
                                row.RelativeItem(2).Text($"النوع: {(GenderTypeName(dto.GenderType))}").AlignRight();
                            });
                        });

                        col.Item().PaddingVertical(10);

                        // 📍 معلومات الموقع
                        col.Item().Text("معلومات الموقع").FontSize(14).Bold().FontColor(Colors.Blue.Darken2).AlignRight();
                        col.Item().PaddingTop(5).Border(1).Padding(8).Column(info =>
                        {
                            info.Item().Text($"المدينة: {dto.CityNameAr}").AlignRight();
                            info.Item().Text($"المنطقة: {dto.RegionNameAr}").AlignRight();
                            info.Item().Text($"العنوان: {dto.AddressAr}").AlignRight();
                        });

                        col.Item().PaddingVertical(10);

                        // ☎️ بيانات التواصل
                        col.Item().Text("بيانات التواصل").FontSize(14).Bold().FontColor(Colors.Blue.Darken2).AlignRight();
                        col.Item().PaddingTop(5).Border(1).Padding(8).Column(contact =>
                        {
                            contact.Item().Text($"البريد الإلكتروني: {dto.Email ?? "-"}").AlignRight();
                            contact.Item().Text($"الهاتف الرئيسي: {dto.MainPhone ?? "-"}").AlignRight();

                            if (dto.PhoneNumbers.Any())
                            {
                                contact.Item().Text("أرقام إضافية:").AlignRight();
                                foreach (var phone in dto.PhoneNumbers)
                                    contact.Item().Text($"- {phone}").FontSize(10).AlignRight();
                            }
                        });

                        col.Item().PaddingVertical(10);

                        // 📊 الإحصائيات العامة
                        col.Item().Text("الإحصائيات العامة").FontSize(14).Bold().FontColor(Colors.Blue.Darken2).AlignRight();
                        col.Item().PaddingTop(5).Border(1).Padding(8).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(1);
                            });

                            void AddRow(string label, int value)
                            {
                                table.Cell().Border(1).Padding(5).Text(label).AlignRight();
                                table.Cell().Border(1).Padding(5).Text(value.ToString()).AlignCenter();
                            }

                            AddRow("عدد المعلمين", dto.TeachersCount);
                            AddRow("عدد الطلاب", dto.StudentsCount);
                            AddRow("عدد الصفوف الدراسية", dto.GradesCount);
                            AddRow("عدد المواد الدراسية", dto.SubjectsCount);
                            AddRow("عدد الأقسام", dto.SectionsCount);
                            AddRow("عدد الأعوام الدراسية", dto.AcademicYearsCount);
                            AddRow("عدد أولياء الأمور", dto.ParentsCount);
                        });

                        col.Item().PaddingVertical(10);

                        // 📰 قسم الأخبار والصور
                        col.Item().Text("المحتوى الإعلامي").FontSize(14).Bold().FontColor(Colors.Blue.Darken2).AlignRight();
                        col.Item().PaddingTop(5).Border(1).Padding(8).Row(media =>
                        {
                            media.RelativeItem().Text($"عدد الأخبار: {dto.NewsCount}").AlignRight();
                            media.RelativeItem().Text($"عدد الصور: {dto.PhotosCount}").AlignRight();
                            media.RelativeItem().Text($"عدد التقييمات: {dto.RatingsCount}").AlignRight();
                        });

                        // 📄 الوصف
                        if (!string.IsNullOrWhiteSpace(dto.DescriptionAr))
                        {
                            col.Item().PaddingVertical(10);
                            col.Item().Text("نبذة عن المدرسة").FontSize(14).Bold().FontColor(Colors.Blue.Darken2).AlignRight();
                            col.Item().PaddingTop(5).Background(Colors.Grey.Lighten4).Padding(8)
                                .Text(dto.DescriptionAr).FontSize(11).AlignRight();
                        }

                        // ✍️ التوقيعات
                        col.Item().PaddingTop(40).Row(signRow =>
                        {
                            signRow.RelativeItem().Text($"توقيع مدير المدرسة: ............................").AlignRight();
                            signRow.RelativeItem().Text($"توقيع المشرف العام: ............................").AlignRight();
                        });

                        // 📄 تذييل الصفحة
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

        // 🧩 تحويل القيم الرقمية إلى نصوص عربية
        private string SchoolLevelName(int level) => level switch
        {
            0 => "روضة",
            1 => "ابتدائي",
            2 => "متوسط",
            3 => "ثانوي",
            _ => "غير محدد"
        };

        private string GenderTypeName(int genderType) => genderType switch
        {
            0 => "بنين",
            1 => "بنات",
            2 => "مختلط",
            _ => "غير محدد"
        };
    }
}
