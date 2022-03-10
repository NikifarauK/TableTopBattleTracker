using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    public enum EAreaType : byte
    {
        Cilinder = 1,
        Cone,
        Cube,
        Line,
        Sphere,
    }


    [Table("area_of_effect")]
    public class AreaOfEffect
    {
        [Key]
        public EAreaType AreaOfEffectId { get; set; }

        [Required, MaxLength(64)]
        public string? Name { get; set; }

        public static List<string> Names { get; } = new List<string>()
        {
            "Цилиндр",
            "Конус",
            "Куб",
            "Линия",
            "Сфера радиусом",
        };

        public static string GetNameById(EAreaType eAreaType)
            => Names[(int)eAreaType - 1];
    }
}