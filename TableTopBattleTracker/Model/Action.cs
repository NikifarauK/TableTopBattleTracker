using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("action")]
    public class Action
    {
        [Key]
        public int ActionId { get; set; }

        [MaxLength(64)]
        public string? Name { get; set; }

        public virtual ICollection<DamageType>? DamageType { get; set; }

        public int Distance { get; set; }

        public int Reach { get; set; }

        public virtual ICollection<Characteristic>? DCTypes { get; set; }

        public string? Desc { get; set; }

        /*In Desc
        Damage value -> %dv%
        DC           -> %dc%
        to use moster-specific values in MonsterAction
        */
    }
}
