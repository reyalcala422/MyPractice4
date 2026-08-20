using Microsoft.EntityFrameworkCore;
using MyPractice4.Model;

namespace MyPractice4.Data
{
    public class APIDbContext:DbContext
    {

        public APIDbContext(DbContextOptions<APIDbContext> options)
                 : base(options)
        {
        }


        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();
        }
    }
}
