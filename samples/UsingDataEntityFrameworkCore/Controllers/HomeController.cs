using System.Linq;
using System.Diagnostics;
using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.Abstractions.Alias;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UsingDataEntityFrameworkCore.Models;

namespace UsingDataEntityFrameworkCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreReader<Student> _studentStore;
        private readonly IStoreReader<Enrollment> _enrollmentStore;

        public HomeController(ILogger<HomeController> logger,
                              IStoreReader<Student> studentStore,
                              IStoreReader<Enrollment> enrollmentStore)
        {
            _logger = logger;
            _studentStore = studentStore;
            _enrollmentStore = enrollmentStore;
        }

        public IActionResult Index()
        {
            // Equivalentes para Find()
            var a1_0 = _studentStore.Find(2, new LinqDataProjection<Student>());
            var a1_1 = _studentStore.Find(2, null);
            var a1 = _studentStore.Find(2);
            var a2 = _studentStore.QueryBuilder().Find(2);

            // Equivalentes para Find() com Include()
            var b1 = _enrollmentStore.QueryBuilder()
                .Projection()
                    .Include(i => i.Student)
                    .Include(i => i.Course)
                    .Project()
                .Find(2);

            var b2 = _enrollmentStore.QueryBuilder()
                .Projection()
                    .Include(i => i.Student)
                    .Include(i => i.Course)
                    .Map(m => new
                    {
                        Student = m.Student.FirstMidName,
                        Course = m.Course.Title
                    })
                    .Project()
                .Find(2);

            // Equivalentes para Find() com Include() e ThenInclude()
            var c1 = _studentStore.QueryBuilder()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Project()
                .Find(2);

            var c2 = _studentStore.QueryBuilder()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Map(m => new
                    {
                        StudentName = m.FirstMidName,
                        TotalEnrollments = m.Enrollments.Count,
                        Enrollments = m.Enrollments.Select(s => new
                        {
                            Grade = s.Grade,
                            Course = s.Course.Title
                        })
                    })
                    .Project()
                .Find(2);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
