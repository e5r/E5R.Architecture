using E5R.Architecture.Core;

namespace UsingDataEntityFrameworkCore.Models
{
    public enum Grade
    {
        A, B, C, D, E, F
    }

    public class Enrollment : IIdentifiable
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }

        public object[] Identifiers => new[] { (object)EnrollmentID };
    }
}
