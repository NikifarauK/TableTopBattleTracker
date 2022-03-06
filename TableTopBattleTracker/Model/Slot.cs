using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    public enum ESpellLelel : byte
    {
        Cantrip,
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh,
        Eighth,
        Ninth,
    }

    [Table("slots")]
    public class Slot
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ESpellLelel SlotId { get; set; }

        public string? Name { get; set; }

        [NotMapped]
        public static List<string> Names { get; }
                = new()
                {
                    "Заговор",
                    "1",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7",
                    "8",
                    "9",
                };

        public static string? GetNameById(ESpellLelel eSpellLelel)
            => Names[(int)eSpellLelel];
    }
}
