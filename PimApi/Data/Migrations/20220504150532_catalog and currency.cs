using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PIM.Api.Data.Migrations
{
    public partial class catalogandcurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "PIM",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    Fullname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Catalogs",
                schema: "PIM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DefualtCurrencyCode = table.Column<string>(type: "text", nullable: false),
                    DefaultCurrencyCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalogs_Currencies_DefaultCurrencyCode",
                        column: x => x.DefaultCurrencyCode,
                        principalSchema: "PIM",
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogUsers",
                schema: "PIM",
                columns: table => new
                {
                    CatalogId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogUsers", x => new { x.CatalogId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CatalogUsers_Catalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalSchema: "PIM",
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogUsers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "PIM",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_DefaultCurrencyCode",
                schema: "PIM",
                table: "Catalogs",
                column: "DefaultCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogUsers_UserId",
                schema: "PIM",
                table: "CatalogUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                schema: "PIM",
                table: "Currencies",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogUsers",
                schema: "PIM");

            migrationBuilder.DropTable(
                name: "Catalogs",
                schema: "PIM");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "PIM");
        }
    }
}
