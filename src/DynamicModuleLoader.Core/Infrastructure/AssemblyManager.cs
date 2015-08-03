using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using DynamicModuleLoader.Core.Contracts;

namespace DynamicModuleLoader.Core.Infrastructure
{
    internal class AssemblyManager
    {
        #region Global Variables / Properties
        
        private List<ILoadedAssembly> LoadedAssemblies { get; set; }

        #endregion

        #region Constructor

        public AssemblyManager()
        {
            LoadedAssemblies = new List<ILoadedAssembly>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clear any loaded assemblies.
        /// </summary>
        public void Clear()
        {
            LoadedAssemblies.Clear();   
        }

        /// <summary>
        /// Return the loaded assembly with the provided file name.
        /// </summary>
        public ILoadedAssembly GetLoadedAssembly(string fileName)
        {
            return LoadedAssemblies.SingleOrDefault(x => x.FileName == fileName);
        }

        /// <summary>
        /// Return true or false if the assembly with the provided file name has already been loaded.
        /// </summary>
        public bool HasAssemblyBeenLoaded(string fileName)
        {
            return GetLoadedAssembly(fileName) != null;
        }

        /// <summary>
        /// Add the provided module to the loaded assembly with the provided file name (or create if required).
        /// </summary>
        public void AddModule(string fileName, Type type)
        {
            lock (this)
            {
                //Get the assembly if it has already been loaded otherwise add it.
                var assembly = GetLoadedAssembly(fileName) ?? AddAssembly(fileName);

                //Add the module to the assembly.
                assembly.ModuleTypes.Add(type);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Add a new assembly with the provided details.
        /// </summary>
        private ILoadedAssembly AddAssembly(string fileName)
        {
            //Add the assembly to the list of loaded assemblies.
            var assembly = new Models.LoadedAssembly
            {
                FileName = fileName
            };
            LoadedAssemblies.Add(assembly);

            return assembly;
        }

        #endregion
    }
}
