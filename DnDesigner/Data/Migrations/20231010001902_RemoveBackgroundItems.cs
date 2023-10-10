using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBackgroundItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Backgrounds_BackgroundId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_BackgroundId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BackgroundId",
                table: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BackgroundId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_BackgroundId",
                table: "Items",
                column: "BackgroundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Backgrounds_BackgroundId",
                table: "Items",
                column: "BackgroundId",
                principalTable: "Backgrounds",
                principalColumn: "BackgroundId");
        }
    }
}
