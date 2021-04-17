using System;
using System.Collections.Generic;
using E5R.Architecture.Core;

namespace UsingDataEntityFrameworkCore.Models
{
    public class Student : IIdentifiable
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public object[] Identifiers => new[] { (object)ID };
    }
}
