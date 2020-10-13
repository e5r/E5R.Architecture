using System.Data.Common;

namespace E5R.Architecture.Infrastructure
{
    public class DbTransactionUnitOfWork : UnitOfWorkByProperty
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
