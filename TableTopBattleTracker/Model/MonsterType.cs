using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monster_types")]
    public class MonsterType
    {
        public int MonsterTypeId { get; set; }

        public string? Name { get; set; }

        public override string ToString()
        {
            return Name ?? nameof(MonsterType);
        }
    }
}