using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("spell_school")]
    public class SpellSchool
    {
        [Key]
        public int SpellSchoolId { get; set; }

        [Required]
        public string? Name { get; set; }

    }
}