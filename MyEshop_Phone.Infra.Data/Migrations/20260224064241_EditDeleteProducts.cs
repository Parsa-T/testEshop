using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEshopPhone.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditDeleteProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productsColors_Products_ProductId",
                table: "productsColors");

            migrationBuilder.AddForeignKey(
                name: "FK_productsColors_Products_ProductId",
                table: "productsColors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productsColors_Products_ProductId",
                table: "productsColors");

            migrationBuilder.AddForeignKey(
                name: "FK_productsColors_Products_ProductId",
                table: "productsColors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
