using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("damage_vulnerabilities")]
    public class DamageVulnerabilitie
    {
        [Key, Column(Order = 0)]
        public int CharacterId { get; set; }

        [Key, Column(Order = 1)]
        public EDamageType DamageTypeId { get; set; }

        public virtual DamageType? DamageType { get; set; }
    }
}