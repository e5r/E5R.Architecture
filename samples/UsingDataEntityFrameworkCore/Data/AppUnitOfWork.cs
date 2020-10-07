using E5R.Architecture.Infrastructure;
using System.Data.Common;

namespace UsingDataEntityFrameworkCore.Data
{
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
