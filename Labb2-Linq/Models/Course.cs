using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2_Linq.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }

        public string CourseName { get; set; }



        public string Description { get; set; }

        public virtual ICollection<InfoConnection>? InfoConnections { get; set; } //navi
    }
}
