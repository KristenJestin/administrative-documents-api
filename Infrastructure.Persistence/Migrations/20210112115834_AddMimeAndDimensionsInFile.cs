using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddMimeAndDimensionsInFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dimensions",
                table: "DocumentFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "DocumentFiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dimensions",
                table: "DocumentFiles");

            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "DocumentFiles");
        }
    }
}
