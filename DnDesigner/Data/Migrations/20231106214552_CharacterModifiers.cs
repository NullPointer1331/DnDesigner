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
                table: "ClassFeatures",
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
                name: "CharacterModifiers",
                table: "ClassFeatures");

            migrationBuilder.DropColumn(
                name: "CharacterModifiers",
                table: "BackgroundFeatures");
        }
    }
}
