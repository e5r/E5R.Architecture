using System;
using System.Collections.Generic;
using E5R.Architecture.Data.Abstractions;

namespace UsingDataEntityFrameworkCore.Models
{
    public class Course : IDataModel
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public object[] IdentifierValues => new[] { (object)CourseID };
    }
}
