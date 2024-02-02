using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Migrations
{
    /// <inheritdoc />
    public partial class ArmorClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArmorClassFormula",
                table: "Effects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifyAttribute_Value",
                table: "Effects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoArmor",
                table: "Effects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoShield",
                table: "Effects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Override",
                table: "Effects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseArmorClass",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusArmorClass",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArmorClassFormula",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "ModifyAttribute_Value",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "NoArmor",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "NoShield",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "Override",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "BaseArmorClass",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BonusArmorClass",
                table: "Characters");
        }
    }
}
