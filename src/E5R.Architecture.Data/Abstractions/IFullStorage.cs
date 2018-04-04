using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IFullStorage<TModel, TIdentifier> : IReadingWritingStorage<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
        IEnumerable<TModel> BulkCreate(IEnumerable<TModel> models);
        IEnumerable<TModel> Replace(IEnumerable<TModel> models);
        void Remove(IEnumerable<TIdentifier> ids);
    }
}