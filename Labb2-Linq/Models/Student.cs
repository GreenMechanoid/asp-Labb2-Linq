using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2_Linq.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DisplayName("Student")]
        public string StudentFullname => $"{FirstName} {LastName}";
        public virtual ICollection<InfoConnection>? InfoConnections { get; set; } //navi
    }
}
