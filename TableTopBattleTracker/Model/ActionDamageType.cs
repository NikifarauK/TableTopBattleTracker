using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("action_damage_types")]
    public class ActionDamageType
    {
        [Key, Column(Order = 0)]
        public int ActionId { get; set; }

        [Key, Column(Order = 1)]
        public EDamageType DamagetypeId { get; set; }

        public virtual Action? Action { get; set; }

        public virtual DamageType? DamageType { get; set; }
    }
}
