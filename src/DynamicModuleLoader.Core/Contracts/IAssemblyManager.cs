using System;
using DynamicModuleLoader.Common.Delegates;

namespace DynamicModuleLoader.Core.Contracts
{
    public interface IAssemblyManager
    {
        void Start();

        void Stop();

        void RegisterOnModuleAddedListener(Event<Type> listener);
    }
}
