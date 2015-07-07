using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicModuleLoader.Common.Contracts;
using DynamicModuleLoader.Core.Contracts;

namespace DynamicModuleLoader.Core.Models
{
    public class LoadedAssembly : ILoadedAssembly
    {
        #region Implemented ILoadedAssembly Members

        public string FileName { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<string> ModuleAssemblyTypes { get; set; }

        #endregion
    }
}
