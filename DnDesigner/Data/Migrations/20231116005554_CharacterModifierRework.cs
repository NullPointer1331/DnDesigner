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
            migrationBuilder.DropTable(
                name: "KnownSpells");

            migrationBuilder.DropTable(
                name: "LearnableSpells");

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
                name: "CharacterSpellcastingSpell",
                columns: table => new
                {
                    PreparedSpellsSpellId = table.Column<int>(type: "int", nullable: false),
                    CharacterSpellcastingCharacterId = table.Column<int>(type: "int", nullable: false),
                    CharacterSpellcastingSpellcastingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSpellcastingSpell", x => new { x.PreparedSpellsSpellId, x.CharacterSpellcastingCharacterId, x.CharacterSpellcastingSpellcastingId });
                    table.ForeignKey(
                        name: "FK_CharacterSpellcastingSpell_CharacterSpellcasting_CharacterSpellcastingCharacterId_CharacterSpellcastingSpellcastingId",
                        columns: x => new { x.CharacterSpellcastingCharacterId, x.CharacterSpellcastingSpellcastingId },
                        principalTable: "CharacterSpellcasting",
                        principalColumns: new[] { "CharacterId", "SpellcastingId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterSpellcastingSpell_Spells_PreparedSpellsSpellId",
                        column: x => x.PreparedSpellsSpellId,
                        principalTable: "Spells",
                        principalColumn: "SpellId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpellSpellcasting",
                columns: table => new
                {
                    LearnableSpellsSpellId = table.Column<int>(type: "int", nullable: false),
                    LearnedBySpellcastingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellSpellcasting", x => new { x.LearnableSpellsSpellId, x.LearnedBySpellcastingId });
                    table.ForeignKey(
                        name: "FK_SpellSpellcasting_Spellcasting_LearnedBySpellcastingId",
                        column: x => x.LearnedBySpellcastingId,
                        principalTable: "Spellcasting",
                        principalColumn: "SpellcastingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpellSpellcasting_Spells_LearnableSpellsSpellId",
                        column: x => x.LearnableSpellsSpellId,
                        principalTable: "Spells",
                        principalColumn: "SpellId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CharacterSpellcastingSpell_CharacterSpellcastingCharacterId_CharacterSpellcastingSpellcastingId",
                table: "CharacterSpellcastingSpell",
                columns: new[] { "CharacterSpellcastingCharacterId", "CharacterSpellcastingSpellcastingId" });

            migrationBuilder.CreateIndex(
                name: "IX_GrantProficienciesProficiency_ProficienciesProficiencyId",
                table: "GrantProficienciesProficiency",
                column: "ProficienciesProficiencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellSpellcasting_LearnedBySpellcastingId",
                table: "SpellSpellcasting",
                column: "LearnedBySpellcastingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterModifierClassFeature");

            migrationBuilder.DropTable(
                name: "CharacterModifierSubclassFeature");

            migrationBuilder.DropTable(
                name: "CharacterSpellcastingSpell");

            migrationBuilder.DropTable(
                name: "GrantProficienciesProficiency");

            migrationBuilder.DropTable(
                name: "SpellSpellcasting");

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

            migrationBuilder.CreateTable(
                name: "KnownSpells",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    SpellcastingId = table.Column<int>(type: "int", nullable: false),
                    SpellId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnownSpells", x => new { x.CharacterId, x.SpellcastingId, x.SpellId });
                    table.ForeignKey(
                        name: "FK_KnownSpells_CharacterSpellcasting_CharacterId_SpellcastingId",
                        columns: x => new { x.CharacterId, x.SpellcastingId },
                        principalTable: "CharacterSpellcasting",
                        principalColumns: new[] { "CharacterId", "SpellcastingId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KnownSpells_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spells",
                        principalColumn: "SpellId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnableSpells",
                columns: table => new
                {
                    SpellId = table.Column<int>(type: "int", nullable: false),
                    SpellcastingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnableSpells", x => new { x.SpellId, x.SpellcastingId });
                    table.ForeignKey(
                        name: "FK_LearnableSpells_Spellcasting_SpellcastingId",
                        column: x => x.SpellcastingId,
                        principalTable: "Spellcasting",
                        principalColumn: "SpellcastingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearnableSpells_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spells",
                        principalColumn: "SpellId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KnownSpells_SpellId",
                table: "KnownSpells",
                column: "SpellId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableSpells_SpellcastingId",
                table: "LearnableSpells",
                column: "SpellcastingId");
        }
    }
}
