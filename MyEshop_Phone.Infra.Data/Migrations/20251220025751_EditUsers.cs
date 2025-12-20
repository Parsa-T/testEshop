using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEshopPhone.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProductsId",
                table: "Users",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Products_ProductsId",
                table: "Users",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Products_ProductsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProductsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "Users");
        }
    }
}
