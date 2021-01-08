using System;
using E5R.Architecture.Data.Abstractions;

namespace UsingDataEntityFrameworkCore.Models
{
    public enum Grade
    {
        A, B, C, D, E, F
    }

    public class Enrollment : IDataModel
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }

        public object[] IdentifierValues => new[] { (object)EnrollmentID };
    }
}
