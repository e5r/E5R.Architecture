using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IStorageReader<Student> _storage;
        private readonly IStorageReader<SchoolContext, Student> _storage2;

        public StudentController(
            SchoolContext context,
            UnitOfWorkProperty<SchoolContext> context2,
            UnitOfWorkProperty<DbConnection> connection,
            UnitOfWorkProperty<DbTransaction> transaction,
            IStorageReader<Student> storage,
            IStorageReader<SchoolContext, Student> storage2)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _context2 = context2 ?? throw new ArgumentNullException(nameof(context2));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _storage2 = storage2 ?? throw new ArgumentNullException(nameof(storage2));
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
            var student = _storage.Find(new Student { ID = id.Value });

            // NOTE: Com as projeções diretamente no IStorage, este trecho será desnecessário
            student.Enrollments = await _context.Enrollments
                .Where(w => w.StudentID == student.ID)
                .Include(i => i.Course)
                .ToListAsync();

            if (student == null)
            {
                return NotFound();
            }

            return View(nameof(Details), student);
        }

        public async Task<IActionResult> Details3(int? id)
        {
            var student = _storage2.Find(new Student { ID = id.Value });

            throw new NotImplementedException("Isso deve gerar um IUnitOfWork.DiscardWork()");
        }
    }
}
