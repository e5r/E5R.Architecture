using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    public abstract class DataModule
    {
        protected DataModule(IUnitOfWork uow)
        {
            ConfigureSession(uow.Session);
        }

        protected abstract void ConfigureSession(UnderlyingSession session);
    }
}
