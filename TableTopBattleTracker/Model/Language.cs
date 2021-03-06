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

        public override string ToString()
        {
            return Name ?? nameof(Language);
        }
    }
}
