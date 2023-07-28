using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2_Linq.Models
{
    public class Classes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassesID { get; set; }
        public string? ClassName { get; set; }

        public virtual ICollection<InfoConnection>? InfoConnections { get; set; } //navi
    }
}
