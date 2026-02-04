using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemenSchoolsV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addschoolIdForAppuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "AspNetUsers");
        }
    }
}
