using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicModuleLoader.Core.Contracts;

namespace DynamicModuleLoader.Common.Contracts
{
    public interface IModuleWrapper
    {
        string FileName { get; set; }

        DateTime ModifiedOn { get; set; }

        List<IModule> Modules { get; set; }
    }
}
