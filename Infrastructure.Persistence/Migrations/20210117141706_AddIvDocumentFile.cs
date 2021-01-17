using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddIvDocumentFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "IV",
                table: "DocumentFiles",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IV",
                table: "DocumentFiles");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Documents",
                type: "datetime",
                nullable: true);
        }
    }
}
