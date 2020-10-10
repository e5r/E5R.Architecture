using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using E5R.Architecture.Data.Abstractions;
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
        private readonly SchoolContext _context;
        private readonly SchoolContext _context2;
        private readonly DbConnection _connection;
        private readonly DbTransaction _transaction;
        private readonly IStorageReader<Student> _readerStorage;
        private readonly IStorageWriter<SchoolContext, Student> _writeStorage;
        private readonly ILogger<StudentController> _logger;

        public StudentController(
            SchoolContext context,
            UnitOfWorkProperty<SchoolContext> context2,
            UnitOfWorkProperty<DbConnection> connection,
            UnitOfWorkProperty<DbTransaction> transaction,
            IStorageReader<Student> readerStorage,
            IStorageReader<SchoolContext, Student> readerStorage2,
            IStorageWriter<SchoolContext, Student> writeStorage,
            ILogger<StudentController> logger
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _context2 = context2 ?? throw new ArgumentNullException(nameof(context2));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            _readerStorage = readerStorage ?? throw new ArgumentNullException(nameof(readerStorage));
            _writeStorage = writeStorage ?? throw new ArgumentNullException(nameof(writeStorage));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .ToListAsync();

            return View(students);
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
            var student = _readerStorage.Find(new Student { ID = id.Value });

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

        public async Task<IActionResult> Details3(int? id)
        {
            var student = _readerStorage.Find(new Student { ID = id.Value });

            if (student == null)
            {
                return NotFound();
            }

            var createdStudent = _writeStorage.Create(new Student
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
