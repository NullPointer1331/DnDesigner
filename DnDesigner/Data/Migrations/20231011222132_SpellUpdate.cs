using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpellUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpellDescription",
                table: "Spells",
                newName: "Description");

            migrationBuilder.AddColumn<bool>(
                name: "IsRitual",
                table: "Spells",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresConcentration",
                table: "Spells",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRitual",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "RequiresConcentration",
                table: "Spells");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Spells",
                newName: "SpellDescription");
        }
    }
}
