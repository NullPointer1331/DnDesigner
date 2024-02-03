using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Migrations
{
    /// <inheritdoc />
    public partial class ChoicesRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupedEffectEffectId",
                table: "Effects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceChoice",
                table: "CharacterEffects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Effects_GroupedEffectEffectId",
                table: "Effects",
                column: "GroupedEffectEffectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Effects_Effects_GroupedEffectEffectId",
                table: "Effects",
                column: "GroupedEffectEffectId",
                principalTable: "Effects",
                principalColumn: "EffectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Effects_Effects_GroupedEffectEffectId",
                table: "Effects");

            migrationBuilder.DropIndex(
                name: "IX_Effects_GroupedEffectEffectId",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "GroupedEffectEffectId",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "SourceChoice",
                table: "CharacterEffects");
        }
    }
}
