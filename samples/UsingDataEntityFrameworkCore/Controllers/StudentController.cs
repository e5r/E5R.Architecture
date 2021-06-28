using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.Abstractions.Alias;
using E5R.Architecture.Data.EntityFrameworkCore.Alias;
using E5R.Architecture.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UsingDataEntityFrameworkCore.Data;
using UsingDataEntityFrameworkCore.Models;
using E5R.Architecture.Core;
using E5R.Architecture.Data;

namespace UsingDataEntityFrameworkCore.Controllers
{
    public class StudentController : Controller
    {
        const uint PAGE_SIZE = 5;

        private readonly SchoolContext _context;
        private readonly SchoolContext _context2;
        private readonly DbConnection _connection;
        private readonly DbTransaction _transaction;
        private readonly IStoreReader<Student> _readerStore;
        private readonly ICountableStorage<Student> _countableStorage;
        private readonly ICountableStorage<SchoolContext, Student> _countableStorageCountable2;
        private readonly IStorageTransportable<Student> _storageTransportable;
        private readonly IStorageTransportable<SchoolContext, Student> _storageTransportable2;
        private readonly IStoreWriter<SchoolContext, Student> _writerStore;
        private readonly IStoreBulkWriter<Student> _bulkWriterStore;
        private readonly IStoreReader<CourseTest> _storeCourseTest;
        private readonly ILogger<StudentController> _logger;

        public StudentController(
            ILogger<StudentController> logger,
            UnitOfWorkProperty<SchoolContext> context,
            UnitOfWorkProperty<DbConnection> connection,
            UnitOfWorkProperty<DbTransaction> transaction,
            SchoolContext context2,
            IStoreReader<Student> readerStore,
            ICountableStorage<Student> countableStorage,
            ICountableStorage<SchoolContext, Student> countableStorageCountable2,
            IStorageTransportable<Student> storageTransportable,
            IStorageTransportable<SchoolContext, Student> storageTransportable2,
            IStoreWriter<SchoolContext, Student> writerStore,
            IStoreBulkWriter<Student> bulkWriterStore,
            IStoreReader<CourseTest> storeCourseTest)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            _context2 = context2 ?? throw new ArgumentNullException(nameof(context2));
            _readerStore = readerStore ?? throw new ArgumentNullException(nameof(readerStore));
            _countableStorage = countableStorage ??
                                throw new ArgumentNullException(nameof(countableStorage));
            _countableStorageCountable2 = countableStorageCountable2 ??
                                 throw new ArgumentNullException(nameof(countableStorageCountable2));
            _storageTransportable = storageTransportable ??
                                throw new ArgumentNullException(nameof(storageTransportable));
            _storageTransportable2 = storageTransportable2 ??
                                     throw new ArgumentNullException(nameof(storageTransportable2));
            _writerStore = writerStore ?? throw new ArgumentNullException(nameof(writerStore));
            _bulkWriterStore = bulkWriterStore ??
                               throw new ArgumentNullException(nameof(bulkWriterStore));
            _storeCourseTest = storeCourseTest ??
                               throw new ArgumentNullException(nameof(storeCourseTest));
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .ToListAsync();

            var count = students.Count();

            var dataResult = new PaginatedResult<Student>(students, 0, (uint)count, count);

            return View(dataResult);
        }

        public IActionResult Search(string searchString, string button, uint? page)
        {
            uint pageOffset = Convert.ToUInt32(page.HasValue ? page.Value - 1 : 0);

            var query = _readerStore.AsFluentQuery()
                .Paginate(page ?? 1, 5);

            PaginatedResult<Student> students = !string.IsNullOrWhiteSpace(searchString)
                ? query
                    .Filter(f =>
                        f.FirstMidName.ToLower().Contains(searchString.ToLower()) ||
                        f.LastName.ToLower().Contains(searchString.ToLower()))
                    .LimitedSearch()
                : query.LimitedGet();

            ViewData["SearchString"] = searchString;

            // Caso o botão clicado seja "FiltrarAtualizar" nós
            // atualizamos o último nome de cada estudante encontrado
            // com o nome fake composto por um número aleatório, só
            // pra demonstrar o uso de Update() em massa
            if (!string.IsNullOrWhiteSpace(searchString) && button == "FiltrarAtualizar")
            {
                Expression<Func<Student, bool>> filterExpression = f =>
                    f.FirstMidName.ToLower().Contains(searchString.ToLower()) ||
                    f.LastName.ToLower().Contains(searchString.ToLower());

                var filter = new DataFilter<Student>()
                    .AddFilter(filterExpression);

                // Todos com mesmo segundo nome
                var random = new Random();

                // Alterando só um
                var first = students.Result.FirstOrDefault();

                if (first != null)
                {
                    var updated1 = _writerStore.AsFluentWriter()
                        .Identifier(first.ID)
                        .Update(new
                        {
                            LastName = $"LastName ({random.Next()})"
                        });

                    var updated2 = _writerStore.AsFluentWriter()
                        .Identifier(first.ID)
                        .Update(c => new
                        {
                            LastName = $"{c.LastName} ({random.Next()})"
                        });
                }

                // Alterando a coleção inteira
                var updatedResult1 = _bulkWriterStore.AsFluentBulkWriter()
                    .Filter(filterExpression)
                    .BulkUpdate(new
                    {
                        LastName = $"LastName {random.Next()}"
                    });

                // Cada um com seu próprio segundo nome
                var updatedResult2 = _bulkWriterStore.AsFluentBulkWriter()
                    .Filter(filterExpression)
                    .BulkUpdate(c => new
                    {
                        LastName = $"{c.LastName} ({random.Next()})"
                    });

                students = new PaginatedResult<Student>(
                    result: updatedResult2,
                    offset: students.Offset,
                    limit: students.Limit,
                    total: students.Total
                );
            }

            return View(nameof(Index), students);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public IActionResult Details2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _readerStore.AsFluentQuery()
                .Projection()
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Project()
                .Filter(w => w.ID == id)
                .Search()
                .FirstOrDefault();

            var studentResume = _readerStore.AsFluentQuery()
                .Projection()
                    .Include(i => i.Enrollments)
                    .Map(m => new
                    {
                        m.FirstMidName,
                        m.LastName
                    })
                    .Project()
                .Filter(w => w.Enrollments.Any())
                .Sort(s => s.FirstMidName)
                .LimitedSearch().Result;

            foreach (var sr in studentResume)
            {
                _logger.LogDebug("First name: {0}, Last name: {1}", sr.FirstMidName, sr.LastName);
            }

            if (student == null)
            {
                return NotFound();
            }

            return View(nameof(Details), student);
        }

        public IActionResult Details3(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _readerStore.AsFluentQuery()
                .Projection()
                    .Include<Enrollment>(i => i.Enrollments)
                        .ThenInclude<Course>(i => i.Course)
                    .Project()
                .Find(id);

            if (student == null)
            {
                return NotFound();
            }

            var createdStudent = _writerStore.Create(new Student
            {
                FirstMidName = student.FirstMidName + " (copy)",
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate
            });

            _logger.LogDebug($"Novo estudante criado com ID: {createdStudent.ID}");

            var allCoursesTests = _storeCourseTest.AsFluentQuery()
                .GetAll();

            foreach (var courseTest in allCoursesTests)
            {
                var reload = _storeCourseTest.Find(courseTest.Identifiers);
                _logger.LogDebug($"CourseTest {{ CourseID: {reload.CourseID}, CourseGUID: {reload.CourseGUID} }}");
            }

            var allCoursesTests2 = new RawSqlRideRepository<CourseTest>(_context, "SELECT * FROM course_test")
                .AsFluentQuery()
                .GetAll();

            foreach (var courseTest in allCoursesTests)
            {
                var reload = _storeCourseTest.Find(courseTest.Identifiers);
                _logger.LogDebug($"CourseTest {{ CourseID: {reload.CourseID}, CourseGUID: {reload.CourseGUID} }}");
            }
            
            var allCoursesTests3 = _storageTransportable.GetAll();
            var allCoursesTests4 = _storageTransportable2.GetAll();

            throw new NotImplementedException("Isso deve gerar um IUnitOfWork.DiscardWork()");
        }

        public IActionResult Contar()
        {
            var count = _countableStorage.CountAll();

            count = _countableStorageCountable2.CountAll();

            return View(count);
        }
    }
}
