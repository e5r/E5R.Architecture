using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UsingDataEntityFrameworkCore.Data.Filter;
using UsingDataEntityFrameworkCore.Models;

namespace UsingDataEntityFrameworkCore.Data.Repositories
{
    public class CountableStorageStudent : ICountableStorage<Student>
    {
        private readonly DbContext _context;

        public CountableStorageStudent(UnitOfWorkProperty<DbContext> context)
        {
            Checker.NotNullArgument(context, nameof(context));

            _context = context;
        }

        public int Count(IDataFilter<Student> filter)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            StudentObjectFilter f = filter.GetObjectFilter<StudentObjectFilter>();

            var query = _context.Set<Student>().AsQueryable();

            return f.TryAggregate(query).Count();
        }

        public int CountAll()
        {
            return _context.Set<Student>().Count();
        }
    }
}
