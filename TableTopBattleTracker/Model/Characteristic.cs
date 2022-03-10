using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    public enum ECharacteristic : byte
    {
        Cha= 1,
        Con,
        Dex,
        Int,
        Str,
        Wis
    }

    [Table("characteristics")]
    public class Characteristic
    {
        public ECharacteristic CharacteristicId { get; set; }

        [Required, MaxLength(12)]
        public string? Name { get; set; }

        public string? Desc { get; set; }

        public ICollection<Action>? Actions { get; set; }

        [NotMapped]
        public static List<string> Names { get; } = new()
        {
            "Хар",
            "Тел",
            "Лов",
            "Инт",
            "Сил",
            "Муд",
        };

        public static string GetNameById(ECharacteristic eCharacteristics)
            => Names[(int)eCharacteristics - 1];
    }
}
