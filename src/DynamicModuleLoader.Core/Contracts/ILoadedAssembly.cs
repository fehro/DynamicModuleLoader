using System;
using System.Collections.Generic;

namespace DynamicModuleLoader.Core.Contracts
{
    interface ILoadedAssembly
    {
        string FileName { get; set; }

        List<Type> ModuleTypes { get; set; } 
    }
}
