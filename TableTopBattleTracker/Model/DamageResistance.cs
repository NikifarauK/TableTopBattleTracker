using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("damage_resistances")]
    public class DamageResistance
    {
        [Key, Column(Order = 0)]
        public int CharacterId { get; set; }

        [Key, Column(Order = 1)]
        public EDamageType DamageTypeId{ get; set; }

        public virtual DamageType? DamageType { get; set; }
    }
}