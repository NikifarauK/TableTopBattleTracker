using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("condition_immunities")]
    public class ConditionImmunity
    {
        [Key, Column(Order = 0)]
        public int CharacterId { get; set; }

        [Key, Column(Order = 1)]
        public ECondition ConditionId { get; set; }

        public virtual Condition? Condition { get; set; }

        [NotMapped]
        public string? Name { get => Condition?.Name; }

    }
}
