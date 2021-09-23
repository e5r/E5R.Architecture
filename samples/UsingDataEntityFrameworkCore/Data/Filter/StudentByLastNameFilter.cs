using E5R.Architecture.Core;
using System;
using System.Linq.Expressions;
using UsingDataEntityFrameworkCore.Models;
using UsingDataEntityFrameworkCore.Utils;
using static E5R.Architecture.Core.Utils.AttributableValueUtil;

namespace UsingDataEntityFrameworkCore.Data.Filter
{
    using static ScapeSqlQuoteUtil;

    public class StudentByLastNameFilter : IIdentifiableExpressionMaker<Student>
    {
        public AttributableValue<string> LastName
        { get; set; }

        public Expression<Func<Student, bool>> MakeExpression()
        {
            return w => !Assigned(LastName) || string.Compare(w.LastName, LastName) == 0;
        }

        public string MakeSqlWhere(string tableAlias = null)
        {
            var ta = string.IsNullOrWhiteSpace(tableAlias) ? string.Empty : $"{tableAlias}.";

            return Assigned(LastName)
                ? $"{ta}{nameof(Student.LastName)} = '{ScapeQuote(LastName)}'"
                : null;
        }
    }
}
