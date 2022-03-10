using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("spells")]
    public class Spell
    {
        [Key]
        public int SpellId { get; set; }

        [MaxLength(96), Required]
        public string? Name { get; set; }

        public ESpellLelel Level { get; set; }

        public string? Desc { get; set; }

        public virtual ICollection<CastingComponent>? CastingComponents { get; set; }

        [MaxLength(96)]
        public string? Materials { get; set; }

        [ForeignKey(nameof(CastRange))]
        public int CastRangeId { get; set; }
        public virtual CastRange? CastRange { get; set; }

        [ForeignKey(nameof(CastTime))]
        public int CastTimeId { get; set; }
        public virtual CastTime? CastTime { get; set; }

        public bool IsRitual { get; set; }

        public bool IsConcetration { get; set; }

        [MaxLength(64)]
        public string? Duration { get; set; }

        public ECharacteristic? DC { get; set; }

        public virtual ICollection<SpellDamage>? SpellDamage { get; set; }

        public EAreaType? AreaOfEffectId{ get; set; }
        public virtual AreaOfEffect? AreaOfEffect { get; set; }

        public int? AreasSize { get; set; }

        [ForeignKey(nameof(SpellSchool))]
        public int SpellSchoolId { get; set; }
        public virtual SpellSchool? SpellSchool { get; set; }

    }
}
