using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    public class BlogDataModule : DataModule
    {
        public BlogDataModule(IUnitOfWork uow, BlogStorage storage)
        {
            Blog = storage;
            
            // TODO: Quando usamos base no construtor, ConfigureSession é chamado
            // antes de Blog ser instanciado
            base(uow);
        }

        protected override void ConfigureSession(UnderlyingSession session)
        {
            Blog.ConfigureSession(session);
        }
        
        public BlogStorage Blog { get; }
    }
}
