using System;

namespace E5R.Architecture.Core.Abstractions
{
    public interface IIoCContainer
    {
        void Register(IoCLifecycle lifecycle, Type implementationType);
        void Register(IoCLifecycle lifecycle, Type baseType, Type implementationType);
        void Register(IoCLifecycle lifecycle, Type baseType, Func<object> implementationFactory);
    }
}
