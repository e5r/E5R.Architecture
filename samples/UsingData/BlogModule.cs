using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    public class BlogModule : DataModule<BlogStorage>
    {
        public BlogModule(IUnitOfWork uow, BlogStorage storage)
        {
            Storage1 = storage.Configure(uow.Session);
        }

        public BlogStorage Blog => Storage1;
    }
}
