using E5R.Architecture.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UsingDataEntityFrameworkCore.Models;

using static E5R.Architecture.Core.Utils.AttributableValueUtil;

namespace UsingDataEntityFrameworkCore.Data.Filter
{
    public class StudentObjectFilter : IdentifiableExpressionMaker<Student>
    {
        public AttributableValue<int> ByID { get; set; }
        public AttributableValue<string> ByLastName { get; set; }
        public AttributableValue<string> LastNameStartsWith { get; set; }
        public AttributableValue<string> LastNameEndsWith { get; set; }
        public AttributableValue<string> LastNameContains { get; set; }
        public AttributableValue<string> ByFirstMidName { get; set; }
        public AttributableValue<string> FirstMidNameStartWith { get; set; }
        public AttributableValue<string> FirstMidNameEndsWith { get; set; }
        public AttributableValue<string> FirstMidNameContains { get; set; }

        protected override IEnumerable<Expression<Func<Student, bool>>> MakeExpressions()
        {
            if (Assigned(ByID))
            {
                yield return w => w.ID == ByID;
            }

            if (Assigned(ByFirstMidName))
            {
                yield return w => w.FirstMidName.Equals(ByFirstMidName);
            }

            if (Assigned(FirstMidNameContains))
            {
                yield return w => w.FirstMidName.ToLower().Contains(FirstMidNameContains.Value.ToLower());
            }

            // TODO: Implementar demais filtros
        }
    }
}
