using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monster_action")]
    public class MonsterAction
    {
        [Key, Column(Order = 0)]
        public int CharacterId { get; set; }

        [Key, Column(Order = 1)]
        public int ActionId { get; set; }
        public virtual Action? Action { get; set; }

        public string? DamageDice { get; set; }

        public int? DifficultyClass { get; set; }

        public int BonusToHit { get; set; }

        [NotMapped]
        public string? Name { get => Action?.Name; }
    }
}