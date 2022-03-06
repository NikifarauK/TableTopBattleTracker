using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("action_dc_types")]
    public class ActionDCType
    {
        [Key, Column(Order = 0)]
        public int ActionId { get; set; }

        [Key, Column(Order = 1)]
        public ECharacteristic CharacteristicId { get; set; }

        public virtual Action? Action { get; set; }
        public virtual Characteristic? Characteristic { get; set; }

    }
}
