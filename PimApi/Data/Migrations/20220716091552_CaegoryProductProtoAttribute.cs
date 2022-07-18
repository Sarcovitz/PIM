using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIM.Api.Data.Migrations
{
    public partial class CaegoryProductProtoAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProductAttributeProto",
                schema: "PIM");

            migrationBuilder.CreateTable(
                name: "CategoryProductAttributeProtos",
                schema: "PIM",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeProtoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProductAttributeProtos", x => new { x.CategoryId, x.ProductAttributeProtoId });
                    table.ForeignKey(
                        name: "FK_CategoryProductAttributeProtos_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "PIM",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProductAttributeProtos_ProductAttributeProtos_Categ~",
                        column: x => x.CategoryId,
                        principalSchema: "PIM",
                        principalTable: "ProductAttributeProtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProductAttributeProtos",
                schema: "PIM");

            migrationBuilder.CreateTable(
                name: "CategoryProductAttributeProto",
                schema: "PIM",
                columns: table => new
                {
                    AttributeProtosId = table.Column<int>(type: "integer", nullable: false),
                    CategoriesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProductAttributeProto", x => new { x.AttributeProtosId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_CategoryProductAttributeProto_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalSchema: "PIM",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProductAttributeProto_ProductAttributeProtos_Attrib~",
                        column: x => x.AttributeProtosId,
                        principalSchema: "PIM",
                        principalTable: "ProductAttributeProtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProductAttributeProto_CategoriesId",
                schema: "PIM",
                table: "CategoryProductAttributeProto",
                column: "CategoriesId");
        }
    }
}
