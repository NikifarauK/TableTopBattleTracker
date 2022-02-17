using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monsters")]
    public class Monster
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        public string? Url { get; set; }

    }
}
