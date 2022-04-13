using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("usage")]
    public class Usage
    {
        public int UsageId { get; set; }

        [StringLength(64)]
        public string? Type { get; set; }

        public int? Times { get; set; }

        public override string ToString()
        {
            return $"{Times}{Type}";
        }
    }
}