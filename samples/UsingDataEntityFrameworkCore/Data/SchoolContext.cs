using E5R.Architecture.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UsingDataEntityFrameworkCore.Models;

namespace UsingDataEntityFrameworkCore.Data
{
    public class SchoolContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public SchoolContext(ILoggerFactory loggerFactory, DbContextOptions<SchoolContext> options)
        : base(options)
        {
            Checker.NotNullArgument(loggerFactory, nameof(loggerFactory));

            _loggerFactory = loggerFactory;
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTest> CoursesTest { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.EnableDetailedErrors(true);
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("course");
            modelBuilder.Entity<Enrollment>().ToTable("enrollment");
            modelBuilder.Entity<Student>().ToTable("student");
            modelBuilder.Entity<Log>().ToTable("log");

            modelBuilder.Entity<CourseTest>().ToTable("course_test")
                .HasKey(pk => new { pk.CourseID, pk.CourseGUID });
        }
    }
}
