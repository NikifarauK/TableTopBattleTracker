using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monster_languges")]
    public class MonsterLanguage
    {
        [Key, Column(Order =1)]
        public int CharacterId { get; set; }

        [Key, Column(Order =2)]
        public int LanguageId { get; set; }
    }
}
