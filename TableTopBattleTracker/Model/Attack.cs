using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("attack")]
    public class Attack
    {
        public int AttackId { get; set; }

        [StringLength(64)]
        public string? Name { get; set; }

        public DamageType? DamageType { get; set; }

        public int Distance { get; set; }

        public int Reach { get; set; }
        public ECharacteristics? DCType { get; set; }

        public string? Desc { get; set; }

        /*In Desc
        Damage value -> %dv%
        DC           -> %dc%
        to use moster-specific values in MonsterAction
        */
    }
}
