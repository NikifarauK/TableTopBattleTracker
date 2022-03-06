using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("characters")]
    public class Character
    {
        [Key]
        public int CharacterId { get; set; }

        [MaxLength(64)]
        public string? Name { get; set; }

        //TODO: return $'/monsters/{CharacterId}'
        //public string? Url { get; set; }

        public int ArmorClass { get; set; }

        [MaxLength(128)]
        public string? Allignment { get; set; }

        public float ChallengeRating { get; set; }

        [MaxLength(16)]
        public string? HitDice { get; set; }

        public int InitialHitPoints { get; set; }
        
        public int CurrentHitPoints { get; set; }

        public int Charisma { get; set; }

        public int Constitution { get; set; }

        public int Dexterity { get; set; }

        public int Intelligence { get; set; }

        public int Strength { get; set; }

        public int Wisdom { get; set; }

        public int Experience { get; set; }

        public int ProficiencyBonus { get; set; }

        [NotMapped]
        public int InitiativeModifier
        {
            get => (Dexterity - 10) >> 1;
        }

        public MonsterType? MonsterType { get; set; }

        public virtual ICollection<MonsterSpeed>? Speeds { get; set; }

        public EMonsterSize MonsterSizeId { get; set; }
        public virtual MonsterSize? MonsterSize { get; set; }

        public virtual ICollection<Language>? Languages { get; set; }

        public virtual ICollection<MonsterSense>? Senses { get; set; }

        public virtual ICollection<DamageImmunitie>? DamageImmunities { get; set; }

        public virtual ICollection<DamageResistance>? DamageResistances { get; set; } 

        public virtual ICollection<DamageVulnerabilitie>? DamageVulnerabilities { get; set; }

        public virtual ICollection<MonsterAction>? MonsterActions { get; set; }

        public virtual ICollection<MultiAction>? MultiAction { get; set; }

        public virtual ICollection<ConditionImmunity>? ConditionImmunities { get; set; }

        public virtual ICollection<LegendaryAction>? LegendaryActions { get; set; }

        public virtual ICollection<MonsterProficiency>? Proficiencies { get; set; }

        public virtual ICollection<SpecialAbility>? SpecialAbilities { get; set; }


    }
}
