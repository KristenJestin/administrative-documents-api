using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class IdForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UpdatedBy",
                table: "Folder",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Folder",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedBy",
                table: "DocumentTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "DocumentTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedBy",
                table: "DocumentTags",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "DocumentTags",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedBy",
                table: "Documents",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Documents",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedBy",
                table: "DocumentFiles",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "DocumentFiles",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Folder",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Folder",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "DocumentTypes",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "DocumentTypes",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "DocumentTags",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "DocumentTags",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Documents",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Documents",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "DocumentFiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "DocumentFiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
