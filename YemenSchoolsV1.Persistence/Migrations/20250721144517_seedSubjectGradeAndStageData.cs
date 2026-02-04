using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YemenSchoolsV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seedSubjectGradeAndStageData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "الصف الأول" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "الصف الثاني" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "الصف الثالث" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "الصف الرابع" },
                    { new Guid("44444444-4444-4444-4444-444444444445"), "KG" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "الصف الخامس" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "الصف السادس" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "الصف السابع" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "الصف الثامن" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "الصف التاسع" }
                });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111112"), "المرحلة الإبتدائية" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), "المرحلة الإعدادية" },
                    { new Guid("33333333-3333-3333-3333-333333333334"), "المرحلة الثانوية" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "القرآن الكريم" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "التربية الإسلامية" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "اللغة العربية" },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "الرياضيات" },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "العلوم" },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "الاجتماعيات" },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "اللغة الإنجليزية" },
                    { new Guid("10000000-0000-0000-0000-000000000008"), "التاريخ" },
                    { new Guid("10000000-0000-0000-0000-000000000009"), "الجغرافيا" },
                    { new Guid("10000000-0000-0000-0000-000000000010"), "الوطنية" },
                    { new Guid("10000000-0000-0000-0000-000000000011"), "الجبر" },
                    { new Guid("10000000-0000-0000-0000-000000000012"), "الهندسة" },
                    { new Guid("10000000-0000-0000-0000-000000000013"), "الكيمياء" },
                    { new Guid("10000000-0000-0000-0000-000000000014"), "الأحياء" },
                    { new Guid("10000000-0000-0000-0000-000000000015"), "الفيزياء" },
                    { new Guid("10000000-0000-0000-0000-000000000016"), "الرسم" },
                    { new Guid("10000000-0000-0000-0000-000000000017"), "الحاسوب" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444445"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"));

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"));

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000017"));
        }
    }
}
