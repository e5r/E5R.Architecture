using System;
using System.Linq.Expressions;

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
