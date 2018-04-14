using System;

namespace E5R.Architecture.Core
{
    public class Checker
    {
        public static void NotNullArgument(object argObj, string argName)
        {
            if (argObj == null)
            {
                // TODO: Implementar internacionalização
                throw new ArgumentNullException(argName);
            }
        }
    }
}
