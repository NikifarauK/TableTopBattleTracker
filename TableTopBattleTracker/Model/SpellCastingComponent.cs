using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("spell_casting_components")]
    public class SpellCastingComponent
    {
        [Key, Column(Order = 0)]
        public int SpellId { get; set; }
        public virtual Spell? Spell { get; set; }

        [Key, Column(Order = 1)]
        public ECastingComponent CastingComponentId { get; set; }

        public virtual CastingComponent? CastingComponent { get; set; }
    }
}
