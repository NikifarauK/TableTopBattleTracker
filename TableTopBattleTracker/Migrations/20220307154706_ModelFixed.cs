using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTopBattleTracker.Migrations
{
    public partial class ModelFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_casting_components_spells_SpellId",
                table: "casting_components");

            migrationBuilder.DropForeignKey(
                name: "FK_characteristics_action_ActionId",
                table: "characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_damage_types_action_ActionId",
                table: "damage_types");

            migrationBuilder.DropForeignKey(
                name: "FK_languages_characters_CharacterId",
                table: "languages");

            migrationBuilder.DropIndex(
                name: "IX_languages_CharacterId",
                table: "languages");

            migrationBuilder.DropIndex(
                name: "IX_damage_types_ActionId",
                table: "damage_types");

            migrationBuilder.DropIndex(
                name: "IX_characteristics_ActionId",
                table: "characteristics");

            migrationBuilder.DropIndex(
                name: "IX_casting_components_SpellId",
                table: "casting_components");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "languages");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "damage_types");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "characteristics");

            migrationBuilder.DropColumn(
                name: "SpellId",
                table: "casting_components");

            migrationBuilder.CreateIndex(
                name: "IX_monster_languges_LanguageId",
                table: "monster_languges",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_monster_languges_characters_CharacterId",
                table: "monster_languges",
                column: "CharacterId",
                principalTable: "characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_monster_languges_languages_LanguageId",
                table: "monster_languges",
                column: "LanguageId",
                principalTable: "languages",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_monster_languges_characters_CharacterId",
                table: "monster_languges");

            migrationBuilder.DropForeignKey(
                name: "FK_monster_languges_languages_LanguageId",
                table: "monster_languges");

            migrationBuilder.DropIndex(
                name: "IX_monster_languges_LanguageId",
                table: "monster_languges");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "languages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                table: "damage_types",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                table: "characteristics",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpellId",
                table: "casting_components",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_languages_CharacterId",
                table: "languages",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_damage_types_ActionId",
                table: "damage_types",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_characteristics_ActionId",
                table: "characteristics",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_casting_components_SpellId",
                table: "casting_components",
                column: "SpellId");

            migrationBuilder.AddForeignKey(
                name: "FK_casting_components_spells_SpellId",
                table: "casting_components",
                column: "SpellId",
                principalTable: "spells",
                principalColumn: "SpellId");

            migrationBuilder.AddForeignKey(
                name: "FK_characteristics_action_ActionId",
                table: "characteristics",
                column: "ActionId",
                principalTable: "action",
                principalColumn: "ActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_damage_types_action_ActionId",
                table: "damage_types",
                column: "ActionId",
                principalTable: "action",
                principalColumn: "ActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_languages_characters_CharacterId",
                table: "languages",
                column: "CharacterId",
                principalTable: "characters",
                principalColumn: "CharacterId");
        }
    }
}
