using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Migrations
{
    /// <inheritdoc />
    public partial class FeatureChoices : Migration
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
                name: "AutoLoad",
                table: "Choices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApplied",
                table: "CharacterChoice",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PreviousChoiceValue",
                table: "CharacterChoice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FeatureFeatureChoice",
                columns: table => new
                {
                    FeatureChoiceChoiceId = table.Column<int>(type: "int", nullable: false),
                    FeaturesFeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureFeatureChoice", x => new { x.FeatureChoiceChoiceId, x.FeaturesFeatureId });
                    table.ForeignKey(
                        name: "FK_FeatureFeatureChoice_Choices_FeatureChoiceChoiceId",
                        column: x => x.FeatureChoiceChoiceId,
                        principalTable: "Choices",
                        principalColumn: "ChoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureFeatureChoice_Features_FeaturesFeatureId",
                        column: x => x.FeaturesFeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Effects_GroupedEffectEffectId",
                table: "Effects",
                column: "GroupedEffectEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureFeatureChoice_FeaturesFeatureId",
                table: "FeatureFeatureChoice",
                column: "FeaturesFeatureId");

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

            migrationBuilder.DropTable(
                name: "FeatureFeatureChoice");

            migrationBuilder.DropIndex(
                name: "IX_Effects_GroupedEffectEffectId",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "GroupedEffectEffectId",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "AutoLoad",
                table: "Choices");

            migrationBuilder.DropColumn(
                name: "IsApplied",
                table: "CharacterChoice");

            migrationBuilder.DropColumn(
                name: "PreviousChoiceValue",
                table: "CharacterChoice");
        }
    }
}
