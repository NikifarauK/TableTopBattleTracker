using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTopBattleTracker.Migrations
{
    public partial class SpecialAbilitiesDescAndValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_special_ability_usage_UsageId",
                table: "special_ability");

            migrationBuilder.AlterColumn<int>(
                name: "UsageId",
                table: "special_ability",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "special_ability",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "special_ability",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_special_ability_usage_UsageId",
                table: "special_ability",
                column: "UsageId",
                principalTable: "usage",
                principalColumn: "UsageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_special_ability_usage_UsageId",
                table: "special_ability");

            migrationBuilder.DropColumn(
                name: "Desc",
                table: "special_ability");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "special_ability");

            migrationBuilder.AlterColumn<int>(
                name: "UsageId",
                table: "special_ability",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_special_ability_usage_UsageId",
                table: "special_ability",
                column: "UsageId",
                principalTable: "usage",
                principalColumn: "UsageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
