using System;

namespace DynamicModuleLoader.Core.Contracts
{
    public interface IModuleManager
    {
        void Start();

        void Stop();

        void RegisterOnModuleAddedListener(EventHandler<Type> listenerCallback);
    }
}
