using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Migrations
{
    /// <inheritdoc />
    public partial class Resources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Max",
                table: "Effects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "Effects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxFormula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestoredPerLongRest = table.Column<int>(type: "int", nullable: false),
                    RestoredPerShortRest = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: true),
                    PactMagic = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ResourceId);
                });

            migrationBuilder.CreateTable(
                name: "CharacterResources",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    MaxAmount = table.Column<int>(type: "int", nullable: false),
                    CurrentAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterResources", x => new { x.CharacterId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_CharacterResources_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "ResourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Effects_ResourceId",
                table: "Effects",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterResources_ResourceId",
                table: "CharacterResources",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Effects_Resources_ResourceId",
                table: "Effects",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "ResourceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Effects_Resources_ResourceId",
                table: "Effects");

            migrationBuilder.DropTable(
                name: "CharacterResources");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Effects_ResourceId",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "Max",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "Effects");
        }
    }
}
