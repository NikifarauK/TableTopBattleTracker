using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    public enum ECastingComponent : byte
    {
        V =1,
        S,
        M,
    }

    [Table("casting_components")]
    public class CastingComponent
    {
        [Key]
        public ECastingComponent CastingComponentId { get; set; }

        [Required, MaxLength(64)]
        public string? Name { get; set; }

        public ICollection<Spell>? Spells { get; set; }
        public static List<string> Names { get; } = new()
        {
            "Вербальный",
            "Соматический",
            "Материальный",
        };

        public static string GetNameById(ECastingComponent eCastingComponent)
            => Names[(int)eCastingComponent - 1];
    }
}