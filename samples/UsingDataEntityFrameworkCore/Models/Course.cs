using System.Collections.Generic;
using E5R.Architecture.Core;

namespace UsingDataEntityFrameworkCore.Models
{
    public class Course : IIdentifiable
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public object[] Identifiers => new[] { (object)CourseID };
    }
}
