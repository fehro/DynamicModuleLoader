using System;

namespace DynamicModuleLoader.Core.Contracts
{
    public interface IAssemblyManager
    {
        void Start();

        void Stop();

        //void RegisterOnModuleAddedListener(EventHandler<Type> listenerCallback);
    }
}
