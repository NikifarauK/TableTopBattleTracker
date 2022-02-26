using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monster_senses")]
    public class MonsterSense
    {
        [Key, Column(Order =1)]
        public int CharacterId { get; set; }

        [Key, Column(Order =2)]
        public int SenseId{ get; set; }

        public Sense? Sense { get; set; }

        public int Distance { get; set; }

    }
}
