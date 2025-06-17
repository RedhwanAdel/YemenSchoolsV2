using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemenSchoolsV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editSubjectId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectGrades",
                table: "SubjectGrades");

            migrationBuilder.DropIndex(
                name: "IX_SubjectGrades_SubjectId",
                table: "SubjectGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignedSubjects",
                table: "AssignedSubjects");

            migrationBuilder.DropIndex(
                name: "IX_AssignedSubjects_SubjectId",
                table: "AssignedSubjects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SubjectGrades");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AssignedSubjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectGrades",
                table: "SubjectGrades",
                columns: new[] { "SubjectId", "GradeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignedSubjects",
                table: "AssignedSubjects",
                columns: new[] { "SubjectId", "SectionId", "TeacherId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectGrades",
                table: "SubjectGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignedSubjects",
                table: "AssignedSubjects");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "SubjectGrades",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AssignedSubjects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectGrades",
                table: "SubjectGrades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignedSubjects",
                table: "AssignedSubjects",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGrades_SubjectId",
                table: "SubjectGrades",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSubjects_SubjectId",
                table: "AssignedSubjects",
                column: "SubjectId");
        }
    }
}
