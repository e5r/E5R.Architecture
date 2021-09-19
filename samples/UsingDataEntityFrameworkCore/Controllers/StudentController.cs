using E5R.Architecture.Core;
using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.Abstractions.Alias;
using E5R.Architecture.Data.EntityFrameworkCore.Alias;
using E5R.Architecture.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UsingDataEntityFrameworkCore.Data;
using UsingDataEntityFrameworkCore.Data.Filter;
using UsingDataEntityFrameworkCore.Models;

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
        private readonly ICountableStorage<SchoolContext, Student> _countableStorage2;
        private readonly IAcquirableStorage<Student> _acquirableStorage;
        private readonly IAcquirableStorage<SchoolContext, Student> _acquirableStorage2;
        private readonly IStoreWriter<SchoolContext, Student> _writerStore;
        private readonly IStoreBulkWriter<Student> _bulkWriterStore;
        private readonly IStoreReader<CourseTest> _storeCourseTest;
        private readonly ILogger<StudentController> _logger;
        private readonly ILazy<ICreatableStorage<Log>> _creatableStorage;

        public StudentController(
            ILogger<StudentController> logger,
            UnitOfWorkProperty<SchoolContext> context,
            UnitOfWorkProperty<DbConnection> connection,
            UnitOfWorkProperty<DbTransaction> transaction,
            SchoolContext context2,
            IStoreReader<Student> readerStore,
            ICountableStorage<Student> countableStorage,
            ICountableStorage<SchoolContext, Student> countableStorage2,
            IAcquirableStorage<Student> acquirableStorage,
            IAcquirableStorage<SchoolContext, Student> acquirableStorage2,
            IStoreWriter<SchoolContext, Student> writerStore,
            IStoreBulkWriter<Student> bulkWriterStore,
            IStoreReader<CourseTest> storeCourseTest,
            ILazy<ICreatableStorage<Log>> creatableStorage)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            _context2 = context2 ?? throw new ArgumentNullException(nameof(context2));
            _readerStore = readerStore ?? throw new ArgumentNullException(nameof(readerStore));
            _countableStorage = countableStorage ??
                                throw new ArgumentNullException(nameof(countableStorage));
            _countableStorage2 = countableStorage2 ??
                                 throw new ArgumentNullException(nameof(countableStorage2));
            _acquirableStorage = acquirableStorage ??
                                throw new ArgumentNullException(nameof(acquirableStorage));
            _acquirableStorage2 = acquirableStorage2 ??
                                     throw new ArgumentNullException(nameof(acquirableStorage2));
            _writerStore = writerStore ?? throw new ArgumentNullException(nameof(writerStore));
            _bulkWriterStore = bulkWriterStore ??
                               throw new ArgumentNullException(nameof(bulkWriterStore));
            _storeCourseTest = storeCourseTest ??
                               throw new ArgumentNullException(nameof(storeCourseTest));
            _creatableStorage = creatableStorage ??
                                throw new ArgumentNullException(nameof(creatableStorage));
        }

        public async Task<IActionResult> Index()
        {
            _creatableStorage.Value.Create(new Log
            { Date = DateTime.Now, Message = "Entrou em Student/Index" });

            var students = await _context.Students
                .ToListAsync();

            var count = students.Count();

            var dataResult = new PaginatedResult<Student>(students, 0, (uint)count, count);

            return View(dataResult);
        }

        public IActionResult Search(string searchString, string button, uint? page)
        {
            _creatableStorage.Value.Create(new Log
            { Date = DateTime.Now, Message = "Entrou em Student/Search" });

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

                var filter = new ExpressionDataFilter<Student>()
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
            _creatableStorage.Value.Create(new Log
            { Date = DateTime.Now, Message = "Entrou em Student/Details" });

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
            _creatableStorage.Value.Create(new Log
            { Date = DateTime.Now, Message = "Entrou em Student/Details2" });

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
            _creatableStorage.Value.Create(new Log
            { Date = DateTime.Now, Message = "Entrou em Student/Details3" });

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

            var allCoursesTests3 = _acquirableStorage.GetAll();
            var allCoursesTests4 = _acquirableStorage2.GetAll();

            throw new NotImplementedException("Isso deve gerar um IUnitOfWork.DiscardWork()");
        }

        public IActionResult Contar()
        {
            var objectFilter = new StudentObjectFilter();

            //objectFilter.ByID = 3;

            //int byIdValue1 = objectFilter.ByID;
            //var byIdValue2 = (int)objectFilter.ByID;

            objectFilter.FirstMidNameContains = "Er";

            var count1 = _countableStorage.CountAll();
            var count2 = _countableStorage.Count(new ObjectDataFilter<Student>(objectFilter));

            return View((count1, count2));
        }
    }
}
