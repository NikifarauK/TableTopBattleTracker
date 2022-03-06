using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    public enum EMonsterSize : byte
    {
        Tine = 1,
        Small,
        Medium,
        Large,
        Huge,
        Gargantuan,
    }

    [Table("monster_size")]
    public class MonsterSize
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public EMonsterSize MonsterSizeId { get; set; }

        [Required]
        [StringLength(64)]
        public string? Name { get; set; }

        [Required]
        public float SpaceModifier { get; set; }

        public static List<string> Names { get; } = new()
        {
            "Крохотный",
            "Маленький",
            "Средний",
            "Большой",
            "Огромный",
            "Гигантский"
        };

        private static List<float> Modifiers { get; } = new()
        {
            0.5f,
            1.0f,
            1.0f,
            2.0f,
            3.0f,
            4.0f,
        };

        public static (string, float) ParamsById(EMonsterSize eMonsterSize)
            => (Names[(int)eMonsterSize - 1], Modifiers[(int)eMonsterSize - 1]);

        public static MonsterSize GetDefault()
        {
            var id = EMonsterSize.Medium;
            var (name, spaceModifier) = MonsterSize.ParamsById(id);
            return new MonsterSize()
            {
                MonsterSizeId = id,
                Name = name,
                SpaceModifier = spaceModifier,
            };
        }
    }
}
