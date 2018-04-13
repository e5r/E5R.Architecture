using System;
using System.Linq.Expressions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Data model (Entity) representation with identifier criteria
    /// </summary>
    /// <typeparam name="T">Type of model</typeparam>
    public abstract class DataModel<T>
        where T : class
    {
        /// <summary>
        /// Reducer that ensure the return of a single object.
        /// </summary>
        /// <returns>Reduce expression</returns>
        public abstract Expression<Func<T, bool>> GetIdenifierCriteria();
    }
}
