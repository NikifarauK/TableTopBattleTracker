using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("damage_vulnerabilities")]
    public class DamageVulnerabilitie
    {
        [Key, Column(Order =1)]
        public int CharacterId { get; set; }

        [Key, Column(Order =2)]
        public EDamageType DamageTypeId { get; set; }

        public DamageType? DamageType { get; set; }
    }
}