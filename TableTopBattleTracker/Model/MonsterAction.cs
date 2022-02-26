using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monster_action")]
    public class MonsterAction
    {
        [Key, Column(Order =1)]
        public int CharacterId { get; set; }

        [Key, Column(Order =2)]
        public int AttackId { get; set; }
        public Attack? Attack { get; set; }

        public string? DamageDice { get; set; }

        public int? DC { get; set; }

        public int BonusToHit { get; set; }

        [NotMapped]
        public string? Name { get => Attack?.Name; }
    }
}