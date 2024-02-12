using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Migrations
{
    /// <inheritdoc />
    public partial class RuleBreaking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureFeatureChoice");

            migrationBuilder.RenameColumn(
                name: "Wisdom",
                table: "Characters",
                newName: "MaxWisdom");

            migrationBuilder.RenameColumn(
                name: "Strength",
                table: "Characters",
                newName: "MaxStrength");

            migrationBuilder.RenameColumn(
                name: "Intelligence",
                table: "Characters",
                newName: "MaxIntelligence");

            migrationBuilder.RenameColumn(
                name: "Dexterity",
                table: "Characters",
                newName: "MaxDexterity");

            migrationBuilder.RenameColumn(
                name: "Constitution",
                table: "Characters",
                newName: "MaxConstitution");

            migrationBuilder.RenameColumn(
                name: "Charisma",
                table: "Characters",
                newName: "MaxCharisma");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Features",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseCharisma",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseConstitution",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseDexterity",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseIntelligence",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseStrength",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseWisdom",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusCharisma",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusConstitution",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusDexterity",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusIntelligence",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusStrength",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusWisdom",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IgnoreLimits",
                table: "Characters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FeatureChoiceSelectableFeature",
                columns: table => new
                {
                    FeatureChoiceChoiceId = table.Column<int>(type: "int", nullable: false),
                    FeaturesFeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureChoiceSelectableFeature", x => new { x.FeatureChoiceChoiceId, x.FeaturesFeatureId });
                    table.ForeignKey(
                        name: "FK_FeatureChoiceSelectableFeature_Choices_FeatureChoiceChoiceId",
                        column: x => x.FeatureChoiceChoiceId,
                        principalTable: "Choices",
                        principalColumn: "ChoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureChoiceSelectableFeature_Features_FeaturesFeatureId",
                        column: x => x.FeaturesFeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureChoiceSelectableFeature_FeaturesFeatureId",
                table: "FeatureChoiceSelectableFeature",
                column: "FeaturesFeatureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureChoiceSelectableFeature");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "BaseCharisma",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BaseConstitution",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BaseDexterity",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BaseIntelligence",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BaseStrength",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BaseWisdom",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BonusCharisma",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BonusConstitution",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BonusDexterity",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BonusIntelligence",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BonusStrength",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BonusWisdom",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "IgnoreLimits",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "MaxWisdom",
                table: "Characters",
                newName: "Wisdom");

            migrationBuilder.RenameColumn(
                name: "MaxStrength",
                table: "Characters",
                newName: "Strength");

            migrationBuilder.RenameColumn(
                name: "MaxIntelligence",
                table: "Characters",
                newName: "Intelligence");

            migrationBuilder.RenameColumn(
                name: "MaxDexterity",
                table: "Characters",
                newName: "Dexterity");

            migrationBuilder.RenameColumn(
                name: "MaxConstitution",
                table: "Characters",
                newName: "Constitution");

            migrationBuilder.RenameColumn(
                name: "MaxCharisma",
                table: "Characters",
                newName: "Charisma");

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
                name: "IX_FeatureFeatureChoice_FeaturesFeatureId",
                table: "FeatureFeatureChoice",
                column: "FeaturesFeatureId");
        }
    }
}
