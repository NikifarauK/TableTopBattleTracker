using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTopBattleTracker.Migrations
{
    public partial class SpellcastingSlotUsageNoSchoolNoAbilityValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Times",
                table: "spellcasting_slots");

            migrationBuilder.DropColumn(
                name: "School",
                table: "spellcasting");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "special_ability");

            migrationBuilder.AddColumn<int>(
                name: "UsageId",
                table: "spellcasting_slots",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_spellcasting_slots_UsageId",
                table: "spellcasting_slots",
                column: "UsageId");

            migrationBuilder.AddForeignKey(
                name: "FK_spellcasting_slots_usage_UsageId",
                table: "spellcasting_slots",
                column: "UsageId",
                principalTable: "usage",
                principalColumn: "UsageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spellcasting_slots_usage_UsageId",
                table: "spellcasting_slots");

            migrationBuilder.DropIndex(
                name: "IX_spellcasting_slots_UsageId",
                table: "spellcasting_slots");

            migrationBuilder.DropColumn(
                name: "UsageId",
                table: "spellcasting_slots");

            migrationBuilder.AddColumn<int>(
                name: "Times",
                table: "spellcasting_slots",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "spellcasting",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "special_ability",
                type: "integer",
                nullable: true);
        }
    }
}
