using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    public class BlogModule
    {
        public BlogModule(IUnitOfWork uow, BlogStorage storage)
        {
            Blog = storage.ConfigureSession(uow.Session);
        }

        public BlogStorage Blog { get; }
    }
}
