using System.Linq;
using System.Diagnostics;
using E5R.Architecture.Core;
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
        private readonly ILazy<IStoreReader<Student>> _studentStoreLoader;
        private readonly IStoreReader<Enrollment> _enrollmentStore;

        public HomeController(ILogger<HomeController> logger,
                              IStoreReader<Student> studentStore,
                              ILazy<IStoreReader<Student>> studentStoreLoader,
                              IStoreReader<Enrollment> enrollmentStore)
        {
            _logger = logger;
            _studentStore = studentStore;
            _studentStoreLoader = studentStoreLoader;
            _enrollmentStore = enrollmentStore;
        }

        public IActionResult Index()
        {
            var lazyStore = _studentStoreLoader.Value;
            
            // Equivalentes para GetAll() com GroupBy()
            var projection = new DataProjection<Student, int, object>(
                s => s.EnrollmentDate.Year,
                g => new
                {
                    Ano = g.Key,
                    Total = g.Count(),
                    MaiorData = g.Max(c => c.EnrollmentDate),
                    MenorData = g.Min(c => c.EnrollmentDate)
                });

            var result = lazyStore.GetAll(projection);
            var resultList = result.ToList();

            var result2 = lazyStore.AsFluentQuery()
                .Projection()
                    .GroupAndMap(s => s.EnrollmentDate.Year, g => new
                    {
                        Ano = g.Key,
                        Total = g.Count(),
                        MaiorData = g.Max(c => c.EnrollmentDate),
                        MenorData = g.Min(c => c.EnrollmentDate)
                    })
                    .Project()
                .GetAll();
            var resultList2 = result2.ToList();

            // Equivalentes para GetAll()
            var a1 = lazyStore.GetAll();
            var a2 = lazyStore.GetAll(new DataIncludes<Student>());
            var a3 = lazyStore.AsFluentQuery().GetAll();

            // Equivalentes para GetAll() com Include() e Select()
            var a4Projection = new DataProjection<Student, object>(s => new
            {
                StudentName = s.FirstMidName
            });
            var a4 = lazyStore.GetAll(a4Projection);
            var a5 = lazyStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Project()
                .GetAll();

            var a6 = lazyStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Map(m => new
                    {
                        StudentName = m.FirstMidName,
                        TotalEnrollments = m.Enrollments.Count
                    })
                    .Project()
                .GetAll();

            // Equivalentes para Find()
            var b1 = lazyStore.Find(2, new DataIncludes<Student>());
            var b2 = lazyStore.Find(2, null);
            var b3 = lazyStore.Find(2);
            var b4 = lazyStore.AsFluentQuery().Find(2);

            // Equivalentes para Find() com Include() e Select()
            var c1 = _enrollmentStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Student)
                    .Include(i => i.Course)
                    .Project()
                .Find(2);

            var c2 = _enrollmentStore.AsFluentQuery()
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
            var d1 = lazyStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Project()
                .Find(2);

            var d2 = lazyStore.AsFluentQuery()
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

            // Equivalentes para Search() com Include() e ThenInclude()
            var e1 = lazyStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Project()
                .Filter(w => w.FirstMidName.Contains("e"))
                .Search()
                .ToList();

            var e2 = lazyStore.AsFluentQuery()
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
                .Filter(w => w.FirstMidName.Contains("e"))
                .Search()
                .ToList();

            // Equivalentes para Search() com Include() e ThenInclude()
            var f1_a = lazyStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Project()
                .Sort(s => s.FirstMidName)
                .LimitedGet();

            var f2_a = lazyStore.AsFluentQuery()
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
                .SortDescending(s => s.FirstMidName)
                .LimitedGet();

            var f1_b = lazyStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Project()
                .OffsetBegin(3)
                .LimitedGet();

            var f2_b = lazyStore.AsFluentQuery()
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
                .OffsetLimit(3)
                .LimitedGet();

            var f1_c = lazyStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Project()
                .Paginate(1, 3)
                .LimitedGet();

            var f2_c = lazyStore.AsFluentQuery()
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
                .Paginate(2, 3)
                .LimitedGet();

            var g1_a = lazyStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Project()
                .Sort(s => s.LastName)
                .Filter(w => w.ID > 0)
                .LimitedSearch();

            var g1_b = lazyStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Project()
                .Filter(w => w.ID > 0)
                .Sort(s => s.LastName)
                .LimitedSearch();

            var g2_a = lazyStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Map(m => new
                    {
                        StudentName = $"{m.LastName}, {m.FirstMidName}",
                        TotalEnrollments = m.Enrollments.Count,
                        Enrollments = m.Enrollments.Select(s => new
                        {
                            Grade = s.Grade,
                            Course = s.Course.Title
                        })
                    })
                    .Project()
                .Sort(s => s.LastName)
                .Filter(w => w.ID > 0)
                .LimitedSearch();

            var g2_b = lazyStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Map(m => new
                    {
                        StudentName = $"{m.LastName}, {m.FirstMidName}",
                        TotalEnrollments = m.Enrollments.Count,
                        Enrollments = m.Enrollments.Select(s => new
                        {
                            Grade = s.Grade,
                            Course = s.Course.Title
                        })
                    })
                    .Project()
                .Filter(w => w.ID > 0)
                .Sort(s => s.LastName)
                .LimitedSearch();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
