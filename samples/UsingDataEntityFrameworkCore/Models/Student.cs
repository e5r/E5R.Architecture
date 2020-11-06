using System;
using System.Collections.Generic;
using E5R.Architecture.Data.Abstractions;

namespace UsingDataEntityFrameworkCore.Models
{
    public class Student : IDataModel
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public object[] IdentifierValues => new[] { (object)ID };
    }

    //public class StudentDataModel : DataModel<Student>
    //{
    //    public override object[] IdentifierValues
    //    {
    //        get
    //        {
    //            Int32 id = Business.ID;

    //            return new object[] { id };
    //        }
    //    }

    //    public int ID { get => Business.ID; set => Business.ID = value; }
    //    public string LastName { get => Business.LastName; set => Business.LastName = value; }
    //    public string FirstMidName { get => Business.FirstMidName; set => Business.FirstMidName = value; }
    //    public DateTime EnrollmentDate { get => Business.EnrollmentDate; set => Business.EnrollmentDate = value; }
    //    public ICollection<Enrollment> Enrollments { get => Business.Enrollments; set => Business.Enrollments = value; }
    //}
}
