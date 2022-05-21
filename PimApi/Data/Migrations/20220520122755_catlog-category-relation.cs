using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIM.Api.Data.Migrations
{
    public partial class catlogcategoryrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatalogId",
                schema: "PIM",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CatalogId",
                schema: "PIM",
                table: "Categories",
                column: "CatalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Catalogs_CatalogId",
                schema: "PIM",
                table: "Categories",
                column: "CatalogId",
                principalSchema: "PIM",
                principalTable: "Catalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Catalogs_CatalogId",
                schema: "PIM",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CatalogId",
                schema: "PIM",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CatalogId",
                schema: "PIM",
                table: "Categories");
        }
    }
}
