using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemenSchoolsV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StandardizeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyLogs_SectionSubject_SectionSubjectId",
                table: "DailyLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeSubject_SchoolGrade_SchoolGradeId",
                table: "GradeSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeSubject_Subjects_SubjectId",
                table: "GradeSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_SectionSubject_SectionSubjectId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Citys_CityId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolGrade_Schools_SchoolId",
                table: "SchoolGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolGrade_StageGrade_StageGradeId",
                table: "SchoolGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_schoolReviews_AspNetUsers_UserId",
                table: "schoolReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_schoolReviews_Schools_SchoolId",
                table: "schoolReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Citys_CityId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_SchoolGrade_SchoolGradeId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSubject_GradeSubject_GradeSubjectId",
                table: "SectionSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSubject_Sections_SectionId",
                table: "SectionSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSubject_Teachers_TeacherId",
                table: "SectionSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSubject_Terms_TermId",
                table: "SectionSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_StageGrade_Grades_GradeId",
                table: "StageGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_StageGrade_Stages_StageId",
                table: "StageGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_schoolReviews",
                table: "schoolReviews");

            migrationBuilder.DropIndex(
                name: "IX_schoolReviews_SchoolId",
                table: "schoolReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StageGrade",
                table: "StageGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionSubject",
                table: "SectionSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolGrade",
                table: "SchoolGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GradeSubject",
                table: "GradeSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Citys",
                table: "Citys");

            migrationBuilder.RenameTable(
                name: "schoolReviews",
                newName: "SchoolReviews");

            migrationBuilder.RenameTable(
                name: "StageGrade",
                newName: "StageGrades");

            migrationBuilder.RenameTable(
                name: "SectionSubject",
                newName: "SectionSubjects");

            migrationBuilder.RenameTable(
                name: "SchoolGrade",
                newName: "SchoolGrades");

            migrationBuilder.RenameTable(
                name: "GradeSubject",
                newName: "GradeSubjects");

            migrationBuilder.RenameTable(
                name: "Citys",
                newName: "Cities");

            migrationBuilder.RenameIndex(
                name: "IX_schoolReviews_UserId",
                table: "SchoolReviews",
                newName: "IX_SchoolReviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StageGrade_StageId",
                table: "StageGrades",
                newName: "IX_StageGrades_StageId");

            migrationBuilder.RenameIndex(
                name: "IX_StageGrade_GradeId",
                table: "StageGrades",
                newName: "IX_StageGrades_GradeId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSubject_TermId",
                table: "SectionSubjects",
                newName: "IX_SectionSubjects_TermId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSubject_TeacherId",
                table: "SectionSubjects",
                newName: "IX_SectionSubjects_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSubject_SectionId",
                table: "SectionSubjects",
                newName: "IX_SectionSubjects_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSubject_GradeSubjectId",
                table: "SectionSubjects",
                newName: "IX_SectionSubjects_GradeSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolGrade_StageGradeId",
                table: "SchoolGrades",
                newName: "IX_SchoolGrades_StageGradeId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolGrade_SchoolId",
                table: "SchoolGrades",
                newName: "IX_SchoolGrades_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_GradeSubject_SubjectId",
                table: "GradeSubjects",
                newName: "IX_GradeSubjects_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_GradeSubject_SchoolGradeId",
                table: "GradeSubjects",
                newName: "IX_GradeSubjects_SchoolGradeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Terms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolReviews",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "SchoolReviews",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AcademicYears",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolReviews",
                table: "SchoolReviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StageGrades",
                table: "StageGrades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionSubjects",
                table: "SectionSubjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolGrades",
                table: "SchoolGrades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradeSubjects",
                table: "GradeSubjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolReviews_SchoolId_UserId",
                table: "SchoolReviews",
                columns: new[] { "SchoolId", "UserId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyLogs_SectionSubjects_SectionSubjectId",
                table: "DailyLogs",
                column: "SectionSubjectId",
                principalTable: "SectionSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeSubjects_SchoolGrades_SchoolGradeId",
                table: "GradeSubjects",
                column: "SchoolGradeId",
                principalTable: "SchoolGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeSubjects_Subjects_SubjectId",
                table: "GradeSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_SectionSubjects_SectionSubjectId",
                table: "Marks",
                column: "SectionSubjectId",
                principalTable: "SectionSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Cities_CityId",
                table: "Regions",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolGrades_Schools_SchoolId",
                table: "SchoolGrades",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolGrades_StageGrades_StageGradeId",
                table: "SchoolGrades",
                column: "StageGradeId",
                principalTable: "StageGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolReviews_AspNetUsers_UserId",
                table: "SchoolReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolReviews_Schools_SchoolId",
                table: "SchoolReviews",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Cities_CityId",
                table: "Schools",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_SchoolGrades_SchoolGradeId",
                table: "Sections",
                column: "SchoolGradeId",
                principalTable: "SchoolGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSubjects_GradeSubjects_GradeSubjectId",
                table: "SectionSubjects",
                column: "GradeSubjectId",
                principalTable: "GradeSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSubjects_Sections_SectionId",
                table: "SectionSubjects",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSubjects_Teachers_TeacherId",
                table: "SectionSubjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSubjects_Terms_TermId",
                table: "SectionSubjects",
                column: "TermId",
                principalTable: "Terms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StageGrades_Grades_GradeId",
                table: "StageGrades",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StageGrades_Stages_StageId",
                table: "StageGrades",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyLogs_SectionSubjects_SectionSubjectId",
                table: "DailyLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeSubjects_SchoolGrades_SchoolGradeId",
                table: "GradeSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeSubjects_Subjects_SubjectId",
                table: "GradeSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_SectionSubjects_SectionSubjectId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Cities_CityId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolGrades_Schools_SchoolId",
                table: "SchoolGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolGrades_StageGrades_StageGradeId",
                table: "SchoolGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolReviews_AspNetUsers_UserId",
                table: "SchoolReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolReviews_Schools_SchoolId",
                table: "SchoolReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Cities_CityId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_SchoolGrades_SchoolGradeId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSubjects_GradeSubjects_GradeSubjectId",
                table: "SectionSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSubjects_Sections_SectionId",
                table: "SectionSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSubjects_Teachers_TeacherId",
                table: "SectionSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSubjects_Terms_TermId",
                table: "SectionSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StageGrades_Grades_GradeId",
                table: "StageGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_StageGrades_Stages_StageId",
                table: "StageGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolReviews",
                table: "SchoolReviews");

            migrationBuilder.DropIndex(
                name: "IX_SchoolReviews_SchoolId_UserId",
                table: "SchoolReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StageGrades",
                table: "StageGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionSubjects",
                table: "SectionSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolGrades",
                table: "SchoolGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GradeSubjects",
                table: "GradeSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.RenameTable(
                name: "SchoolReviews",
                newName: "schoolReviews");

            migrationBuilder.RenameTable(
                name: "StageGrades",
                newName: "StageGrade");

            migrationBuilder.RenameTable(
                name: "SectionSubjects",
                newName: "SectionSubject");

            migrationBuilder.RenameTable(
                name: "SchoolGrades",
                newName: "SchoolGrade");

            migrationBuilder.RenameTable(
                name: "GradeSubjects",
                newName: "GradeSubject");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "Citys");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolReviews_UserId",
                table: "schoolReviews",
                newName: "IX_schoolReviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StageGrades_StageId",
                table: "StageGrade",
                newName: "IX_StageGrade_StageId");

            migrationBuilder.RenameIndex(
                name: "IX_StageGrades_GradeId",
                table: "StageGrade",
                newName: "IX_StageGrade_GradeId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSubjects_TermId",
                table: "SectionSubject",
                newName: "IX_SectionSubject_TermId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSubjects_TeacherId",
                table: "SectionSubject",
                newName: "IX_SectionSubject_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSubjects_SectionId",
                table: "SectionSubject",
                newName: "IX_SectionSubject_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionSubjects_GradeSubjectId",
                table: "SectionSubject",
                newName: "IX_SectionSubject_GradeSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolGrades_StageGradeId",
                table: "SchoolGrade",
                newName: "IX_SchoolGrade_StageGradeId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolGrades_SchoolId",
                table: "SchoolGrade",
                newName: "IX_SchoolGrade_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_GradeSubjects_SubjectId",
                table: "GradeSubject",
                newName: "IX_GradeSubject_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_GradeSubjects_SchoolGradeId",
                table: "GradeSubject",
                newName: "IX_GradeSubject_SchoolGradeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Terms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "schoolReviews",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "schoolReviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AcademicYears",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_schoolReviews",
                table: "schoolReviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StageGrade",
                table: "StageGrade",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionSubject",
                table: "SectionSubject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolGrade",
                table: "SchoolGrade",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradeSubject",
                table: "GradeSubject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Citys",
                table: "Citys",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_schoolReviews_SchoolId",
                table: "schoolReviews",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyLogs_SectionSubject_SectionSubjectId",
                table: "DailyLogs",
                column: "SectionSubjectId",
                principalTable: "SectionSubject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeSubject_SchoolGrade_SchoolGradeId",
                table: "GradeSubject",
                column: "SchoolGradeId",
                principalTable: "SchoolGrade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeSubject_Subjects_SubjectId",
                table: "GradeSubject",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_SectionSubject_SectionSubjectId",
                table: "Marks",
                column: "SectionSubjectId",
                principalTable: "SectionSubject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Citys_CityId",
                table: "Regions",
                column: "CityId",
                principalTable: "Citys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolGrade_Schools_SchoolId",
                table: "SchoolGrade",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolGrade_StageGrade_StageGradeId",
                table: "SchoolGrade",
                column: "StageGradeId",
                principalTable: "StageGrade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_schoolReviews_AspNetUsers_UserId",
                table: "schoolReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_schoolReviews_Schools_SchoolId",
                table: "schoolReviews",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Citys_CityId",
                table: "Schools",
                column: "CityId",
                principalTable: "Citys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_SchoolGrade_SchoolGradeId",
                table: "Sections",
                column: "SchoolGradeId",
                principalTable: "SchoolGrade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSubject_GradeSubject_GradeSubjectId",
                table: "SectionSubject",
                column: "GradeSubjectId",
                principalTable: "GradeSubject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSubject_Sections_SectionId",
                table: "SectionSubject",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSubject_Teachers_TeacherId",
                table: "SectionSubject",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSubject_Terms_TermId",
                table: "SectionSubject",
                column: "TermId",
                principalTable: "Terms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StageGrade_Grades_GradeId",
                table: "StageGrade",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StageGrade_Stages_StageId",
                table: "StageGrade",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
