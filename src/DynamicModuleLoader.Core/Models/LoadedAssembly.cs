using System;
using System.Collections.Generic;
using DynamicModuleLoader.Core.Contracts;

namespace DynamicModuleLoader.Core.Models
{
    public class LoadedAssembly : ILoadedAssembly
    {
        #region Implemented ILoadedAssembly Members

        public string FileName { get; set; }

        public List<Type> ModuleTypes { get; set; }

        #endregion

        #region Constructor

        public LoadedAssembly()
        {
            ModuleTypes = new List<Type>();
        }

        #endregion
    }
}
