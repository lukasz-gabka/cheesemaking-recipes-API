using Microsoft.EntityFrameworkCore.Migrations;

namespace Cheesemaking_recipes_API.Migrations
{
    public partial class OrderColumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Labels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Categories");
        }
    }
}
