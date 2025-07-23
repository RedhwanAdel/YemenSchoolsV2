using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemenSchoolsV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editScoolMangmentAnlaysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYears_Stages_StageId",
                table: "AcademicYears");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Terms_TermId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_schoolPhones_Schools_SchoolId",
                table: "schoolPhones");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Grades_GradeId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Schools_SchoolId",
                table: "Stages");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Sections_SectionId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Schools_SchoolId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "AssignedSubjects");

            migrationBuilder.DropTable(
                name: "SubjectGrades");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SchoolId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Stages_SchoolId",
                table: "Stages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_schoolPhones",
                table: "schoolPhones");

            migrationBuilder.DropIndex(
                name: "IX_Grades_TermId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Grades");

            migrationBuilder.RenameTable(
                name: "schoolPhones",
                newName: "SchoolPhones");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Subjects",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "GradeId",
                table: "Sections",
                newName: "SchoolGradeId");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_GradeId",
                table: "Sections",
                newName: "IX_Sections_SchoolGradeId");

            migrationBuilder.RenameIndex(
                name: "IX_schoolPhones_SchoolId",
                table: "SchoolPhones",
                newName: "IX_SchoolPhones_SchoolId");

            migrationBuilder.RenameColumn(
                name: "StageId",
                table: "AcademicYears",
                newName: "SchoolId");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AcademicYears",
                newName: "IsCurrentYear");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicYears_StageId",
                table: "AcademicYears",
                newName: "IX_AcademicYears_SchoolId");

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicYearId",
                table: "Sections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolPhones",
                table: "SchoolPhones",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StageGrade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageGrade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageGrade_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StageGrade_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolGrade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StageGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolGrade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolGrade_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolGrade_StageGrade_StageGradeId",
                        column: x => x.StageGradeId,
                        principalTable: "StageGrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GradeSubject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeSubject_SchoolGrade_SchoolGradeId",
                        column: x => x.SchoolGradeId,
                        principalTable: "SchoolGrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GradeSubject_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SectionSubject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeSubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionSubject_GradeSubject_GradeSubjectId",
                        column: x => x.GradeSubjectId,
                        principalTable: "GradeSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionSubject_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionSubject_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SectionSubject_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_AcademicYearId",
                table: "Sections",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSubject_SchoolGradeId",
                table: "GradeSubject",
                column: "SchoolGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSubject_SubjectId",
                table: "GradeSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolGrade_SchoolId",
                table: "SchoolGrade",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolGrade_StageGradeId",
                table: "SchoolGrade",
                column: "StageGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSubject_GradeSubjectId",
                table: "SectionSubject",
                column: "GradeSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSubject_SectionId",
                table: "SectionSubject",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSubject_TeacherId",
                table: "SectionSubject",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSubject_TermId",
                table: "SectionSubject",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_StageGrade_GradeId",
                table: "StageGrade",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_StageGrade_StageId",
                table: "StageGrade",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYears_Schools_SchoolId",
                table: "AcademicYears",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolPhones_Schools_SchoolId",
                table: "SchoolPhones",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_AcademicYears_AcademicYearId",
                table: "Sections",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
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
                name: "FK_Students_Sections_SectionId",
                table: "Students",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYears_Schools_SchoolId",
                table: "AcademicYears");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolPhones_Schools_SchoolId",
                table: "SchoolPhones");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_AcademicYears_AcademicYearId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_SchoolGrade_SchoolGradeId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Sections_SectionId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "SectionSubject");

            migrationBuilder.DropTable(
                name: "GradeSubject");

            migrationBuilder.DropTable(
                name: "SchoolGrade");

            migrationBuilder.DropTable(
                name: "StageGrade");

            migrationBuilder.DropIndex(
                name: "IX_Sections_AcademicYearId",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolPhones",
                table: "SchoolPhones");

            migrationBuilder.DropColumn(
                name: "AcademicYearId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Sections");

            migrationBuilder.RenameTable(
                name: "SchoolPhones",
                newName: "schoolPhones");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Subjects",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "SchoolGradeId",
                table: "Sections",
                newName: "GradeId");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_SchoolGradeId",
                table: "Sections",
                newName: "IX_Sections_GradeId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolPhones_SchoolId",
                table: "schoolPhones",
                newName: "IX_schoolPhones_SchoolId");

            migrationBuilder.RenameColumn(
                name: "SchoolId",
                table: "AcademicYears",
                newName: "StageId");

            migrationBuilder.RenameColumn(
                name: "IsCurrentYear",
                table: "AcademicYears",
                newName: "IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicYears_SchoolId",
                table: "AcademicYears",
                newName: "IX_AcademicYears_StageId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Terms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Terms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Terms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Subjects",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Subjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "Subjects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Subjects",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "Stages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Sections",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Sections",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "Sections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Sections",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Grades",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Grades",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TermId",
                table: "Grades",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Grades",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_schoolPhones",
                table: "schoolPhones",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssignedSubjects",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedSubjects", x => new { x.SubjectId, x.SectionId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_AssignedSubjects_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedSubjects_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGrades",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxMark = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    MinPassMark = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGrades", x => new { x.SubjectId, x.GradeId });
                    table.ForeignKey(
                        name: "FK_SubjectGrades_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectGrades_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SchoolId",
                table: "Subjects",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_SchoolId",
                table: "Stages",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_TermId",
                table: "Grades",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSubjects_SectionId",
                table: "AssignedSubjects",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSubjects_TeacherId",
                table: "AssignedSubjects",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGrades_GradeId",
                table: "SubjectGrades",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYears_Stages_StageId",
                table: "AcademicYears",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Terms_TermId",
                table: "Grades",
                column: "TermId",
                principalTable: "Terms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_schoolPhones_Schools_SchoolId",
                table: "schoolPhones",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Grades_GradeId",
                table: "Sections",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Schools_SchoolId",
                table: "Stages",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Sections_SectionId",
                table: "Students",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Schools_SchoolId",
                table: "Subjects",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
