using System;
using System.Collections.Generic;

namespace DynamicModuleLoader.Core.Contracts
{
    public interface ILoadedAssembly
    {
        string FileName { get; set; }

        List<Type> ModuleTypes { get; set; } 
    }
}
