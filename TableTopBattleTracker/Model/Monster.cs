using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monsters")]
    public class Monster
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        public string? Name { get; set; }

        
        public string? Url { get; set; }

    }
}
