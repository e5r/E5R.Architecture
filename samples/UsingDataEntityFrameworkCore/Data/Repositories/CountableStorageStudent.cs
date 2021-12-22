using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
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

            var sqlWhereClauses = filter.GetObjects()
                .Select((o) => GetSqlWhereClause(o, "t"))
                .ToList();

            var sqlWhere = sqlWhereClauses.Any()
                ? string.Join(" and ", sqlWhereClauses)
                : string.Empty;

            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                var whereClause = !string.IsNullOrWhiteSpace(sqlWhere)
                    ? $" where {sqlWhere}"
                    : string.Empty
                    ;

                cmd.CommandText = $"select count(*) from {nameof(Student)} t{whereClause}";

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }

            return 0;
        }

        public int CountAll()
        {
            return _context.Set<Student>().Count();
        }

        private string GetSqlWhereClause(object objectFilterMaker, string tableAlias)
        {
            Checker.NotNullArgument(objectFilterMaker, nameof(objectFilterMaker));

            var objectType = objectFilterMaker.GetType();

            if (objectType == typeof(StudentByIdFilter))
            {
                return (objectFilterMaker as StudentByIdFilter).MakeSqlWhere(tableAlias);
            }
            else if (objectType == typeof(StudentByLastNameFilter))
            {
                return (objectFilterMaker as StudentByLastNameFilter).MakeSqlWhere(tableAlias);
            }
            else if (objectType == typeof(StudentFirstMidNameContainsFilter))
            {
                return (objectFilterMaker as StudentFirstMidNameContainsFilter).MakeSqlWhere(tableAlias);
            }

            throw new InvalidOperationException($"O tipo de filtro {objectType.Name} não está mapeado para SQL");
        }
    }
}
