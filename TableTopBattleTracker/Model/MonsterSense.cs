using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monster_senses")]
    public class MonsterSense
    {
        [Key, Column(Order = 0)]
        public int CharacterId { get; set; }

        [Key, Column(Order = 1)]
        public int SenseId{ get; set; }

        public virtual Sense? Sense { get; set; }

        public int Distance { get; set; }

        public override string ToString()
        {
            return $"{Sense?.Name ?? nameof(MonsterSense)}: {Distance}";
        }

    }
}
