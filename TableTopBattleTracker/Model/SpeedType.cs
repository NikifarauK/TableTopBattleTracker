using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    public enum ESpeedType : byte
    {
        Walk = 1,
        Fly,
        Swim,
        Climb,
        Burrow,
    }

    [Table("speed_type")]
    public class SpeedType
{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ESpeedType SpeedTypeId { get; set; }

        [StringLength(64)]
        public string? Name { get; set; }

        public static List<string> Names { get; } = new List<string>
        {
            "Обычная",
            "Летая",
            "Плавая",
            "Лазая",
            "Копая",
        };

        public static string GetNameById(ESpeedType eSpeedType)
            => Names[(int)eSpeedType - 1];

        public override string ToString()
        {
            return Name ?? nameof(SpeedType);
        }
    }
}
