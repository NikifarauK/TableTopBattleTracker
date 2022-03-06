using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    public enum EIncreaseType
    {
        OnCharacterLevel = 1,
        OnSpellSlot,
    }


    [Table("spell_damages")]
    public class SpellDamage
    {
        [Key]
        public int SpellDamageId { get; set; }

        public EDamageType DamageTypeId { get; set; }
        public virtual DamageType? DamageType { get; set; }

        public EIncreaseType IncreaseType { get; set; }

        public virtual ICollection<SpellDamageValue>? SpellDamageValues { get; set; }
    }
}
