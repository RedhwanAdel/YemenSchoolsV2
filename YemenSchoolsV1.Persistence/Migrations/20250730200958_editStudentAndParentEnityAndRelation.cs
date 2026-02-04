using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemenSchoolsV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editStudentAndParentEnityAndRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Parents_ParentId",
                table: "ParentStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Students_StudentId",
                table: "ParentStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Sections_SectionId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UserId",
                table: "Parents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParentStudent",
                table: "ParentStudent");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Parents");

            migrationBuilder.RenameTable(
                name: "ParentStudent",
                newName: "ParentStudents");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Students",
                newName: "CurrentSectionId");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Students",
                newName: "DateOfBirth");

            migrationBuilder.RenameIndex(
                name: "IX_Students_SectionId",
                table: "Students",
                newName: "IX_Students_CurrentSectionId");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Parents",
                newName: "DateOfBirth");

            migrationBuilder.RenameIndex(
                name: "IX_ParentStudent_StudentId",
                table: "ParentStudents",
                newName: "IX_ParentStudents_StudentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImage",
                table: "Students",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentAcademicYearId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Parents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "Parents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                table: "Parents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "Parents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Parents",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Parents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Parents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntityId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ParentEntityId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentEntityId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ParentStudents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryContact",
                table: "ParentStudents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParentStudents",
                table: "ParentStudents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CurrentAcademicYearId",
                table: "Students",
                column: "CurrentAcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_RegisterNo",
                table: "Students",
                column: "RegisterNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolId",
                table: "Students",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_NationalId",
                table: "Parents",
                column: "NationalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParentEntityId",
                table: "AspNetUsers",
                column: "ParentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentEntityId",
                table: "AspNetUsers",
                column: "StudentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentStudents_ParentId_StudentId",
                table: "ParentStudents",
                columns: new[] { "ParentId", "StudentId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Parents_ParentEntityId",
                table: "AspNetUsers",
                column: "ParentEntityId",
                principalTable: "Parents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Students_StudentEntityId",
                table: "AspNetUsers",
                column: "StudentEntityId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudents_Parents_ParentId",
                table: "ParentStudents",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudents_Students_StudentId",
                table: "ParentStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AcademicYears_CurrentAcademicYearId",
                table: "Students",
                column: "CurrentAcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Sections_CurrentSectionId",
                table: "Students",
                column: "CurrentSectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Parents_ParentEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Students_StudentEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudents_Parents_ParentId",
                table: "ParentStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudents_Students_StudentId",
                table: "ParentStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AcademicYears_CurrentAcademicYearId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Sections_CurrentSectionId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CurrentAcademicYearId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_RegisterNo",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SchoolId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Parents_NationalId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UserId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ParentEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParentStudents",
                table: "ParentStudents");

            migrationBuilder.DropIndex(
                name: "IX_ParentStudents_ParentId_StudentId",
                table: "ParentStudents");

            migrationBuilder.DropColumn(
                name: "CurrentAcademicYearId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ParentEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ParentStudents");

            migrationBuilder.DropColumn(
                name: "IsPrimaryContact",
                table: "ParentStudents");

            migrationBuilder.RenameTable(
                name: "ParentStudents",
                newName: "ParentStudent");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Students",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "CurrentSectionId",
                table: "Students",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_CurrentSectionId",
                table: "Students",
                newName: "IX_Students_SectionId");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Parents",
                newName: "BirthDate");

            migrationBuilder.RenameIndex(
                name: "IX_ParentStudents_StudentId",
                table: "ParentStudent",
                newName: "IX_ParentStudent_StudentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImage",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Parents",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParentStudent",
                table: "ParentStudent",
                columns: new[] { "ParentId", "StudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Parents_ParentId",
                table: "ParentStudent",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Students_StudentId",
                table: "ParentStudent",
                column: "StudentId",
                principalTable: "Students",
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
    }
}
