using Microsoft.EntityFrameworkCore;
using MEDIQUICK.Models;

namespace MEDIQUICK.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<VerificationCode> VerificationCodes { get; set; }
    }
}
