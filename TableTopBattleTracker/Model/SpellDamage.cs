using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    public enum EIncreaseType
    {
        None,
        OnCharacterLevel,
        OnSpellSlot,
    }


    [Table("spell_damages")]
    public class SpellDamage
    {        
        [Key, Column(Order = 0)]
        public int SpellId { get; set; }
        public Spell? Spell { get; set; }
        
        [Key, Column(Order = 1)]
        public EDamageType DamageTypeId { get; set; }
        public virtual DamageType? DamageType { get; set; }

        public EIncreaseType IncreaseType { get; set; }

        public virtual ICollection<SpellDamageValue>? SpellDamageValues { get; set; }
    }
}
