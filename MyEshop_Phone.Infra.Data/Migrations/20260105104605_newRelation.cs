using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEshopPhone.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class newRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubmenuGroupsId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubmenuGroupsId",
                table: "Products",
                column: "SubmenuGroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_submenuGroups_SubmenuGroupsId",
                table: "Products",
                column: "SubmenuGroupsId",
                principalTable: "submenuGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_submenuGroups_SubmenuGroupsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubmenuGroupsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubmenuGroupsId",
                table: "Products");
        }
    }
}
