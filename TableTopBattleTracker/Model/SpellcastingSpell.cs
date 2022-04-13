using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("spellcasting_spells")]
    public class SpellcastingSpell
    {
        [Key, Column(Order = 0)]
        public int SpellId { get; set; }

        [Key, Column(Order = 1)]
        public int SpellcastingId { get; set; }

        [ForeignKey(nameof(SpellId))]
        public virtual Spell? Spell { get; set; }

        [Required]
        public int UsageId { get; set; }
        public virtual Usage? Usage { get; set; }
    }
}
