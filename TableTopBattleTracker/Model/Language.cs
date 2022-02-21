using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("languages")]
    public class Language
    {
        public int LangugeId { get; set; }
        
        [Required]
        public string? Name { get; set; }
    }
}
