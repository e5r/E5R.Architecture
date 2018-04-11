using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    public class BlogDataModule
    {
        public BlogDataModule(IUnitOfWork uow, BlogStorage storage)
        {
            Blog = storage.ConfigureSession(uow.Session);
        }

        public BlogStorage Blog { get; }
    }
}
