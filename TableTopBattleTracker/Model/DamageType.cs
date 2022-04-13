using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    public enum EDamageType : byte
    {
        Slashing = 1,
        Bludgeoning,
        Piercing,
        Force,
        Fire,
        Cold,
        Ligthning,
        Thunder,
        Poison,
        Acid,
        Psichic,
        Necrotic,
        Radiant,
        Healing,
    }
    
    [Table("damage_types")]
    public class DamageType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public EDamageType DamageTypeId { get; set; }

        [StringLength(64)]
        public string? Name { get; set; } = null;

        public virtual ICollection<Action>? Actions { get; set; }


        public static List<string> Names { get; } = new()
        {
            "Рубящий",
            "Дробящий",
            "Колющий",
            "Силовым полем",
            "Огнем",
            "Холодом",
            "Молния",
            "Звуком",
            "Ядом",
            "Кислотой",
            "Психический",
            "Некротический",
            "Излучением",
            "Лечение",
        };

        public static string GetNameById(EDamageType damageType)
            => Names[(int)damageType - 1];

        public static EDamageType? GetDamageType(string damageTypeString)
            => (EDamageType?)(Names.IndexOf(damageTypeString) + 1);

        public override string ToString()
        {
            return Name ?? nameof(DamageType);
        }
    }
}
