using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Data.Migrations
{
    /// <inheritdoc />
    public partial class LevelUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChosenIndex",
                table: "Effects");

            migrationBuilder.AddColumn<int>(
                name: "SubclassLevel",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "CharacterEffects",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubclassLevel",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "CharacterEffects");

            migrationBuilder.AddColumn<int>(
                name: "ChosenIndex",
                table: "Effects",
                type: "int",
                nullable: true);
        }
    }
}
