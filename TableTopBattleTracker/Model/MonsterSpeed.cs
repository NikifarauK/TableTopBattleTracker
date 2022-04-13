using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monster_speed")]
    public class MonsterSpeed
    {
        [Key, Column(Order = 0)]
        public int CharacterId { get; set; }
        
        [Key, Column(Order = 1)]
        public ESpeedType SpeedTypeId { get; set; }

        public virtual SpeedType? SpeedType { get; set; }

        public int Value { get; set; }

        public override string ToString()
        {
            return $"{SpeedType?.Name ?? nameof(MonsterSpeed)}: {Value}";
        }
    }
}