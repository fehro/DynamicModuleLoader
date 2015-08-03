using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DynamicModuleLoader.Core.Contracts
{
    public interface IModuleManager
    {
        void Start();

        void Stop();

        void RegisterOnModuleAddedListener(EventHandler<Type> listenerCallback);
    }
}
