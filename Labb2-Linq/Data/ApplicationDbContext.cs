using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Labb2_Linq.Models;

namespace Labb2_Linq.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Labb2_Linq.Models.Course> Course { get; set; } = default!;
        public DbSet<Labb2_Linq.Models.InfoConnection> InfoConnection { get; set; } = default!;
        public DbSet<Labb2_Linq.Models.Classes> Classes { get; set; } = default!;
        public DbSet<Labb2_Linq.Models.Student> Student { get; set; } = default!;
        public DbSet<Labb2_Linq.Models.Teachers> Teachers { get; set; } = default!;
    }
}