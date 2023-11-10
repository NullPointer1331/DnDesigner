using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Data.Migrations
{
    /// <inheritdoc />
    public partial class Actions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KnownSpells",
                table: "KnownSpells");

            migrationBuilder.DropIndex(
                name: "IX_KnownSpells_CharacterId_SpellcastingId",
                table: "KnownSpells");

            migrationBuilder.AlterColumn<int>(
                name: "ProficiencyId",
                table: "CharacterProficiencies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "CharacterProficiencies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KnownSpells",
                table: "KnownSpells",
                columns: new[] { "CharacterId", "SpellcastingId", "SpellId" });

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    ActionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Range = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttackBonusCalculation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaveDCCalculation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaveAttribute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Damage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DamageType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.ActionId);
                });

            migrationBuilder.CreateTable(
                name: "CharacterActions",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterActions", x => new { x.CharacterId, x.ActionId });
                    table.ForeignKey(
                        name: "FK_CharacterActions_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "ActionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterActions_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterActions_ActionId",
                table: "CharacterActions",
                column: "ActionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterActions");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KnownSpells",
                table: "KnownSpells");

            migrationBuilder.AlterColumn<int>(
                name: "ProficiencyId",
                table: "CharacterProficiencies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "CharacterProficiencies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KnownSpells",
                table: "KnownSpells",
                columns: new[] { "CharacterId", "SpellId" });

            migrationBuilder.CreateIndex(
                name: "IX_KnownSpells_CharacterId_SpellcastingId",
                table: "KnownSpells",
                columns: new[] { "CharacterId", "SpellcastingId" });
        }
    }
}
