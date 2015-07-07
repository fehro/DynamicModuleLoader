using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicModuleLoader.Common.Contracts;
using DynamicModuleLoader.Core.Contracts;

namespace DynamicModuleLoader.Core.Models
{
    public class ModuleWrapper: IModuleWrapper
    {
        #region Implemented IModuleWrapper Members

        public string FileName { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<IModule> Modules { get; set; }

        #endregion
    }
}
