using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monster_proficiency")]
    public class MonsterProficiency
    {
        [Key, Column(Order = 0)]
        public int CharacterId { get; set; }

        [Key, Column(Order = 1)]
        public int ProficiencyId { get; set; }

        public virtual Proficiency? Proficiency { get; set; }

        public int Modifier { get; set; }
    }
}
