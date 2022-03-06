using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TableTopBattleTracker.Migrations
{
    public partial class ModelCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "action",
                columns: table => new
                {
                    ActionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Distance = table.Column<int>(type: "integer", nullable: false),
                    Reach = table.Column<int>(type: "integer", nullable: false),
                    Desc = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_action", x => x.ActionId);
                });

            migrationBuilder.CreateTable(
                name: "area_of_effect",
                columns: table => new
                {
                    AreaOfEffectId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_area_of_effect", x => x.AreaOfEffectId);
                });

            migrationBuilder.CreateTable(
                name: "cast_range",
                columns: table => new
                {
                    CastRangeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cast_range", x => x.CastRangeId);
                });

            migrationBuilder.CreateTable(
                name: "cast_time",
                columns: table => new
                {
                    CastTimeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cast_time", x => x.CastTimeId);
                });

            migrationBuilder.CreateTable(
                name: "conditions",
                columns: table => new
                {
                    ConditionId = table.Column<byte>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Desc = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conditions", x => x.ConditionId);
                });

            migrationBuilder.CreateTable(
                name: "monster_languges",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monster_languges", x => new { x.CharacterId, x.LanguageId });
                });

            migrationBuilder.CreateTable(
                name: "monster_size",
                columns: table => new
                {
                    MonsterSizeId = table.Column<byte>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    SpaceModifier = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monster_size", x => x.MonsterSizeId);
                });

            migrationBuilder.CreateTable(
                name: "monster_types",
                columns: table => new
                {
                    MonsterTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monster_types", x => x.MonsterTypeId);
                });

            migrationBuilder.CreateTable(
                name: "proficiencies",
                columns: table => new
                {
                    ProficiencyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proficiencies", x => x.ProficiencyId);
                });

            migrationBuilder.CreateTable(
                name: "senses",
                columns: table => new
                {
                    SenseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Desc = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_senses", x => x.SenseId);
                });

            migrationBuilder.CreateTable(
                name: "slots",
                columns: table => new
                {
                    SlotId = table.Column<byte>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_slots", x => x.SlotId);
                });

            migrationBuilder.CreateTable(
                name: "speed_type",
                columns: table => new
                {
                    SpeedTypeId = table.Column<byte>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_speed_type", x => x.SpeedTypeId);
                });

            migrationBuilder.CreateTable(
                name: "spell_school",
                columns: table => new
                {
                    SpellSchoolId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spell_school", x => x.SpellSchoolId);
                });

            migrationBuilder.CreateTable(
                name: "usage",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Times = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usage", x => x.UsageId);
                });

            migrationBuilder.CreateTable(
                name: "characteristics",
                columns: table => new
                {
                    CharacteristicId = table.Column<byte>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    Desc = table.Column<string>(type: "text", nullable: true),
                    ActionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characteristics", x => x.CharacteristicId);
                    table.ForeignKey(
                        name: "FK_characteristics_action_ActionId",
                        column: x => x.ActionId,
                        principalTable: "action",
                        principalColumn: "ActionId");
                });

            migrationBuilder.CreateTable(
                name: "damage_types",
                columns: table => new
                {
                    DamageTypeId = table.Column<byte>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ActionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_damage_types", x => x.DamageTypeId);
                    table.ForeignKey(
                        name: "FK_damage_types_action_ActionId",
                        column: x => x.ActionId,
                        principalTable: "action",
                        principalColumn: "ActionId");
                });

            migrationBuilder.CreateTable(
                name: "characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ArmorClass = table.Column<int>(type: "integer", nullable: false),
                    Allignment = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ChallengeRating = table.Column<float>(type: "real", nullable: false),
                    HitDice = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    InitialHitPoints = table.Column<int>(type: "integer", nullable: false),
                    CurrentHitPoints = table.Column<int>(type: "integer", nullable: false),
                    Charisma = table.Column<int>(type: "integer", nullable: false),
                    Constitution = table.Column<int>(type: "integer", nullable: false),
                    Dexterity = table.Column<int>(type: "integer", nullable: false),
                    Intelligence = table.Column<int>(type: "integer", nullable: false),
                    Strength = table.Column<int>(type: "integer", nullable: false),
                    Wisdom = table.Column<int>(type: "integer", nullable: false),
                    Experience = table.Column<int>(type: "integer", nullable: false),
                    ProficiencyBonus = table.Column<int>(type: "integer", nullable: false),
                    MonsterTypeId = table.Column<int>(type: "integer", nullable: true),
                    MonsterSizeId = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_characters_monster_size_MonsterSizeId",
                        column: x => x.MonsterSizeId,
                        principalTable: "monster_size",
                        principalColumn: "MonsterSizeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_characters_monster_types_MonsterTypeId",
                        column: x => x.MonsterTypeId,
                        principalTable: "monster_types",
                        principalColumn: "MonsterTypeId");
                });

            migrationBuilder.CreateTable(
                name: "action_dc_types",
                columns: table => new
                {
                    ActionId = table.Column<int>(type: "integer", nullable: false),
                    CharacteristicId = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_action_dc_types", x => new { x.ActionId, x.CharacteristicId });
                    table.ForeignKey(
                        name: "FK_action_dc_types_action_ActionId",
                        column: x => x.ActionId,
                        principalTable: "action",
                        principalColumn: "ActionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_action_dc_types_characteristics_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "characteristics",
                        principalColumn: "CharacteristicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spellcasting",
                columns: table => new
                {
                    SpellcastingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacteristicId = table.Column<byte>(type: "smallint", nullable: false),
                    DifficultyClass = table.Column<int>(type: "integer", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Modifier = table.Column<int>(type: "integer", nullable: false),
                    School = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spellcasting", x => x.SpellcastingId);
                    table.ForeignKey(
                        name: "FK_spellcasting_characteristics_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "characteristics",
                        principalColumn: "CharacteristicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "action_damage_types",
                columns: table => new
                {
                    ActionId = table.Column<int>(type: "integer", nullable: false),
                    DamagetypeId = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_action_damage_types", x => new { x.ActionId, x.DamagetypeId });
                    table.ForeignKey(
                        name: "FK_action_damage_types_action_ActionId",
                        column: x => x.ActionId,
                        principalTable: "action",
                        principalColumn: "ActionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_action_damage_types_damage_types_DamagetypeId",
                        column: x => x.DamagetypeId,
                        principalTable: "damage_types",
                        principalColumn: "DamageTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spell_damages",
                columns: table => new
                {
                    SpellDamageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DamageTypeId = table.Column<byte>(type: "smallint", nullable: false),
                    IncreaseType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spell_damages", x => x.SpellDamageId);
                    table.ForeignKey(
                        name: "FK_spell_damages_damage_types_DamageTypeId",
                        column: x => x.DamageTypeId,
                        principalTable: "damage_types",
                        principalColumn: "DamageTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "condition_immunities",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    ConditionId = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_condition_immunities", x => new { x.CharacterId, x.ConditionId });
                    table.ForeignKey(
                        name: "FK_condition_immunities_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_condition_immunities_conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "conditions",
                        principalColumn: "ConditionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "damage_immunities",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    DamageTypeId = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_damage_immunities", x => new { x.CharacterId, x.DamageTypeId });
                    table.ForeignKey(
                        name: "FK_damage_immunities_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_damage_immunities_damage_types_DamageTypeId",
                        column: x => x.DamageTypeId,
                        principalTable: "damage_types",
                        principalColumn: "DamageTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "damage_resistances",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    DamageTypeId = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_damage_resistances", x => new { x.CharacterId, x.DamageTypeId });
                    table.ForeignKey(
                        name: "FK_damage_resistances_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_damage_resistances_damage_types_DamageTypeId",
                        column: x => x.DamageTypeId,
                        principalTable: "damage_types",
                        principalColumn: "DamageTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "damage_vulnerabilities",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    DamageTypeId = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_damage_vulnerabilities", x => new { x.CharacterId, x.DamageTypeId });
                    table.ForeignKey(
                        name: "FK_damage_vulnerabilities_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_damage_vulnerabilities_damage_types_DamageTypeId",
                        column: x => x.DamageTypeId,
                        principalTable: "damage_types",
                        principalColumn: "DamageTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CharacterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_languages", x => x.LanguageId);
                    table.ForeignKey(
                        name: "FK_languages_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId");
                });

            migrationBuilder.CreateTable(
                name: "legendary_actions",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    ActionId = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<int>(type: "integer", nullable: false),
                    DamageDice = table.Column<string>(type: "text", nullable: true),
                    DC = table.Column<int>(type: "integer", nullable: true),
                    BonusToHit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_legendary_actions", x => new { x.CharacterId, x.ActionId });
                    table.ForeignKey(
                        name: "FK_legendary_actions_action_ActionId",
                        column: x => x.ActionId,
                        principalTable: "action",
                        principalColumn: "ActionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_legendary_actions_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "monster_action",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    ActionId = table.Column<int>(type: "integer", nullable: false),
                    DamageDice = table.Column<string>(type: "text", nullable: true),
                    DifficultyClass = table.Column<int>(type: "integer", nullable: true),
                    BonusToHit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monster_action", x => new { x.CharacterId, x.ActionId });
                    table.ForeignKey(
                        name: "FK_monster_action_action_ActionId",
                        column: x => x.ActionId,
                        principalTable: "action",
                        principalColumn: "ActionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_monster_action_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "monster_proficiency",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    ProficiencyId = table.Column<int>(type: "integer", nullable: false),
                    Modifier = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monster_proficiency", x => new { x.CharacterId, x.ProficiencyId });
                    table.ForeignKey(
                        name: "FK_monster_proficiency_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_monster_proficiency_proficiencies_ProficiencyId",
                        column: x => x.ProficiencyId,
                        principalTable: "proficiencies",
                        principalColumn: "ProficiencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "monster_senses",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    SenseId = table.Column<int>(type: "integer", nullable: false),
                    Distance = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monster_senses", x => new { x.CharacterId, x.SenseId });
                    table.ForeignKey(
                        name: "FK_monster_senses_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_monster_senses_senses_SenseId",
                        column: x => x.SenseId,
                        principalTable: "senses",
                        principalColumn: "SenseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "monster_speed",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    SpeedTypeId = table.Column<byte>(type: "smallint", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monster_speed", x => new { x.CharacterId, x.SpeedTypeId });
                    table.ForeignKey(
                        name: "FK_monster_speed_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_monster_speed_speed_type_SpeedTypeId",
                        column: x => x.SpeedTypeId,
                        principalTable: "speed_type",
                        principalColumn: "SpeedTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "special_ability",
                columns: table => new
                {
                    SpecialAbilityId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    UsageId = table.Column<int>(type: "integer", nullable: false),
                    SpellcastingId = table.Column<int>(type: "integer", nullable: true),
                    CharacterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_special_ability", x => x.SpecialAbilityId);
                    table.ForeignKey(
                        name: "FK_special_ability_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId");
                    table.ForeignKey(
                        name: "FK_special_ability_spellcasting_SpellcastingId",
                        column: x => x.SpellcastingId,
                        principalTable: "spellcasting",
                        principalColumn: "SpellcastingId");
                    table.ForeignKey(
                        name: "FK_special_ability_usage_UsageId",
                        column: x => x.UsageId,
                        principalTable: "usage",
                        principalColumn: "UsageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spellcasting_slots",
                columns: table => new
                {
                    SpellcastingId = table.Column<int>(type: "integer", nullable: false),
                    SlotId = table.Column<byte>(type: "smallint", nullable: false),
                    Times = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spellcasting_slots", x => new { x.SpellcastingId, x.SlotId });
                    table.ForeignKey(
                        name: "FK_spellcasting_slots_slots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "slots",
                        principalColumn: "SlotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spellcasting_slots_spellcasting_SpellcastingId",
                        column: x => x.SpellcastingId,
                        principalTable: "spellcasting",
                        principalColumn: "SpellcastingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spellcasting_spells",
                columns: table => new
                {
                    SpellId = table.Column<int>(type: "integer", nullable: false),
                    SpellcastingId = table.Column<int>(type: "integer", nullable: false),
                    UsageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spellcasting_spells", x => new { x.SpellId, x.SpellcastingId });
                    table.ForeignKey(
                        name: "FK_spellcasting_spells_spellcasting_SpellcastingId",
                        column: x => x.SpellcastingId,
                        principalTable: "spellcasting",
                        principalColumn: "SpellcastingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spellcasting_spells_usage_UsageId",
                        column: x => x.UsageId,
                        principalTable: "usage",
                        principalColumn: "UsageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spell_damage_values",
                columns: table => new
                {
                    SpellDamageId = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spell_damage_values", x => new { x.SpellDamageId, x.Level });
                    table.ForeignKey(
                        name: "FK_spell_damage_values_spell_damages_SpellDamageId",
                        column: x => x.SpellDamageId,
                        principalTable: "spell_damages",
                        principalColumn: "SpellDamageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spells",
                columns: table => new
                {
                    SpellId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: false),
                    Level = table.Column<byte>(type: "smallint", nullable: false),
                    Desc = table.Column<string>(type: "text", nullable: true),
                    Materials = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    CastRangeId = table.Column<int>(type: "integer", nullable: false),
                    CastTimeId = table.Column<int>(type: "integer", nullable: false),
                    IsRitual = table.Column<bool>(type: "boolean", nullable: false),
                    IsConcetration = table.Column<bool>(type: "boolean", nullable: false),
                    Duration = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    DC = table.Column<byte>(type: "smallint", nullable: false),
                    SpellDamageId = table.Column<int>(type: "integer", nullable: false),
                    AreaOfEffectId = table.Column<int>(type: "integer", nullable: false),
                    AreasSize = table.Column<int>(type: "integer", nullable: false),
                    SpellSchoolId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spells", x => x.SpellId);
                    table.ForeignKey(
                        name: "FK_spells_area_of_effect_AreaOfEffectId",
                        column: x => x.AreaOfEffectId,
                        principalTable: "area_of_effect",
                        principalColumn: "AreaOfEffectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spells_cast_range_CastRangeId",
                        column: x => x.CastRangeId,
                        principalTable: "cast_range",
                        principalColumn: "CastRangeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spells_cast_time_CastTimeId",
                        column: x => x.CastTimeId,
                        principalTable: "cast_time",
                        principalColumn: "CastTimeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spells_spell_damages_SpellDamageId",
                        column: x => x.SpellDamageId,
                        principalTable: "spell_damages",
                        principalColumn: "SpellDamageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spells_spell_school_SpellSchoolId",
                        column: x => x.SpellSchoolId,
                        principalTable: "spell_school",
                        principalColumn: "SpellSchoolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "multiattack",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    MonsterActionId = table.Column<int>(type: "integer", nullable: false),
                    MonsterActionCharacterId = table.Column<int>(type: "integer", nullable: true),
                    MonsterActionActionId = table.Column<int>(type: "integer", nullable: true),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_multiattack", x => new { x.CharacterId, x.MonsterActionId });
                    table.ForeignKey(
                        name: "FK_multiattack_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_multiattack_monster_action_MonsterActionCharacterId_Monster~",
                        columns: x => new { x.MonsterActionCharacterId, x.MonsterActionActionId },
                        principalTable: "monster_action",
                        principalColumns: new[] { "CharacterId", "ActionId" });
                });

            migrationBuilder.CreateTable(
                name: "casting_components",
                columns: table => new
                {
                    CastingComponentId = table.Column<byte>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    SpellId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_casting_components", x => x.CastingComponentId);
                    table.ForeignKey(
                        name: "FK_casting_components_spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "spells",
                        principalColumn: "SpellId");
                });

            migrationBuilder.CreateTable(
                name: "spell_casting_components",
                columns: table => new
                {
                    SpellId = table.Column<int>(type: "integer", nullable: false),
                    CastingComponentId = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spell_casting_components", x => new { x.SpellId, x.CastingComponentId });
                    table.ForeignKey(
                        name: "FK_spell_casting_components_casting_components_CastingComponen~",
                        column: x => x.CastingComponentId,
                        principalTable: "casting_components",
                        principalColumn: "CastingComponentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spell_casting_components_spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "spells",
                        principalColumn: "SpellId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_action_damage_types_DamagetypeId",
                table: "action_damage_types",
                column: "DamagetypeId");

            migrationBuilder.CreateIndex(
                name: "IX_action_dc_types_CharacteristicId",
                table: "action_dc_types",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_casting_components_SpellId",
                table: "casting_components",
                column: "SpellId");

            migrationBuilder.CreateIndex(
                name: "IX_characteristics_ActionId",
                table: "characteristics",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_characters_MonsterSizeId",
                table: "characters",
                column: "MonsterSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_characters_MonsterTypeId",
                table: "characters",
                column: "MonsterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_condition_immunities_ConditionId",
                table: "condition_immunities",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_damage_immunities_DamageTypeId",
                table: "damage_immunities",
                column: "DamageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_damage_resistances_DamageTypeId",
                table: "damage_resistances",
                column: "DamageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_damage_types_ActionId",
                table: "damage_types",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_damage_vulnerabilities_DamageTypeId",
                table: "damage_vulnerabilities",
                column: "DamageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_languages_CharacterId",
                table: "languages",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_legendary_actions_ActionId",
                table: "legendary_actions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_monster_action_ActionId",
                table: "monster_action",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_monster_proficiency_ProficiencyId",
                table: "monster_proficiency",
                column: "ProficiencyId");

            migrationBuilder.CreateIndex(
                name: "IX_monster_senses_SenseId",
                table: "monster_senses",
                column: "SenseId");

            migrationBuilder.CreateIndex(
                name: "IX_monster_speed_SpeedTypeId",
                table: "monster_speed",
                column: "SpeedTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_multiattack_MonsterActionCharacterId_MonsterActionActionId",
                table: "multiattack",
                columns: new[] { "MonsterActionCharacterId", "MonsterActionActionId" });

            migrationBuilder.CreateIndex(
                name: "IX_special_ability_CharacterId",
                table: "special_ability",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_special_ability_SpellcastingId",
                table: "special_ability",
                column: "SpellcastingId");

            migrationBuilder.CreateIndex(
                name: "IX_special_ability_UsageId",
                table: "special_ability",
                column: "UsageId");

            migrationBuilder.CreateIndex(
                name: "IX_spell_casting_components_CastingComponentId",
                table: "spell_casting_components",
                column: "CastingComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_spell_damages_DamageTypeId",
                table: "spell_damages",
                column: "DamageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_spellcasting_CharacteristicId",
                table: "spellcasting",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_spellcasting_slots_SlotId",
                table: "spellcasting_slots",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_spellcasting_spells_SpellcastingId",
                table: "spellcasting_spells",
                column: "SpellcastingId");

            migrationBuilder.CreateIndex(
                name: "IX_spellcasting_spells_UsageId",
                table: "spellcasting_spells",
                column: "UsageId");

            migrationBuilder.CreateIndex(
                name: "IX_spells_AreaOfEffectId",
                table: "spells",
                column: "AreaOfEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_spells_CastRangeId",
                table: "spells",
                column: "CastRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_spells_CastTimeId",
                table: "spells",
                column: "CastTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_spells_SpellDamageId",
                table: "spells",
                column: "SpellDamageId");

            migrationBuilder.CreateIndex(
                name: "IX_spells_SpellSchoolId",
                table: "spells",
                column: "SpellSchoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "action_damage_types");

            migrationBuilder.DropTable(
                name: "action_dc_types");

            migrationBuilder.DropTable(
                name: "condition_immunities");

            migrationBuilder.DropTable(
                name: "damage_immunities");

            migrationBuilder.DropTable(
                name: "damage_resistances");

            migrationBuilder.DropTable(
                name: "damage_vulnerabilities");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropTable(
                name: "legendary_actions");

            migrationBuilder.DropTable(
                name: "monster_languges");

            migrationBuilder.DropTable(
                name: "monster_proficiency");

            migrationBuilder.DropTable(
                name: "monster_senses");

            migrationBuilder.DropTable(
                name: "monster_speed");

            migrationBuilder.DropTable(
                name: "multiattack");

            migrationBuilder.DropTable(
                name: "special_ability");

            migrationBuilder.DropTable(
                name: "spell_casting_components");

            migrationBuilder.DropTable(
                name: "spell_damage_values");

            migrationBuilder.DropTable(
                name: "spellcasting_slots");

            migrationBuilder.DropTable(
                name: "spellcasting_spells");

            migrationBuilder.DropTable(
                name: "conditions");

            migrationBuilder.DropTable(
                name: "proficiencies");

            migrationBuilder.DropTable(
                name: "senses");

            migrationBuilder.DropTable(
                name: "speed_type");

            migrationBuilder.DropTable(
                name: "monster_action");

            migrationBuilder.DropTable(
                name: "casting_components");

            migrationBuilder.DropTable(
                name: "slots");

            migrationBuilder.DropTable(
                name: "spellcasting");

            migrationBuilder.DropTable(
                name: "usage");

            migrationBuilder.DropTable(
                name: "characters");

            migrationBuilder.DropTable(
                name: "spells");

            migrationBuilder.DropTable(
                name: "characteristics");

            migrationBuilder.DropTable(
                name: "monster_size");

            migrationBuilder.DropTable(
                name: "monster_types");

            migrationBuilder.DropTable(
                name: "area_of_effect");

            migrationBuilder.DropTable(
                name: "cast_range");

            migrationBuilder.DropTable(
                name: "cast_time");

            migrationBuilder.DropTable(
                name: "spell_damages");

            migrationBuilder.DropTable(
                name: "spell_school");

            migrationBuilder.DropTable(
                name: "damage_types");

            migrationBuilder.DropTable(
                name: "action");
        }
    }
}
