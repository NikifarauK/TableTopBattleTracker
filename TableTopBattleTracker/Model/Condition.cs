using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    public enum ECondition : byte
    {
        Blinded = 1,
        Charmed,
        Deafened,
        Frightened,
        Grappled,
        Incapacitated,
        Invisible,
        Paralyzed,
        Petrified,
        Poisoned,
        Prone,
        Restrained,
        Stunned,
        Unconcious,
        Exhaustion_1,
        Exhaustion_2,
        Exhaustion_3,
        Exhaustion_4,
        Exhaustion_5,
        Exhaustion_6,
    }


    [Table("condition")]
    public class Condition
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ECondition ConditionId { get; set; }

        [StringLength(64)]
        public string? Name { get; set; }

        public string? Desc { get; set; }


        public static List<string> Names { get; } = new()
        {
            "Ослеплен",
            "Очарован",
            "Оглох",
            "Испуган",
            "Схвачен",
            "Недееспособен",
            "Невидим",
            "Парализован",
            "Окаменевший",
            "Отравлен",
            "Ничком",
            "Опутан",
            "Оглушен",
            "Безсознания",
            "Истощен x1",
            "Истощен x2",
            "Истощен x3",
            "Истощен x4",
            "Истощен x5",
            "Истощен x6",
        };

        public static string? GetNameById(ECondition eCondition)
            => Names[(int)eCondition - 1];
    }
}