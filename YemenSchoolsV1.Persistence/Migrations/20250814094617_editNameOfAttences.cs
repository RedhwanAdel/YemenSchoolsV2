using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemenSchoolsV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editNameOfAttences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_AcademicYears_AcademicYearId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Sections_SectionId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Teachers_ClassTeacherId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceDetail_Attendance_AttendanceId",
                table: "AttendanceDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceDetail_Students_StudentId",
                table: "AttendanceDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceDetail",
                table: "AttendanceDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance");

            migrationBuilder.RenameTable(
                name: "AttendanceDetail",
                newName: "AttendanceDetails");

            migrationBuilder.RenameTable(
                name: "Attendance",
                newName: "Attendances");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceDetail_StudentId",
                table: "AttendanceDetails",
                newName: "IX_AttendanceDetails_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceDetail_AttendanceId",
                table: "AttendanceDetails",
                newName: "IX_AttendanceDetails_AttendanceId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_SectionId",
                table: "Attendances",
                newName: "IX_Attendances_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_ClassTeacherId",
                table: "Attendances",
                newName: "IX_Attendances_ClassTeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_AcademicYearId",
                table: "Attendances",
                newName: "IX_Attendances_AcademicYearId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceDetails",
                table: "AttendanceDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceDetails_Attendances_AttendanceId",
                table: "AttendanceDetails",
                column: "AttendanceId",
                principalTable: "Attendances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceDetails_Students_StudentId",
                table: "AttendanceDetails",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AcademicYears_AcademicYearId",
                table: "Attendances",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Sections_SectionId",
                table: "Attendances",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Teachers_ClassTeacherId",
                table: "Attendances",
                column: "ClassTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceDetails_Attendances_AttendanceId",
                table: "AttendanceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceDetails_Students_StudentId",
                table: "AttendanceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AcademicYears_AcademicYearId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Sections_SectionId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Teachers_ClassTeacherId",
                table: "Attendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceDetails",
                table: "AttendanceDetails");

            migrationBuilder.RenameTable(
                name: "Attendances",
                newName: "Attendance");

            migrationBuilder.RenameTable(
                name: "AttendanceDetails",
                newName: "AttendanceDetail");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_SectionId",
                table: "Attendance",
                newName: "IX_Attendance_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_ClassTeacherId",
                table: "Attendance",
                newName: "IX_Attendance_ClassTeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_AcademicYearId",
                table: "Attendance",
                newName: "IX_Attendance_AcademicYearId");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceDetails_StudentId",
                table: "AttendanceDetail",
                newName: "IX_AttendanceDetail_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceDetails_AttendanceId",
                table: "AttendanceDetail",
                newName: "IX_AttendanceDetail_AttendanceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceDetail",
                table: "AttendanceDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_AcademicYears_AcademicYearId",
                table: "Attendance",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Sections_SectionId",
                table: "Attendance",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Teachers_ClassTeacherId",
                table: "Attendance",
                column: "ClassTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceDetail_Attendance_AttendanceId",
                table: "AttendanceDetail",
                column: "AttendanceId",
                principalTable: "Attendance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceDetail_Students_StudentId",
                table: "AttendanceDetail",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
