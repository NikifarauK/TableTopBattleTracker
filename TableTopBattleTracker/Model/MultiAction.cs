using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("multiattack")]
    public class MultiAction
    {
        [Key, Column(Order = 0)]
        public int CharacterId { get; set; }

        [Key, Column(Order = 1)]
        public int MonsterActionId { get; set; }
        public virtual MonsterAction? MonsterAction { get; set; }

        public int Count { get; set; }

        [NotMapped]
        public string? Name { get => MonsterAction?.Name; }
    }
}