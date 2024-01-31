using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Migrations
{
    /// <inheritdoc />
    public partial class AvailableHitDicechangedtoseparatefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvailableHitDice",
                table: "Characters",
                newName: "d8HitDiceAvailable");

            migrationBuilder.AddColumn<int>(
                name: "d10HitDiceAvailable",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "d12HitDiceAvailable",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "d6HitDiceAvailable",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "d10HitDiceAvailable",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "d12HitDiceAvailable",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "d6HitDiceAvailable",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "d8HitDiceAvailable",
                table: "Characters",
                newName: "AvailableHitDice");
        }
    }
}
