using Microsoft.EntityFrameworkCore;
using RMS.Models;

namespace RMS.DatabaseContext
{
    public class AccountContext:DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> opt):base(opt)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<FeeDetails> feeDetails { get; set; }
    }
}
