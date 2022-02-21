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
    }
    
    [Table("damage_types")]
    public class DamageType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public EDamageType DamageTypeId { get; set; }

        [StringLength(64)]
        public string? Name { get; set; } = null;


        
        private static readonly List<string> _rus = new List<string>()
        {
            "Рубящий",
            "Дробящий",
            "Колющий",
            "Силой",
            "Огнем",
            "Холодом",
            "Молния",
            "Звуком",
            "Ядом",
            "Кислотой",
            "Психический",
            "Некротический",
            "Энергией",
        };

        public static string GetRusString(EDamageType damageType)
            => _rus[(int)damageType];

        public static EDamageType? GetDamageType(string damageTypeString)
            => (EDamageType?)(_rus.IndexOf(damageTypeString) + 1);

    }
}
