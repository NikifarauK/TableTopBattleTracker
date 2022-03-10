using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TableTopBattleTracker.Migrations
{
    public partial class ModelFixedLittleMore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spells_area_of_effect_AreaOfEffectId",
                table: "spells");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "area_of_effect",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "AreasSize",
                table: "spells",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<byte>(
                name: "AreaOfEffectId",
                table: "spells",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<byte>(
                name: "AreaOfEffectId",
                table: "area_of_effect",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_spells_area_of_effect_AreaOfEffectId",
                table: "spells",
                column: "AreaOfEffectId",
                principalTable: "area_of_effect",
                principalColumn: "AreaOfEffectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spells_area_of_effect_AreaOfEffectId",
                table: "spells");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "area_of_effect",
                newName: "Type");

            migrationBuilder.AlterColumn<int>(
                name: "AreasSize",
                table: "spells",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaOfEffectId",
                table: "spells",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaOfEffectId",
                table: "area_of_effect",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_spells_area_of_effect_AreaOfEffectId",
                table: "spells",
                column: "AreaOfEffectId",
                principalTable: "area_of_effect",
                principalColumn: "AreaOfEffectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
