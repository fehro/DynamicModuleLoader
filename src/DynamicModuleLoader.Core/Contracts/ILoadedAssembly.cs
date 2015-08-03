using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicModuleLoader.Core.Contracts
{
    public interface ILoadedAssembly
    {
        string FileName { get; set; }

        List<Type> ModuleTypes { get; set; } 
    }
}
