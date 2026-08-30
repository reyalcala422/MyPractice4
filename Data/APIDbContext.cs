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
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<UserPlaces> UserPlaces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

            modelBuilder.Entity<UserPlaces>()
            .HasKey(x => new
            {
                x.UserId,
                x.PlaceId
            });


            modelBuilder.Entity<TaskItem>()
            .HasOne(x => x.User)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);


            // User -> UserPlaces
            modelBuilder.Entity<UserPlaces>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserPlaces)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Place -> UserPlaces
            modelBuilder.Entity<UserPlaces>()
                .HasOne(x => x.Place)
                .WithMany(x => x.UserPlaces)
                .HasForeignKey(x => x.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
