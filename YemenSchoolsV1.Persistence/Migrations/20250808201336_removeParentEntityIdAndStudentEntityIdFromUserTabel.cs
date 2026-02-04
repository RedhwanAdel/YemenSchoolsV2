using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemenSchoolsV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removeParentEntityIdAndStudentEntityIdFromUserTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Parents_ParentEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Students_StudentEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ParentEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ParentEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentEntityId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParentEntityId",
                table: "AspNetUsers",
                column: "ParentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentEntityId",
                table: "AspNetUsers",
                column: "StudentEntityId");

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
        }
    }
}
