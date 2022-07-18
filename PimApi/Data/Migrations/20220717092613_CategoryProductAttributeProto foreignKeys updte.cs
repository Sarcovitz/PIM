using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIM.Api.Data.Migrations
{
    public partial class CategoryProductAttributeProtoforeignKeysupdte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProductAttributeProtos_ProductAttributeProtos_Categ~",
                schema: "PIM",
                table: "CategoryProductAttributeProtos");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProductAttributeProtos_ProductAttributeProtoId",
                schema: "PIM",
                table: "CategoryProductAttributeProtos",
                column: "ProductAttributeProtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProductAttributeProtos_ProductAttributeProtos_Produ~",
                schema: "PIM",
                table: "CategoryProductAttributeProtos",
                column: "ProductAttributeProtoId",
                principalSchema: "PIM",
                principalTable: "ProductAttributeProtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProductAttributeProtos_ProductAttributeProtos_Produ~",
                schema: "PIM",
                table: "CategoryProductAttributeProtos");

            migrationBuilder.DropIndex(
                name: "IX_CategoryProductAttributeProtos_ProductAttributeProtoId",
                schema: "PIM",
                table: "CategoryProductAttributeProtos");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProductAttributeProtos_ProductAttributeProtos_Categ~",
                schema: "PIM",
                table: "CategoryProductAttributeProtos",
                column: "CategoryId",
                principalSchema: "PIM",
                principalTable: "ProductAttributeProtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
