using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIM.Api.Data.Migrations
{
    public partial class catalogcorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefualtCurrencyCode",
                schema: "PIM",
                table: "Catalogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefualtCurrencyCode",
                schema: "PIM",
                table: "Catalogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
