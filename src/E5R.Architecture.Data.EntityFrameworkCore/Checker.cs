using System;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    internal class Checker : Core.Checker
    {
        public static void NotNullContext(DbContext context)
        {
            if (context == null)
            {
                // TODO: Implementar internacionalização
                throw new NullReferenceException(
                    $"The context is null. The session has not been configured.");
            }
        }
    }
}
