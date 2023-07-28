using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2_Linq.Models
{
    public class Teachers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherID { get; set; }

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        [DisplayName("Teacher")]
        public string TeacherFullname => $"{FirstName} {LastName}";

        public virtual ICollection<InfoConnection>? InfoConnections { get; set; } //navi
    }
}
