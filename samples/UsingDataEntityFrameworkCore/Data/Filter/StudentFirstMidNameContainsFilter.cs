using E5R.Architecture.Core;
using System;
using System.Linq.Expressions;
using UsingDataEntityFrameworkCore.Models;
using UsingDataEntityFrameworkCore.Utils;
using static E5R.Architecture.Core.Utils.AttributableValueUtil;

namespace UsingDataEntityFrameworkCore.Data.Filter
{
    using static ScapeSqlQuoteUtil;

    public class StudentFirstMidNameContainsFilter : IIdentifiableExpressionMaker<Student>
    {
        public AttributableValue<string> FirstMidName { get; set; }

        public Expression<Func<Student, bool>> MakeExpression()
        {
            return w => !Assigned(FirstMidName) || (w.FirstMidName ?? string.Empty).ToLower().Contains(FirstMidName.Value.ToLower());
        }

        public string MakeSqlWhere(string tableAlias = null)
        {
            var ta = string.IsNullOrWhiteSpace(tableAlias) ? string.Empty : $"{tableAlias}.";

            return Assigned(FirstMidName)
                ? $"{ta}{nameof(Student.FirstMidName)} like '%{ScapeQuote(FirstMidName)}%'"
                : null;
        }
    }
}
