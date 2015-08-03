using System;
using System.IO;
using DynamicModuleLoader.Common.Contracts;
using DynamicModuleLoader.Core.Contracts;
using DynamicModuleLoader.Core.Infrastructure;

namespace DynamicModuleLoader.Core
{
    public class ModuleManager : IModuleManager
    {
        #region Global Variables / Properties

        private readonly ILogger _logger;
        private readonly DirectoryScanner _directoryScanner;
        private readonly AssemblyManager _assemblyManager;
        private readonly EventEmitter<Type> _onModuleLoadedEventEmitter;

        #endregion

        #region Constructor

        public ModuleManager(string modulesDirectory, string moduleFilePattern, ILogger logger = null)
        {
            //Set the injected logger.
            _logger = logger;

            //Setup the assembly manager.
            _assemblyManager = new AssemblyManager();

            //Setup the event emitters.
            _onModuleLoadedEventEmitter = new EventEmitter<Type>();

            //Setup the directory scanner.
            _directoryScanner = new DirectoryScanner(modulesDirectory, moduleFilePattern);
            _directoryScanner.RegisterOnFileFoundListener(OnFileFoundListener);
        }

        #endregion

        #region Implemented IModuleManager Members

        /// <summary>
        /// Start the module manager.
        /// </summary>
        public void Start()
        {
            //Start the directory scanner.
            _directoryScanner.Start();
        }

        /// <summary>
        /// Stop the module manager.
        /// </summary>
        public void Stop()
        {
            //Stop the directory scanner.
            _directoryScanner.Stop();

            //Clear any loaded assemblies in the assembly manager.
            _assemblyManager.Clear();
        }

        /// <summary>
        /// Register an on module added listener.
        /// </summary>
        public void RegisterOnModuleAddedListener(EventHandler<Type> listener)
        {
            _onModuleLoadedEventEmitter.RegisterListener(listener);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The on file found listener.
        /// </summary>
        private void OnFileFoundListener(object sender, FileInfo fileInfo)
        {
            try
            {
                //Check if the assembly has not been loaded yet.
                if (!_assemblyManager.HasAssemblyBeenLoaded(fileInfo.Name))
                {
                    //Write to the log.
                    LogEvent(string.Format("Found new file {0} in directory {1}", fileInfo.Name, fileInfo.Directory));

                    //Handle the load process of the new assembly.
                    HandleAssemblyLoad(fileInfo);
                }
            }
            catch (Exception ex)
            {
                //Write to the log.
                LogEvent(string.Format("Exception whilst processing file {0}\n{1}", fileInfo.Name, ex.Message));
            }
        }

        /// <summary>
        /// Handle the load process of a new assembly.
        /// </summary>
        private void HandleAssemblyLoad(FileSystemInfo fileInfo)
        {
            //Load the class types in the assembly that implement the IModule interface.
            var types = AssemblyLoader.LoadAssemblyClasses<IModule>(fileInfo);

            //Loop the types.
            foreach (var type in types)
            {
                try
                {
                    //Add the module.
                    _assemblyManager.AddModule(fileInfo.Name, type);

                    //Emit a module loaded event.
                    _onModuleLoadedEventEmitter.Emit(type);

                    //Write to the log.
                    LogEvent(string.Format("Added module {0} from file {1}", type.Name, fileInfo.Name));
                }
                catch (Exception ex)
                {
                    //Write to the log.
                    LogEvent(string.Format("Exception whilst adding module {0} from file {1}\n{2}", type.Name, fileInfo.Name, ex.Message));
                }
            }
        }

        /// <summary>
        /// Log an event to any injected logger.
        /// </summary>
        private void LogEvent(string value)
        {
            //If there is no injected logger then just return.
            if (_logger == null) return;

            _logger.LogEvent(value);
        }

        #endregion
    }
}
