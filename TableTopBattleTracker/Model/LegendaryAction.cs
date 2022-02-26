using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("legendary_actions")]
    public class LegendaryAction
    {
        [Key, Column(Order = 1)]
        public int CharacterId { get; set; }

        [Key, Column(Order = 2)]
        public int AttackId { get; set; }

        public Attack? Attack { get; set; }

        public int Cost { get; set; } = 1;

        public string? DamageDice { get; set; }

        public int? DC { get; set; }

        public int BonusToHit { get; set; }

        [NotMapped]
        public string? Name { get => Attack?.Name; }

        public static string? Desc { get
                => "может совершить 3 легендарных действия, выбирая из представленных ниже вариантов. За один раз можно использовать только одно легендарное действие, и только в конце хода другого существа. Восстанавливает использованные легендарные действия в начале своего хода";
        }
    }
}