using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Data.Migrations
{
    /// <inheritdoc />
    public partial class GetOnlyProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ProficiencyBonus",
                table: "Characters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
