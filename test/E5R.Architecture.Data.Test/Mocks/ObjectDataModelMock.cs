using System;
using System.Linq.Expressions;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Test.Mocks
{
    internal class ObjectDataModelMock : DataModel<ObjectDataModelMock>
    {
        public override Expression<Func<ObjectDataModelMock, bool>> GetIdenifierCriteria()
        {
            throw new NotImplementedException();
        }
    }
}
