using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YemenSchoolsV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seedStageGradData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StageGrade",
                columns: new[] { "Id", "GradeId", "StageId" },
                values: new object[,]
                {
                    { new Guid("aaaa1111-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111112") },
                    { new Guid("aaaa1111-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111112") },
                    { new Guid("aaaa1111-0000-0000-0000-000000000004"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("11111111-1111-1111-1111-111111111112") },
                    { new Guid("aaaa1111-0000-0000-0000-000000000005"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("11111111-1111-1111-1111-111111111112") },
                    { new Guid("aaaa1111-0000-0000-0000-000000000006"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("11111111-1111-1111-1111-111111111112") },
                    { new Guid("aaaa1111-0000-0000-0000-000000000007"), new Guid("66666666-6666-6666-6666-666666666666"), new Guid("11111111-1111-1111-1111-111111111112") },
                    { new Guid("aaaa1111-0000-0000-0000-000000000008"), new Guid("77777777-7777-7777-7777-777777777777"), new Guid("22222222-2222-2222-2222-222222222223") },
                    { new Guid("aaaa1111-0000-0000-0000-000000000009"), new Guid("88888888-8888-8888-8888-888888888888"), new Guid("22222222-2222-2222-2222-222222222223") },
                    { new Guid("aaaa1111-0000-0000-0000-000000000010"), new Guid("99999999-9999-9999-9999-999999999999"), new Guid("22222222-2222-2222-2222-222222222223") }
                });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222783"), "الروضة" });

            migrationBuilder.InsertData(
                table: "StageGrade",
                columns: new[] { "Id", "GradeId", "StageId" },
                values: new object[] { new Guid("aaaa1111-0000-0000-0000-000000000001"), new Guid("44444444-4444-4444-4444-444444444445"), new Guid("22222222-2222-2222-2222-222222222783") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StageGrade",
                keyColumn: "Id",
                keyValue: new Guid("aaaa1111-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "StageGrade",
                keyColumn: "Id",
                keyValue: new Guid("aaaa1111-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "StageGrade",
                keyColumn: "Id",
                keyValue: new Guid("aaaa1111-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "StageGrade",
                keyColumn: "Id",
                keyValue: new Guid("aaaa1111-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "StageGrade",
                keyColumn: "Id",
                keyValue: new Guid("aaaa1111-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "StageGrade",
                keyColumn: "Id",
                keyValue: new Guid("aaaa1111-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "StageGrade",
                keyColumn: "Id",
                keyValue: new Guid("aaaa1111-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "StageGrade",
                keyColumn: "Id",
                keyValue: new Guid("aaaa1111-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "StageGrade",
                keyColumn: "Id",
                keyValue: new Guid("aaaa1111-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "StageGrade",
                keyColumn: "Id",
                keyValue: new Guid("aaaa1111-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222783"));
        }
    }
}
