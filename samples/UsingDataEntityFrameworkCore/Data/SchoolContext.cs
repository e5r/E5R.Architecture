using UsingDataEntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace UsingDataEntityFrameworkCore.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("course");
            modelBuilder.Entity<Enrollment>().ToTable("enrollment");
            modelBuilder.Entity<Student>().ToTable("student");
        }
    }
}
