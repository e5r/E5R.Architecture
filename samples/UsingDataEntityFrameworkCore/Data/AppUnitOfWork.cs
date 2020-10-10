using System.Data.Common;
using E5R.Architecture.Infrastructure;

namespace UsingDataEntityFrameworkCore.Data
{
    // TODO: Mover para E5R.Architecture.Infrastructure
    public class AppUnitOfWork : UnitOfWorkByProperty
    {
        public override void DiscardWork()
        {
            var transaction = Property<DbTransaction>();

            if (transaction != null)
            {
                transaction.Rollback();
            }
        }

        public override void SaveWork()
        {
            var transaction = Property<DbTransaction>();

            if (transaction != null)
            {
                transaction.Commit();
            }
        }
    }
}
