using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Data.Migrations
{
    /// <inheritdoc />
    public partial class ItemChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
