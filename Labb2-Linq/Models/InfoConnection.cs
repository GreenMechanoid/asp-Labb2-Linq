using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Labb2_Linq.Models
{
    public class InfoConnection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConnectionID { get; set; }

        [ForeignKey("students")]
        public int FK_StudentID { get; set; }
        public virtual Student? students { get; set; } //navi

        [ForeignKey("teachers")]
        public int FK_teachersID { get; set; }
        public virtual Teachers? teachers { get; set; } //navi

        [ForeignKey("classes")]
        public int FK_classesID { get; set; }
        public virtual Classes? classes { get; set; } //navi

        [ForeignKey("courses")]
        public int FK_CourseID { get; set; }
        public virtual Course? courses { get; set; } //navi
    } 
}
