using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DynamicModuleLoader.Common.Contracts;
using DynamicModuleLoader.Common.Delegates;
using DynamicModuleLoader.Common.Infrastructure;
using DynamicModuleLoader.Core.Contracts;
using DynamicModuleLoader.Core.Infrastructure;

namespace DynamicModuleLoader.Core
{
    public class AssemblyManager: LoggableClass, IAssemblyManager
    {
        #region Global Variables / Properties

        private readonly DirectoryScanner _directoryScanner;
        private readonly ModuleManager _moduleManager;

        /// <summary>
        /// The pool of loaded assemblies.
        /// </summary>
        private List<ILoadedAssembly> AssemblyPool { get; set; }

        #endregion

        #region Constructor

        public AssemblyManager(string modulesDirectory, string moduleFilePattern, ILogger logger = null)
            :base(logger)
        {
            //Initialize the assembly pool.
            AssemblyPool = new List<ILoadedAssembly>();

            //Setup the directory scanner.
            _directoryScanner = new DirectoryScanner(modulesDirectory, moduleFilePattern);
            _directoryScanner.RegisterOnFileFoundListener(OnFileFoundListener);

            //Setup the module manager.
            _moduleManager = new ModuleManager(logger);
        }

        #endregion

        #region Implemented IAssemblyManager Members

        /// <summary>
        /// Start the assembly manager.
        /// </summary>
        public void Start()
        {
            //Start the directory scanner.
            _directoryScanner.Start();
        }

        //Stop the assembly manager.
        public void Stop()
        {
            //Stop the directory scanner.
            _directoryScanner.Stop();

            //Clear any loaded assemblies.
            AssemblyPool.Clear();
        }

        /// <summary>
        /// Register an on module added listener.
        /// </summary>
        public void RegisterOnModuleAddedListener(Event<Type> listener)
        {
            _moduleManager.OnModuleLoadedEventEmitter.RegisterListener(listener);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The on file found listener.
        /// </summary>
        private void OnFileFoundListener(FileInfo fileInfo)
        {
            ////Write to the log.
            //base.LogEvent(string.Format("Tick"));

            //Check if the assembly has not been loaded yet.
            if (!HasAssemblyBeenLoaded(fileInfo.Name))
            {
                //Write to the log.
                base.LogEvent(string.Format("Found new assembly {0}", fileInfo.Name));

                //Load the assembly.
                LoadAssembly(fileInfo.Name);
            }
        }

        /// <summary>
        /// Load the assembly with the provided file name.
        /// </summary>
        private void LoadAssembly(string fileName)
        {
            try
            {
                lock (this)
                {
                    //Add the assembly to the pool of loaded assemblies.
                    ILoadedAssembly assembly = new Models.LoadedAssembly
                    {
                        FileName = fileName
                    };
                    AssemblyPool.Add(assembly);

                    //Load any new modules for the assembly.
                    _moduleManager.LoadModulesForAssembly(ref assembly);
                }
            }
            catch (Exception ex)
            {
                //Write to the log.
                base.LogEvent(string.Format("Exception whilst loading assembly {0}", fileName));
            }
        }

        /// <summary>
        /// Return the loaded assembly in the pool with the provided file name.
        /// </summary>
        private ILoadedAssembly GetLoadedAssembly(string fileName)
        {
            return AssemblyPool.SingleOrDefault(x => x.FileName == fileName);
        }

        /// <summary>
        /// Return true or false if the assembly with the provided file name has already been loaded into the pool.
        /// </summary>
        private bool HasAssemblyBeenLoaded(string fileName)
        {
            return GetLoadedAssembly(fileName) != null;
        }

        #endregion
    }
}
