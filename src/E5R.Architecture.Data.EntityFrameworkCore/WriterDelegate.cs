using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public partial class StorageWriter<TModel>
    {
        protected delegate void WriterDelegate(object entity, Action<EntityEntryGraphNode> writer);
    }
}
