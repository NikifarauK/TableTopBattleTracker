using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    public enum ECharacteristic : byte
    {
        Charisma = 1,
        Constitution,
        Dexterety,
        Intelligence,
        Strength,
        Wisdom
    }
    [Table("characteristics")]
    public class Characteristic
    {
        public ECharacteristic CharacteristicId { get; set; }

        public string? Name { get; set; }

        [NotMapped]
        public static List<string> Names { get; } = new()
        {
            "Харизма",
            "Телосложение",
            "Ловкость",
            "Интеллект",
            "Сила",
            "Мудрость",
        };

        public static string? NameById(ECharacteristic eCharacteristics)
            => Names[(int)eCharacteristics - 1];
    }
}
