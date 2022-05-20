using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PIM.Api.Data.Migrations
{
    public partial class attributeProto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductAttributeProtos",
                schema: "PIM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    AttributeType = table.Column<int>(type: "integer", nullable: false),
                    DefaultValue = table.Column<string>(type: "text", nullable: true),
                    PossibleValues = table.Column<string>(type: "text", nullable: true),
                    CatalogId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeProtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeProtos_Catalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalSchema: "PIM",
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                schema: "PIM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    AttributeProtoId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_ProductAttributeProtos_AttributeProtoId",
                        column: x => x.AttributeProtoId,
                        principalSchema: "PIM",
                        principalTable: "ProductAttributeProtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "PIM",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProductAttributeProto_CategoriesId",
                schema: "PIM",
                table: "CategoryProductAttributeProto",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeProtos_CatalogId",
                schema: "PIM",
                table: "ProductAttributeProtos",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeProtoId",
                schema: "PIM",
                table: "ProductAttributes",
                column: "AttributeProtoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductId",
                schema: "PIM",
                table: "ProductAttributes",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProductAttributeProto",
                schema: "PIM");

            migrationBuilder.DropTable(
                name: "ProductAttributes",
                schema: "PIM");

            migrationBuilder.DropTable(
                name: "ProductAttributeProtos",
                schema: "PIM");
        }
    }
}
