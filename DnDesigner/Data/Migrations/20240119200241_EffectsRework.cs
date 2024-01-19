using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Migrations
{
    /// <inheritdoc />
    public partial class EffectsRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Effects_Effects_EffectChoiceEffectId",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "CharacterEffects");

            migrationBuilder.RenameColumn(
                name: "EffectChoiceEffectId",
                table: "Effects",
                newName: "EffectChoiceChoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Effects_EffectChoiceEffectId",
                table: "Effects",
                newName: "IX_Effects_EffectChoiceChoiceId");

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    ChoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultChoice = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => x.ChoiceId);
                    table.ForeignKey(
                        name: "FK_Choices_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId");
                });

            migrationBuilder.CreateTable(
                name: "CharacterChoice",
                columns: table => new
                {
                    CharacterChoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterFeatureId = table.Column<int>(type: "int", nullable: false),
                    ChoiceId = table.Column<int>(type: "int", nullable: false),
                    ChoiceValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterChoice", x => x.CharacterChoiceId);
                    table.ForeignKey(
                        name: "FK_CharacterChoice_CharacterFeatures_CharacterFeatureId",
                        column: x => x.CharacterFeatureId,
                        principalTable: "CharacterFeatures",
                        principalColumn: "CharacterFeatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterChoice_Choices_ChoiceId",
                        column: x => x.ChoiceId,
                        principalTable: "Choices",
                        principalColumn: "ChoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterChoice_CharacterFeatureId",
                table: "CharacterChoice",
                column: "CharacterFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterChoice_ChoiceId",
                table: "CharacterChoice",
                column: "ChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Choices_FeatureId",
                table: "Choices",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Effects_Choices_EffectChoiceChoiceId",
                table: "Effects",
                column: "EffectChoiceChoiceId",
                principalTable: "Choices",
                principalColumn: "ChoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Effects_Choices_EffectChoiceChoiceId",
                table: "Effects");

            migrationBuilder.DropTable(
                name: "CharacterChoice");

            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.RenameColumn(
                name: "EffectChoiceChoiceId",
                table: "Effects",
                newName: "EffectChoiceEffectId");

            migrationBuilder.RenameIndex(
                name: "IX_Effects_EffectChoiceChoiceId",
                table: "Effects",
                newName: "IX_Effects_EffectChoiceEffectId");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "CharacterEffects",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Effects_Effects_EffectChoiceEffectId",
                table: "Effects",
                column: "EffectChoiceEffectId",
                principalTable: "Effects",
                principalColumn: "EffectId");
        }
    }
}
