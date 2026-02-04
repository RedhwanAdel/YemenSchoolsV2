using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemenSchoolsV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SchoolReviewsEditStringToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schoolReviews_AspNetUsers_UserId1",
                table: "schoolReviews");

            migrationBuilder.DropIndex(
                name: "IX_schoolReviews_UserId1",
                table: "schoolReviews");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "schoolReviews");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "schoolReviews",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_schoolReviews_UserId",
                table: "schoolReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_schoolReviews_AspNetUsers_UserId",
                table: "schoolReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schoolReviews_AspNetUsers_UserId",
                table: "schoolReviews");

            migrationBuilder.DropIndex(
                name: "IX_schoolReviews_UserId",
                table: "schoolReviews");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "schoolReviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "schoolReviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_schoolReviews_UserId1",
                table: "schoolReviews",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_schoolReviews_AspNetUsers_UserId1",
                table: "schoolReviews",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
