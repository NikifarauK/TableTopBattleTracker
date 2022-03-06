using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("cast_time")]
    public class CastTime
    {
        [Key]
        public int CastTimeId { get; set; }

        [Required, MaxLength(64)]
        public string? Name { get; set; }
    }
}