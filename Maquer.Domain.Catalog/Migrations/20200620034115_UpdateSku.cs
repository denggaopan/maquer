using Microsoft.EntityFrameworkCore.Migrations;

namespace Maquer.Domain.Catalog.Migrations
{
    public partial class UpdateSku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecCombination",
                table: "Sku");

            migrationBuilder.AddColumn<string>(
                name: "ProductAttributeId",
                table: "SkuItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkuItem_ProductAttributeId",
                table: "SkuItem",
                column: "ProductAttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkuItem_ProductAttribute_ProductAttributeId",
                table: "SkuItem",
                column: "ProductAttributeId",
                principalTable: "ProductAttribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkuItem_ProductAttribute_ProductAttributeId",
                table: "SkuItem");

            migrationBuilder.DropIndex(
                name: "IX_SkuItem_ProductAttributeId",
                table: "SkuItem");

            migrationBuilder.DropColumn(
                name: "ProductAttributeId",
                table: "SkuItem");

            migrationBuilder.AddColumn<string>(
                name: "SpecCombination",
                table: "Sku",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
