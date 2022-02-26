using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("condition_immunities")]
    public class ConditionImmunitie
    {
        [Key, Column(Order =1)]
        public int CharacterId { get; set; }

        [Column(Order =2)]
        public ECondition ConditionId { get; set; }

        public Condition? Condition { get; set; }

        [NotMapped]
        public string? Name { get => Condition?.Name; }

        [NotMapped]
        public string? Desc { get => Condition?.Desc; }
    }
}
