using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Data.Migrations
{
    /// <inheritdoc />
    public partial class CharacterModifiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackgroundProficiencies");

            migrationBuilder.DropTable(
                name: "ClassProficiencies");

            migrationBuilder.DropTable(
                name: "RaceProficiencies");

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

            migrationBuilder.AddColumn<int>(
                name: "Rarity",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CharacterModifiers",
                table: "ClassFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "InitialClass",
                table: "CharacterClasses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CharacterModifiers",
                table: "BackgroundFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CharacterFeatures",
                columns: table => new
                {
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CharacterModifiers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterFeatures", x => x.FeatureId);
                    table.ForeignKey(
                        name: "FK_CharacterFeatures_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFeatures_CharacterId",
                table: "CharacterFeatures",
                column: "CharacterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterFeatures");

            migrationBuilder.DropColumn(
                name: "CharacterModifiers",
                table: "SubclassFeatures");

            migrationBuilder.DropColumn(
                name: "CharacterModifiers",
                table: "RaceFeatures");

            migrationBuilder.DropColumn(
                name: "Rarity",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CharacterModifiers",
                table: "ClassFeatures");

            migrationBuilder.DropColumn(
                name: "InitialClass",
                table: "CharacterClasses");

            migrationBuilder.DropColumn(
                name: "CharacterModifiers",
                table: "BackgroundFeatures");

            migrationBuilder.CreateTable(
                name: "BackgroundProficiencies",
                columns: table => new
                {
                    BackgroundId = table.Column<int>(type: "int", nullable: false),
                    ProficiencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackgroundProficiencies", x => new { x.BackgroundId, x.ProficiencyId });
                    table.ForeignKey(
                        name: "FK_BackgroundProficiencies_Backgrounds_BackgroundId",
                        column: x => x.BackgroundId,
                        principalTable: "Backgrounds",
                        principalColumn: "BackgroundId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BackgroundProficiencies_Proficiencies_ProficiencyId",
                        column: x => x.ProficiencyId,
                        principalTable: "Proficiencies",
                        principalColumn: "ProficiencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassProficiencies",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    ProficiencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassProficiencies", x => new { x.ClassId, x.ProficiencyId });
                    table.ForeignKey(
                        name: "FK_ClassProficiencies_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassProficiencies_Proficiencies_ProficiencyId",
                        column: x => x.ProficiencyId,
                        principalTable: "Proficiencies",
                        principalColumn: "ProficiencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceProficiencies",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    ProficiencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceProficiencies", x => new { x.RaceId, x.ProficiencyId });
                    table.ForeignKey(
                        name: "FK_RaceProficiencies_Proficiencies_ProficiencyId",
                        column: x => x.ProficiencyId,
                        principalTable: "Proficiencies",
                        principalColumn: "ProficiencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceProficiencies_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackgroundProficiencies_ProficiencyId",
                table: "BackgroundProficiencies",
                column: "ProficiencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassProficiencies_ProficiencyId",
                table: "ClassProficiencies",
                column: "ProficiencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceProficiencies_ProficiencyId",
                table: "RaceProficiencies",
                column: "ProficiencyId");
        }
    }
}
