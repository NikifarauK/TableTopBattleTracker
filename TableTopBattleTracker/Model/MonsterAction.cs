using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monster_action")]
    public class MonsterAction
    {
        public int MonsterActionId { get; set; }

        public int CharacterId { get; set; }

        public Attack? Attack { get; set; }

        public string? DamageDice { get; set; }

        public int? DC { get; set; }

        public int BonusToHit { get; set; }

        [NotMapped]
        public string? Name { get => Attack?.Name; }
    }
}