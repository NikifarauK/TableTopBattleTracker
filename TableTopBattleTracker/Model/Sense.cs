using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("senses")]
    public class Sense
    {
        public int SenseId { get; set; }

        [StringLength(64)]
        public string? Name { get; set; }

        public string? Desc { get; set; }

        public override string ToString()
        {
            return Name ?? nameof(Sense);
        }
    }
}