using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Data.Migrations
{
    /// <inheritdoc />
    public partial class BackgroundAndRace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Races",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StarterGold",
                table: "Backgrounds",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "StarterGold",
                table: "Backgrounds");
        }
    }
}
