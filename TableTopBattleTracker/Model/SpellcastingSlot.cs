using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("spellcasting_slots")]
    public class SpellcastingSlot
    {
        [Key, Column(Order = 0)]
        public int SpellcastingId { get; set; }

        [Key, Column(Order = 1)]
        public ESpellLelel SlotId { get; set; }

        [ForeignKey(nameof(SlotId))]
        public virtual Slot? Slot { get; set; }

        public int Times { get; set; }
    }


}