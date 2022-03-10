using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("spell_damage_values")]
    public class SpellDamageValue
    {
        [Key, Column(Order = 0)]
        public int SpellId { get; set; }

        [Key, Column(Order = 1)]
        public EDamageType DamageTypeId { get; set; }

        [Key, Column(Order = 2)]
        public int Level { get; set; }

        [Required, MaxLength(8)]
        public string? Value { get; set; }
    }
}
