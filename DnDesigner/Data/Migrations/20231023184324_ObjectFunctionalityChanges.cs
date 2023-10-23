using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Data.Migrations
{
    /// <inheritdoc />
    public partial class ObjectFunctionalityChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HitDieType",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ProficiencyBonus",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "ProficiencyType",
                table: "Proficiencies",
                newName: "Type");

            migrationBuilder.AddColumn<bool>(
                name: "Attuned",
                table: "InventoryItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Equipped",
                table: "InventoryItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EquippedIn",
                table: "InventoryItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attuned",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "Equipped",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "EquippedIn",
                table: "InventoryItems");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Proficiencies",
                newName: "ProficiencyType");

            migrationBuilder.AddColumn<string>(
                name: "HitDieType",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProficiencyBonus",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
