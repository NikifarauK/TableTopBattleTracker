using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("allignment")]
    public class Allignment
    {
        public int AllignmentId { get; set; }

        [Required]
        public string? Name { get; set; }

        public override string ToString()
        {
            return Name ?? nameof(Allignment);
        }
    }
}