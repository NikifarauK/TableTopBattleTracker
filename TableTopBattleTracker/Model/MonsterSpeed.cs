using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monster_speed")]
    public class MonsterSpeed
    {
        public int MonsterSpeedId { get; set; }

        public int CharacterId { get; set; }
        public ESpeedType SpeedTypeId { get; set; }

        public SpeedType? SpeedType { get; set; }

        public int Value { get; set; }

    }
}