using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("languages")]
    public class Language
    {
        public int LanguageId { get; set; }
        
        [Required]
        public string? Name { get; set; }

        public ICollection<Character>? Characters { get; set; }
    }
}
