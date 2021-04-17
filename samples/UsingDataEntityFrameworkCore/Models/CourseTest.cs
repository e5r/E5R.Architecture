using E5R.Architecture.Core;

namespace UsingDataEntityFrameworkCore.Models
{
    public class CourseTest : IIdentifiable
    {
        public int CourseID { get; set; }
        public string CourseGUID { get; set; }
        public string Title { get; set; }
        public object[] Identifiers => new[] { (object)CourseID, (object)CourseGUID };
    }
}
