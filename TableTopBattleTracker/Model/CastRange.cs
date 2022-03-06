using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("cast_range")]
    public class CastRange
    {
        [Key]
        public int CastRangeId { get; set; }

        [Required, MaxLength(64)]
        public string? Name { get; set; }
    }
}