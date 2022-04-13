using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TableTopBattleTracker.Migrations
{
    public partial class CharacterAllignmentAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allignment",
                table: "characters");

            migrationBuilder.AddColumn<int>(
                name: "AllignmentId",
                table: "characters",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "allignment",
                columns: table => new
                {
                    AllignmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allignment", x => x.AllignmentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_characters_AllignmentId",
                table: "characters",
                column: "AllignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_characters_allignment_AllignmentId",
                table: "characters",
                column: "AllignmentId",
                principalTable: "allignment",
                principalColumn: "AllignmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characters_allignment_AllignmentId",
                table: "characters");

            migrationBuilder.DropTable(
                name: "allignment");

            migrationBuilder.DropIndex(
                name: "IX_characters_AllignmentId",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "AllignmentId",
                table: "characters");

            migrationBuilder.AddColumn<string>(
                name: "Allignment",
                table: "characters",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);
        }
    }
}
