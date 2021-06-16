using Microsoft.EntityFrameworkCore.Migrations;

namespace Cheesemaking_recipes_API.Migrations
{
    public partial class userrefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Templates_UserId",
                table: "Templates",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Users_UserId",
                table: "Templates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Users_UserId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Templates_UserId",
                table: "Templates");
        }
    }
}
