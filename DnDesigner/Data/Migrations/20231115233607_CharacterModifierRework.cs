using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Data.Migrations
{
    /// <inheritdoc />
    public partial class CharacterModifierRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterModifiers",
                table: "SubclassFeatures");

            migrationBuilder.DropColumn(
                name: "CharacterModifiers",
                table: "RaceFeatures");

            migrationBuilder.DropColumn(
                name: "CharacterModifiers",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CharacterModifiers",
                table: "ClassFeatures");

            migrationBuilder.DropColumn(
                name: "CharacterModifiers",
                table: "CharacterFeatures");

            migrationBuilder.DropColumn(
                name: "CharacterModifiers",
                table: "BackgroundFeatures");

            migrationBuilder.CreateTable(
                name: "CharacterModifiers",
                columns: table => new
                {
                    CharacterModifierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsApplied = table.Column<bool>(type: "bit", nullable: false),
                    BackgroundFeatureFeatureId = table.Column<int>(type: "int", nullable: true),
                    CharacterFeatureFeatureId = table.Column<int>(type: "int", nullable: true),
                    CharacterModifierChoiceCharacterModifierId = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    RaceFeatureFeatureId = table.Column<int>(type: "int", nullable: true),
                    ActionId = table.Column<int>(type: "int", nullable: true),
                    ChosenIndex = table.Column<int>(type: "int", nullable: true),
                    Expertise = table.Column<bool>(type: "bit", nullable: true),
                    Attribute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterModifiers", x => x.CharacterModifierId);
                    table.ForeignKey(
                        name: "FK_CharacterModifiers_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "ActionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterModifiers_BackgroundFeatures_BackgroundFeatureFeatureId",
                        column: x => x.BackgroundFeatureFeatureId,
                        principalTable: "BackgroundFeatures",
                        principalColumn: "FeatureId");
                    table.ForeignKey(
                        name: "FK_CharacterModifiers_CharacterFeatures_CharacterFeatureFeatureId",
                        column: x => x.CharacterFeatureFeatureId,
                        principalTable: "CharacterFeatures",
                        principalColumn: "FeatureId");
                    table.ForeignKey(
                        name: "FK_CharacterModifiers_CharacterModifiers_CharacterModifierChoiceCharacterModifierId",
                        column: x => x.CharacterModifierChoiceCharacterModifierId,
                        principalTable: "CharacterModifiers",
                        principalColumn: "CharacterModifierId");
                    table.ForeignKey(
                        name: "FK_CharacterModifiers_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId");
                    table.ForeignKey(
                        name: "FK_CharacterModifiers_RaceFeatures_RaceFeatureFeatureId",
                        column: x => x.RaceFeatureFeatureId,
                        principalTable: "RaceFeatures",
                        principalColumn: "FeatureId");
                });

            migrationBuilder.CreateTable(
                name: "CharacterModifierClassFeature",
                columns: table => new
                {
                    CharacterModifiersCharacterModifierId = table.Column<int>(type: "int", nullable: false),
                    ClassFeatureFeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterModifierClassFeature", x => new { x.CharacterModifiersCharacterModifierId, x.ClassFeatureFeatureId });
                    table.ForeignKey(
                        name: "FK_CharacterModifierClassFeature_CharacterModifiers_CharacterModifiersCharacterModifierId",
                        column: x => x.CharacterModifiersCharacterModifierId,
                        principalTable: "CharacterModifiers",
                        principalColumn: "CharacterModifierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterModifierClassFeature_ClassFeatures_ClassFeatureFeatureId",
                        column: x => x.ClassFeatureFeatureId,
                        principalTable: "ClassFeatures",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterModifierSubclassFeature",
                columns: table => new
                {
                    CharacterModifiersCharacterModifierId = table.Column<int>(type: "int", nullable: false),
                    SubclassFeatureFeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterModifierSubclassFeature", x => new { x.CharacterModifiersCharacterModifierId, x.SubclassFeatureFeatureId });
                    table.ForeignKey(
                        name: "FK_CharacterModifierSubclassFeature_CharacterModifiers_CharacterModifiersCharacterModifierId",
                        column: x => x.CharacterModifiersCharacterModifierId,
                        principalTable: "CharacterModifiers",
                        principalColumn: "CharacterModifierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterModifierSubclassFeature_SubclassFeatures_SubclassFeatureFeatureId",
                        column: x => x.SubclassFeatureFeatureId,
                        principalTable: "SubclassFeatures",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrantProficienciesProficiency",
                columns: table => new
                {
                    GrantProficienciesCharacterModifierId = table.Column<int>(type: "int", nullable: false),
                    ProficienciesProficiencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrantProficienciesProficiency", x => new { x.GrantProficienciesCharacterModifierId, x.ProficienciesProficiencyId });
                    table.ForeignKey(
                        name: "FK_GrantProficienciesProficiency_CharacterModifiers_GrantProficienciesCharacterModifierId",
                        column: x => x.GrantProficienciesCharacterModifierId,
                        principalTable: "CharacterModifiers",
                        principalColumn: "CharacterModifierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrantProficienciesProficiency_Proficiencies_ProficienciesProficiencyId",
                        column: x => x.ProficienciesProficiencyId,
                        principalTable: "Proficiencies",
                        principalColumn: "ProficiencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterModifierClassFeature_ClassFeatureFeatureId",
                table: "CharacterModifierClassFeature",
                column: "ClassFeatureFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterModifiers_ActionId",
                table: "CharacterModifiers",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterModifiers_BackgroundFeatureFeatureId",
                table: "CharacterModifiers",
                column: "BackgroundFeatureFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterModifiers_CharacterFeatureFeatureId",
                table: "CharacterModifiers",
                column: "CharacterFeatureFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterModifiers_CharacterModifierChoiceCharacterModifierId",
                table: "CharacterModifiers",
                column: "CharacterModifierChoiceCharacterModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterModifiers_ItemId",
                table: "CharacterModifiers",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterModifiers_RaceFeatureFeatureId",
                table: "CharacterModifiers",
                column: "RaceFeatureFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterModifierSubclassFeature_SubclassFeatureFeatureId",
                table: "CharacterModifierSubclassFeature",
                column: "SubclassFeatureFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_GrantProficienciesProficiency_ProficienciesProficiencyId",
                table: "GrantProficienciesProficiency",
                column: "ProficienciesProficiencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterModifierClassFeature");

            migrationBuilder.DropTable(
                name: "CharacterModifierSubclassFeature");

            migrationBuilder.DropTable(
                name: "GrantProficienciesProficiency");

            migrationBuilder.DropTable(
                name: "CharacterModifiers");

            migrationBuilder.AddColumn<string>(
                name: "CharacterModifiers",
                table: "SubclassFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CharacterModifiers",
                table: "RaceFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CharacterModifiers",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CharacterModifiers",
                table: "ClassFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CharacterModifiers",
                table: "CharacterFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CharacterModifiers",
                table: "BackgroundFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
