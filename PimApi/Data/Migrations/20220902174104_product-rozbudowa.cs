using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIM.Api.Data.Migrations
{
    public partial class productrozbudowa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "PIM",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                schema: "PIM",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Desctiption",
                schema: "PIM",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                schema: "PIM",
                table: "Products",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Length",
                schema: "PIM",
                table: "Products",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                schema: "PIM",
                table: "Products",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "PIM",
                table: "Products",
                column: "CategoryId",
                principalSchema: "PIM",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "PIM",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Desctiption",
                schema: "PIM",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "PIM",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Length",
                schema: "PIM",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Width",
                schema: "PIM",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                schema: "PIM",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "PIM",
                table: "Products",
                column: "CategoryId",
                principalSchema: "PIM",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
