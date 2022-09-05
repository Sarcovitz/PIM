using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIM.Api.Data.Migrations
{
    public partial class productcreatemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desctiption",
                schema: "PIM",
                table: "Products",
                newName: "DescriptionHTML");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "PIM",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                schema: "PIM",
                table: "ProductImages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "PIM",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ContentType",
                schema: "PIM",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "DescriptionHTML",
                schema: "PIM",
                table: "Products",
                newName: "Desctiption");
        }
    }
}
