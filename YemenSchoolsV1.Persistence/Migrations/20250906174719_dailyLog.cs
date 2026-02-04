using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemenSchoolsV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dailyLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Teachers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "DailyLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonCovered = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    HomeworkAssigned = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TeacherNotes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SectionSubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyLogs_SectionSubject_SectionSubjectId",
                        column: x => x.SectionSubjectId,
                        principalTable: "SectionSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyLogs_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyLogs_SectionSubjectId_Date",
                table: "DailyLogs",
                columns: new[] { "SectionSubjectId", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_DailyLogs_TeacherId",
                table: "DailyLogs",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyLogs");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Teachers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
