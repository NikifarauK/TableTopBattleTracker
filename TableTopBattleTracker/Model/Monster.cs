using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monsters")]
    public class Monster
    {
        [Key]
        [Column("Id")]
        public int Index { get; set; }

        [Required]
        public string? Name { get; set; }

        
        public string? Url { get; set; }

    }
}
