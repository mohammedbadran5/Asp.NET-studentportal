using Microsoft.EntityFrameworkCore;
using studentportal.Models.Entities;

namespace studentportal.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)//save it in you mind
        {
            
        }
        public DbSet<Student> Students { get; set; }
    }
}
