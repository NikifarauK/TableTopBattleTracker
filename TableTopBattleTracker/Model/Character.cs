using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("characters")]
    public class Character
    {
        [Key]
        public int CharacterId { get; set; }

        [StringLength(128)]
        public string? Name { get; set; }

        public string? Url { get; set; }

        public int ArmorClass { get; set; }

        [StringLength(128)]
        public string? Allignment { get; set; }

        public float ChallengeRating { get; set; }

        [StringLength(16)]
        public string? HitDice { get; set; }

        public int HitPoints { get; set; }

        public int Charisma { get; set; }

        public int Constitution { get; set; }

        public int Dexterity { get; set; }

        public int Intelligence { get; set; }

        public int Strength { get; set; }

        public int Wisdom { get; set; }

        public int Experience { get; set; }

        [NotMapped]
        public int Initiative
        {
            get => (Dexterity - 10) / 2;
        }

        public MonsterType? MonsterType { get; set; }
        public ICollection<MonsterSpeed>? Speeds { get; set; }

        public EMonsterSize MonsterSizeId { get; set; }
        public MonsterSize? MonsterSize { get; set; }

        public ICollection<Language>? Languages { get; set; }

        public ICollection<Sense>? Senses { get; set; }

        public ICollection<DamageImmunitie>? DamageImmunities { get; set; }

        public ICollection<DamageResistance>? DamageResistances { get; set; } 

        public ICollection<DamageVulnerabilitie>? DamageVulnerabilities { get; set; }

        public ICollection<MonsterAction>? MonsterActions { get; set; }

        public ICollection<MultiAction>? MultiAction { get; set; }

        public ICollection<Condition>? ConditionImmunity { get; set; }

        public ICollection<LegendaryAction>? LegendaryActions { get; set; }


    }
}
