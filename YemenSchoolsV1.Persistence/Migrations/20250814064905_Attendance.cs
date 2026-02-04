using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemenSchoolsV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Attendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClassTeacherId",
                table: "Sections",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDayOff = table.Column<bool>(type: "bit", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassTeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_Teachers_ClassTeacherId",
                        column: x => x.ClassTeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceDetail_Attendance_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceDetail_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ClassTeacherId",
                table: "Sections",
                column: "ClassTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_AcademicYearId",
                table: "Attendance",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_ClassTeacherId",
                table: "Attendance",
                column: "ClassTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_SectionId",
                table: "Attendance",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceDetail_AttendanceId",
                table: "AttendanceDetail",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceDetail_StudentId",
                table: "AttendanceDetail",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Teachers_ClassTeacherId",
                table: "Sections",
                column: "ClassTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Teachers_ClassTeacherId",
                table: "Sections");

            migrationBuilder.DropTable(
                name: "AttendanceDetail");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Sections_ClassTeacherId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "ClassTeacherId",
                table: "Sections");
        }
    }
}
