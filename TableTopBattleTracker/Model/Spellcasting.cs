using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("spellcasting")]
    public class Spellcasting
    {
        [Key]
        public int SpellcastingId { get; set; }

        [ForeignKey(nameof(Ability))]
        public ECharacteristic CharacteristicId { get; set; }
        public virtual Characteristic? Ability { get; set; }

        public virtual ICollection<SpellcastingSlot>? Slots { get; set; }

        public int? DifficultyClass { get; set; }

        public int Level { get; set; }

        public int Modifier { get; set; }

        public virtual ICollection<SpellcastingSpell>? Spells { get; set; }
    }
}