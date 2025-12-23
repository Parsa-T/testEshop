using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEshopPhone.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "submenuGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductsGroupsId = table.Column<int>(name: "Products_GroupsId", type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submenuGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_submenuGroups_Products_Groups_Products_GroupsId",
                        column: x => x.ProductsGroupsId,
                        principalTable: "Products_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_submenuGroups_Products_GroupsId",
                table: "submenuGroups",
                column: "Products_GroupsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "submenuGroups");
        }
    }
}
