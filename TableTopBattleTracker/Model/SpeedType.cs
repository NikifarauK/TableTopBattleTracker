using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    public enum ESpeedType : byte
    {
        Walk,
        Fly,
        Swim,
        Climb
    }

    [Table("speed_type")]
    public class SpeedType
{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ESpeedType SpeedTypeId { get; set; }

        [StringLength(64)]
        public string? Name { get; set; }

        private static List<string> _names { get; } = new List<string>
        {
            "Обычная",
            "Летая",
            "Плавая",
            "Лазая",
        };

        public static string NameById(ESpeedType eSpeedType)
            => _names[(int)eSpeedType];
    }
}
