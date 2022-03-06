using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("area_of_effect")]
    public class AreaOfEffect
    {
        [Key]
        public int AreaOfEffectId { get; set; }

        [Required, MaxLength(64)]
        public string? Type { get; set; }
    }
}