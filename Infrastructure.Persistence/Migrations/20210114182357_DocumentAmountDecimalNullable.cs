using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class DocumentAmountDecimalNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Documents",
                type: "decimal(12,3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Documents",
                type: "decimal(12,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldNullable: true);
        }
    }
}
