using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    public class BlogDataModule : ITradableObject
    {
        public BlogDataModule(IUnitOfWork uow, BlogStorage storage)
        {
            Blog = storage;

            ConfigureSession(uow.Session);
        }

        public void ConfigureSession(UnderlyingSession session)
        {
            Blog.ConfigureSession(session);
        }

        public BlogStorage Blog { get; }
    }
}
