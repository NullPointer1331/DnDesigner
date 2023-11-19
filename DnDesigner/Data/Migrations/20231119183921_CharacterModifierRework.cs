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
                name: "Effects",
                columns: table => new
                {
                    EffectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BackgroundFeatureFeatureId = table.Column<int>(type: "int", nullable: true),
                    ClassFeatureFeatureId = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectChoiceEffectId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    RaceFeatureFeatureId = table.Column<int>(type: "int", nullable: true),
                    SubclassFeatureFeatureId = table.Column<int>(type: "int", nullable: true),
                    ChosenIndex = table.Column<int>(type: "int", nullable: true),
                    ActionId = table.Column<int>(type: "int", nullable: true),
                    Expertise = table.Column<bool>(type: "bit", nullable: true),
                    Attribute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Effects", x => x.EffectId);
                    table.ForeignKey(
                        name: "FK_Effects_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "ActionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Effects_BackgroundFeatures_BackgroundFeatureFeatureId",
                        column: x => x.BackgroundFeatureFeatureId,
                        principalTable: "BackgroundFeatures",
                        principalColumn: "FeatureId");
                    table.ForeignKey(
                        name: "FK_Effects_ClassFeatures_ClassFeatureFeatureId",
                        column: x => x.ClassFeatureFeatureId,
                        principalTable: "ClassFeatures",
                        principalColumn: "FeatureId");
                    table.ForeignKey(
                        name: "FK_Effects_Effects_EffectChoiceEffectId",
                        column: x => x.EffectChoiceEffectId,
                        principalTable: "Effects",
                        principalColumn: "EffectId");
                    table.ForeignKey(
                        name: "FK_Effects_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId");
                    table.ForeignKey(
                        name: "FK_Effects_RaceFeatures_RaceFeatureFeatureId",
                        column: x => x.RaceFeatureFeatureId,
                        principalTable: "RaceFeatures",
                        principalColumn: "FeatureId");
                    table.ForeignKey(
                        name: "FK_Effects_SubclassFeatures_SubclassFeatureFeatureId",
                        column: x => x.SubclassFeatureFeatureId,
                        principalTable: "SubclassFeatures",
                        principalColumn: "FeatureId");
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
                name: "CharacterEffects",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    EffectId = table.Column<int>(type: "int", nullable: false),
                    IsApplied = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEffects", x => new { x.CharacterId, x.EffectId });
                    table.ForeignKey(
                        name: "FK_CharacterEffects_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterEffects_Effects_EffectId",
                        column: x => x.EffectId,
                        principalTable: "Effects",
                        principalColumn: "EffectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterFeatureEffect",
                columns: table => new
                {
                    CharacterFeatureFeatureId = table.Column<int>(type: "int", nullable: false),
                    EffectsEffectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterFeatureEffect", x => new { x.CharacterFeatureFeatureId, x.EffectsEffectId });
                    table.ForeignKey(
                        name: "FK_CharacterFeatureEffect_CharacterFeatures_CharacterFeatureFeatureId",
                        column: x => x.CharacterFeatureFeatureId,
                        principalTable: "CharacterFeatures",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterFeatureEffect_Effects_EffectsEffectId",
                        column: x => x.EffectsEffectId,
                        principalTable: "Effects",
                        principalColumn: "EffectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrantProficienciesProficiency",
                columns: table => new
                {
                    GrantProficienciesEffectId = table.Column<int>(type: "int", nullable: false),
                    ProficienciesProficiencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrantProficienciesProficiency", x => new { x.GrantProficienciesEffectId, x.ProficienciesProficiencyId });
                    table.ForeignKey(
                        name: "FK_GrantProficienciesProficiency_Effects_GrantProficienciesEffectId",
                        column: x => x.GrantProficienciesEffectId,
                        principalTable: "Effects",
                        principalColumn: "EffectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrantProficienciesProficiency_Proficiencies_ProficienciesProficiencyId",
                        column: x => x.ProficienciesProficiencyId,
                        principalTable: "Proficiencies",
                        principalColumn: "ProficiencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEffects_EffectId",
                table: "CharacterEffects",
                column: "EffectId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFeatureEffect_EffectsEffectId",
                table: "CharacterFeatureEffect",
                column: "EffectsEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSpellcastingSpell_CharacterSpellcastingCharacterId_CharacterSpellcastingSpellcastingId",
                table: "CharacterSpellcastingSpell",
                columns: new[] { "CharacterSpellcastingCharacterId", "CharacterSpellcastingSpellcastingId" });

            migrationBuilder.CreateIndex(
                name: "IX_Effects_ActionId",
                table: "Effects",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Effects_BackgroundFeatureFeatureId",
                table: "Effects",
                column: "BackgroundFeatureFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Effects_ClassFeatureFeatureId",
                table: "Effects",
                column: "ClassFeatureFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Effects_EffectChoiceEffectId",
                table: "Effects",
                column: "EffectChoiceEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_Effects_ItemId",
                table: "Effects",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Effects_RaceFeatureFeatureId",
                table: "Effects",
                column: "RaceFeatureFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Effects_SubclassFeatureFeatureId",
                table: "Effects",
                column: "SubclassFeatureFeatureId");

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
                name: "CharacterEffects");

            migrationBuilder.DropTable(
                name: "CharacterFeatureEffect");

            migrationBuilder.DropTable(
                name: "CharacterSpellcastingSpell");

            migrationBuilder.DropTable(
                name: "GrantProficienciesProficiency");

            migrationBuilder.DropTable(
                name: "SpellSpellcasting");

            migrationBuilder.DropTable(
                name: "Effects");

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
