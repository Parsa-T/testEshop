using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEshopPhone.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixModelUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
