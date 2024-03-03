using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Migrations
{
    /// <inheritdoc />
    public partial class Sources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subclasses_Classes_ClassId",
                table: "Subclasses");

            migrationBuilder.DropColumn(
                name: "Sourcebook",
                table: "Subclasses");

            migrationBuilder.DropColumn(
                name: "Sourcebook",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "Sourcebook",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "Sourcebook",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Sourcebook",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Sourcebook",
                table: "Backgrounds");

            migrationBuilder.AddColumn<int>(
                name: "SourceBookSourceId",
                table: "Subclasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "Subclasses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceBookSourceId",
                table: "Spells",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceBookSourceId",
                table: "Races",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "Races",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceBookSourceId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceBookSourceId",
                table: "Features",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "Features",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceId1",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceBookSourceId",
                table: "Backgrounds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "Backgrounds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Source",
                columns: table => new
                {
                    SourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.SourceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subclasses_SourceBookSourceId",
                table: "Subclasses",
                column: "SourceBookSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Subclasses_SourceId",
                table: "Subclasses",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_SourceBookSourceId",
                table: "Spells",
                column: "SourceBookSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_SourceBookSourceId",
                table: "Races",
                column: "SourceBookSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_SourceId",
                table: "Races",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SourceBookSourceId",
                table: "Items",
                column: "SourceBookSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SourceId",
                table: "Items",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_SourceBookSourceId",
                table: "Features",
                column: "SourceBookSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_SourceId",
                table: "Features",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SourceId",
                table: "Classes",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SourceId1",
                table: "Classes",
                column: "SourceId1");

            migrationBuilder.CreateIndex(
                name: "IX_Backgrounds_SourceBookSourceId",
                table: "Backgrounds",
                column: "SourceBookSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Backgrounds_SourceId",
                table: "Backgrounds",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Backgrounds_Source_SourceBookSourceId",
                table: "Backgrounds",
                column: "SourceBookSourceId",
                principalTable: "Source",
                principalColumn: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Backgrounds_Source_SourceId",
                table: "Backgrounds",
                column: "SourceId",
                principalTable: "Source",
                principalColumn: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Source_SourceId",
                table: "Classes",
                column: "SourceId",
                principalTable: "Source",
                principalColumn: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Source_SourceId1",
                table: "Classes",
                column: "SourceId1",
                principalTable: "Source",
                principalColumn: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Source_SourceBookSourceId",
                table: "Features",
                column: "SourceBookSourceId",
                principalTable: "Source",
                principalColumn: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Source_SourceId",
                table: "Features",
                column: "SourceId",
                principalTable: "Source",
                principalColumn: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Source_SourceBookSourceId",
                table: "Items",
                column: "SourceBookSourceId",
                principalTable: "Source",
                principalColumn: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Source_SourceId",
                table: "Items",
                column: "SourceId",
                principalTable: "Source",
                principalColumn: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Source_SourceBookSourceId",
                table: "Races",
                column: "SourceBookSourceId",
                principalTable: "Source",
                principalColumn: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Source_SourceId",
                table: "Races",
                column: "SourceId",
                principalTable: "Source",
                principalColumn: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spells_Source_SourceBookSourceId",
                table: "Spells",
                column: "SourceBookSourceId",
                principalTable: "Source",
                principalColumn: "SourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subclasses_Classes_ClassId",
                table: "Subclasses",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subclasses_Source_SourceBookSourceId",
                table: "Subclasses",
                column: "SourceBookSourceId",
                principalTable: "Source",
                principalColumn: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subclasses_Source_SourceId",
                table: "Subclasses",
                column: "SourceId",
                principalTable: "Source",
                principalColumn: "SourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Backgrounds_Source_SourceBookSourceId",
                table: "Backgrounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Backgrounds_Source_SourceId",
                table: "Backgrounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Source_SourceId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Source_SourceId1",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_Source_SourceBookSourceId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_Source_SourceId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Source_SourceBookSourceId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Source_SourceId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Races_Source_SourceBookSourceId",
                table: "Races");

            migrationBuilder.DropForeignKey(
                name: "FK_Races_Source_SourceId",
                table: "Races");

            migrationBuilder.DropForeignKey(
                name: "FK_Spells_Source_SourceBookSourceId",
                table: "Spells");

            migrationBuilder.DropForeignKey(
                name: "FK_Subclasses_Classes_ClassId",
                table: "Subclasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Subclasses_Source_SourceBookSourceId",
                table: "Subclasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Subclasses_Source_SourceId",
                table: "Subclasses");

            migrationBuilder.DropTable(
                name: "Source");

            migrationBuilder.DropIndex(
                name: "IX_Subclasses_SourceBookSourceId",
                table: "Subclasses");

            migrationBuilder.DropIndex(
                name: "IX_Subclasses_SourceId",
                table: "Subclasses");

            migrationBuilder.DropIndex(
                name: "IX_Spells_SourceBookSourceId",
                table: "Spells");

            migrationBuilder.DropIndex(
                name: "IX_Races_SourceBookSourceId",
                table: "Races");

            migrationBuilder.DropIndex(
                name: "IX_Races_SourceId",
                table: "Races");

            migrationBuilder.DropIndex(
                name: "IX_Items_SourceBookSourceId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_SourceId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Features_SourceBookSourceId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_SourceId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SourceId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SourceId1",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Backgrounds_SourceBookSourceId",
                table: "Backgrounds");

            migrationBuilder.DropIndex(
                name: "IX_Backgrounds_SourceId",
                table: "Backgrounds");

            migrationBuilder.DropColumn(
                name: "SourceBookSourceId",
                table: "Subclasses");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "Subclasses");

            migrationBuilder.DropColumn(
                name: "SourceBookSourceId",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "SourceBookSourceId",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "SourceBookSourceId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SourceBookSourceId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SourceId1",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SourceBookSourceId",
                table: "Backgrounds");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "Backgrounds");

            migrationBuilder.AddColumn<string>(
                name: "Sourcebook",
                table: "Subclasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sourcebook",
                table: "Spells",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sourcebook",
                table: "Races",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sourcebook",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Features",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sourcebook",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sourcebook",
                table: "Backgrounds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Subclasses_Classes_ClassId",
                table: "Subclasses",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
