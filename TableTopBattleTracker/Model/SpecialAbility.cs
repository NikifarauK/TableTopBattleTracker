using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("special_ability")]
    public class SpecialAbility
    {
        public int SpecialAbilityId { get; set; }

        [Required, StringLength(64)]
        public string? Name { get; set; }

        public string? Desc { get; set; }

        //public int? Value { get; set; }

        [ForeignKey(nameof(Usage))]
        public int? UsageId { get; set; }
        public virtual Usage? Usage { get; set; }
                
        [ForeignKey(nameof(Spellcasting))]
        public int? SpellcastingId { get; set; }
        public virtual Spellcasting? Spellcasting { get; set; }
    }
}