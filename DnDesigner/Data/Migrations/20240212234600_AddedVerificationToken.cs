using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDesigner.Migrations
{
    /// <inheritdoc />
    public partial class AddedVerificationToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "VerificationToken",
            table: "AspNetUsers",
            maxLength: 50,
            nullable: true,
            defaultValueSql: "NEWID()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "VerificationToken",
            table: "AspNetUsers");
        }
    }
}
