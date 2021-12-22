using E5R.Architecture.Core;
using System;
using System.Linq.Expressions;
using UsingDataEntityFrameworkCore.Models;

using static E5R.Architecture.Core.Utils.AttributableValueUtil;

namespace UsingDataEntityFrameworkCore.Data.Filter
{
    public class StudentByIdFilter : IIdentifiableExpressionMaker<Student>
    {
        public AttributableValue<int> ID { get; set; }

        public Expression<Func<Student, bool>> MakeExpression()
        {
            return w => !Assigned(ID) || w.ID == ID;
        }

        public string MakeSqlWhere(string tableAlias = null)
        {
            var ta = string.IsNullOrWhiteSpace(tableAlias) ? string.Empty : $"{tableAlias}.";

            return Assigned(ID)
                ? $"{ta}{nameof(Student.ID)} = {ID.Value}"
                : null;
        }
    }
}
