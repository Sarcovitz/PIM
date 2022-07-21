using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIM.Api.Data.Migrations
{
    public partial class categryhasmanytest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                schema: "PIM",
                table: "Categories");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                schema: "PIM",
                table: "Categories",
                column: "ParentCategoryId",
                principalSchema: "PIM",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                schema: "PIM",
                table: "Categories");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                schema: "PIM",
                table: "Categories",
                column: "ParentCategoryId",
                principalSchema: "PIM",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
