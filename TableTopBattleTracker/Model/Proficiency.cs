using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("proficiencies")]
    public class Proficiency
    {
        public int ProficiencyId { get; set; }

        [Required, StringLength(64)]
        public string? Name { get; set; }

        public static string NamePrefics(char type)
            => type switch
            {
                'С' => "Спасбросок-",
                'Н' => "Навык-",
                _ => throw new ArgumentException("Wrong name prefics char"),
            };

        public override string ToString()
        {
            return Name ?? nameof(Proficiency);
        }
    }
}