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

        public static void NotNullObject(object @object, string objName)
        {
            if (@object == null)
            {
                // TODO: Implementar internacionalização
                throw new NullReferenceException($"Object {objName} can not be null.");
            }
        }
    }
}
