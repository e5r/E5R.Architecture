using E5R.Architecture.Data.Abstractions;

namespace UsingDataEntityFrameworkCore.Models
{
    public class CourseTest : IDataModel
    {
        public int CourseID { get; set; }
        public string CourseGUID { get; set; }
        public string Title { get; set; }
        public object[] IdentifierValues => new[] { (object)CourseID, (object)CourseGUID };
    }
}
