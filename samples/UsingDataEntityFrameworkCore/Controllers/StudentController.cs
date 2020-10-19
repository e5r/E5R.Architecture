using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.Abstractions.Alias;
using E5R.Architecture.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UsingDataEntityFrameworkCore.Data;
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
        private readonly IStoreWriter<SchoolContext, Student> _writerStore;
        private readonly ILogger<StudentController> _logger;

        public StudentController(
            ILogger<StudentController> logger,
            UnitOfWorkProperty<SchoolContext> context,
            UnitOfWorkProperty<DbConnection> connection,
            UnitOfWorkProperty<DbTransaction> transaction,
            SchoolContext context2,
            IStoreReader<Student> readerStore,
            IStoreWriter<SchoolContext, Student> writerStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            _context2 = context2 ?? throw new ArgumentNullException(nameof(context2));
            _readerStore = readerStore ?? throw new ArgumentNullException(nameof(readerStore));
            _writerStore = writerStore ?? throw new ArgumentNullException(nameof(writerStore));
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .ToListAsync();

            var count = students.Count();

            var dataResult = new DataLimiterResult<Student>(students, 0, (uint)count, count);

            return View(dataResult);
        }

        public IActionResult Search(string searchString, uint? page)
        {
            uint pageOffset = Convert.ToUInt32(page.HasValue ? page.Value - 1 : 0);

            // #if !DEBUG
            // Usando filtro implícito
            var query = _readerStore.Query()
                .OffsetBegin(pageOffset * PAGE_SIZE)
                .OffsetLimit(5);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query.AddFilter(f =>
                    f.FirstMidName.ToLower().Contains(searchString.ToLower()) ||
                    f.LastName.ToLower().Contains(searchString.ToLower())
                );
            }

            var students = query.LimitedSearch();
            // #else
            //             // Usando filtro explícito
            //             var filter = new LinqDataFilter<Student>();

            //             if (!string.IsNullOrWhiteSpace(searchString))
            //             {
            //                 filter = filter
            //                     .AddFilter(f =>
            //                         f.FirstMidName.ToLower().Contains(searchString.ToLower()) ||
            //                         f.LastName.ToLower().Contains(searchString.ToLower())
            //                     );
            //             }

            //             var students = _readerStore.Search(filter);
            // #endif

            ViewData["SearchString"] = searchString;

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

        public async Task<IActionResult> Details2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // TODO: Incluir projeção
            var student = _readerStore.Find(new Student { ID = id.Value });

            if (student == null)
            {
                return NotFound();
            }

            // NOTE: Com as projeções diretamente no IStorage, este trecho será desnecessário
            student.Enrollments = await _context.Enrollments
                .Where(w => w.StudentID == student.ID)
                .Include(i => i.Course)
                .ToListAsync();

            return View(nameof(Details), student);
        }

        public IActionResult Details3(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _readerStore.Find(new Student { ID = id.Value });

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

            throw new NotImplementedException("Isso deve gerar um IUnitOfWork.DiscardWork()");
        }
    }
}
