using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TableTopBattleTracker.Migrations
{
    public partial class MultipleDamageTypesInSpell : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spell_damage_values_spell_damages_SpellDamageId",
                table: "spell_damage_values");

            migrationBuilder.DropForeignKey(
                name: "FK_spells_spell_damages_SpellDamageId",
                table: "spells");

            migrationBuilder.DropIndex(
                name: "IX_spells_SpellDamageId",
                table: "spells");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spell_damages",
                table: "spell_damages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spell_damage_values",
                table: "spell_damage_values");

            migrationBuilder.DropColumn(
                name: "SpellDamageId",
                table: "spells");

            migrationBuilder.RenameColumn(
                name: "SpellDamageId",
                table: "spell_damages",
                newName: "SpellId");

            migrationBuilder.RenameColumn(
                name: "SpellDamageId",
                table: "spell_damage_values",
                newName: "SpellId");

            migrationBuilder.AlterColumn<byte>(
                name: "DamageTypeId",
                table: "spell_damages",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "SpellId",
                table: "spell_damages",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "spell_damage_values",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<byte>(
                name: "DamageTypeId",
                table: "spell_damage_values",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_spell_damages",
                table: "spell_damages",
                columns: new[] { "SpellId", "DamageTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_spell_damage_values",
                table: "spell_damage_values",
                columns: new[] { "SpellId", "DamageTypeId", "Level" });

            migrationBuilder.AddForeignKey(
                name: "FK_spell_damage_values_spell_damages_SpellId_DamageTypeId",
                table: "spell_damage_values",
                columns: new[] { "SpellId", "DamageTypeId" },
                principalTable: "spell_damages",
                principalColumns: new[] { "SpellId", "DamageTypeId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spell_damages_spells_SpellId",
                table: "spell_damages",
                column: "SpellId",
                principalTable: "spells",
                principalColumn: "SpellId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spell_damage_values_spell_damages_SpellId_DamageTypeId",
                table: "spell_damage_values");

            migrationBuilder.DropForeignKey(
                name: "FK_spell_damages_spells_SpellId",
                table: "spell_damages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spell_damages",
                table: "spell_damages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spell_damage_values",
                table: "spell_damage_values");

            migrationBuilder.DropColumn(
                name: "DamageTypeId",
                table: "spell_damage_values");

            migrationBuilder.RenameColumn(
                name: "SpellId",
                table: "spell_damages",
                newName: "SpellDamageId");

            migrationBuilder.RenameColumn(
                name: "SpellId",
                table: "spell_damage_values",
                newName: "SpellDamageId");

            migrationBuilder.AddColumn<int>(
                name: "SpellDamageId",
                table: "spells",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<byte>(
                name: "DamageTypeId",
                table: "spell_damages",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "SpellDamageId",
                table: "spell_damages",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "spell_damage_values",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_spell_damages",
                table: "spell_damages",
                column: "SpellDamageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_spell_damage_values",
                table: "spell_damage_values",
                columns: new[] { "SpellDamageId", "Level" });

            migrationBuilder.CreateIndex(
                name: "IX_spells_SpellDamageId",
                table: "spells",
                column: "SpellDamageId");

            migrationBuilder.AddForeignKey(
                name: "FK_spell_damage_values_spell_damages_SpellDamageId",
                table: "spell_damage_values",
                column: "SpellDamageId",
                principalTable: "spell_damages",
                principalColumn: "SpellDamageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spells_spell_damages_SpellDamageId",
                table: "spells",
                column: "SpellDamageId",
                principalTable: "spell_damages",
                principalColumn: "SpellDamageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
