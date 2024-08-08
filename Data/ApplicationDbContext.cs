using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online__Smart_Learning_System.Models;

namespace Online__Smart_Learning_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Course> Course { get; set; }
        public DbSet<Users> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<Users>().HasData(
                new Users { UserId = 1, UserName = "Antor Sarker", Email = "antor@example.com", Password = "password1" },
                new Users { UserId = 2, UserName = "Arpit Vai", Email = "instructor2@example.com", Password = "password2" }
                );
            modelBuilder.Entity<IdentityRole>().HasData(
                 new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                 new IdentityRole { Id = "2", Name = "Teacher", NormalizedName = "TEACHER" },
                 new IdentityRole { Id = "3", Name = "Student", NormalizedName = "STUDENT" }
             );
            modelBuilder.Entity<IdentityError>().HasNoKey();
            
        }
    }
}
