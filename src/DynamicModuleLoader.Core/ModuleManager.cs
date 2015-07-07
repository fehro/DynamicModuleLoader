using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DynamicModuleLoader.Common.Contracts;
using DynamicModuleLoader.Core.Contracts;

namespace DynamicModuleLoader.Core
{
    public class ModuleManager : IModuleManager
    {
        #region Global Variables / Properties

        //todo: Remove these.
        private const string ModulesFolder = "";
        private const int ModuleScanFrequency = 1000;
        private const string ModulesFileExtension = "*.SampleModules.dll";

        public List<ILoadedAssembly> LoadedAssemblies { get; private set; }

        //Injectable logger.
        private readonly ILogger _logger;

        //The timer object.
        private Timer _timer;

        //The calculated modules directory to scan.
        private string _modulesDirectory;

        #endregion

        #region Constructor

        public ModuleManager(ILogger logger)
        {
            //Set the injected logger.
            _logger = logger;

            //Initialize the module manager.
            Initialize();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialize the module manager.
        /// </summary>
        private void Initialize()
        {
            LoadedAssemblies = new List<ILoadedAssembly>();

            //Calculate the modules directory.
            _modulesDirectory = AppDomain.CurrentDomain.BaseDirectory + ModulesFolder;

            //Setup the timer to perform a rescan of the modules.
            _timer = new Timer(ScanModules, null, TimeSpan.Zero, TimeSpan.FromSeconds(ModuleScanFrequency));
        }

        /// <summary>
        /// Scan for new, updated or deleted modules.
        /// </summary>
        private void ScanModules(object state)
        {
            //Get all the file names in the modules directory.
            var fileNames = Directory.GetFiles(_modulesDirectory, ModulesFileExtension);

            LogEvent(string.Format("Found {0} files in directory {1}", fileNames.Count(), _modulesDirectory));

            foreach (var fileName in fileNames)
            {
                //Process the file.
                ProcessFile(fileName);
            }
        }

        /// <summary>
        /// Process a file and load it if required.
        /// </summary>
        private void ProcessFile(string fileName)
        {
            try
            {
                //Get the file info.
                var fileInfo = new FileInfo(fileName);

                //Check if the assembly has not been loaded yet.
                if (!HasAssemblyBeenLoaded(fileName, fileInfo.LastWriteTimeUtc))
                {
                    HandleAssemblyAdd(fileName);
                }

                //Check if the assembly has been updated since it was loaded.
                if (HasAssemblyBeenUpdated(fileName, fileInfo.LastWriteTimeUtc))
                {
                    HandleAssemblyUpdate(fileName);
                }
            }
            catch (Exception ex)
            {
                LogEvent(string.Format("Exception whilst processing file {0}\n{1}", fileName, ex.Message));
            }
        }

        /// <summary>
        /// Handle an updated assembly.
        /// </summary>
        private void HandleAssemblyUpdate(string fileName)
        {
            //Load the assembly.
            var assembly = Assembly.LoadFrom(fileName);

            //Get the IModule instantiating types from the assembly.
            var types = GetIModuleInstantiatingTypesFromAssembly(ref assembly);
        }

        /// <summary>
        /// Handle a new assembly.
        /// </summary>
        private void HandleAssemblyAdd(string fileName)
        {
            //Load the assembly.
            var assembly = Assembly.LoadFrom(fileName);

            //Get the IModule instantiating types from the assembly.
            var types = GetIModuleInstantiatingTypesFromAssembly(ref assembly);
        }

        /// <summary>
        /// Get any types in the assembly that instantiate the IModule interface.
        /// </summary>
        private IEnumerable<Type> GetIModuleInstantiatingTypesFromAssembly(ref Assembly assembly)
        {
            var returnValue = new List<Type>();

            //Load the types from the assembly and loop instantiating any that are unloaded.
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                //If the type does not implement the IModule interface then just continue.
                if (type.GetInterface(typeof(IModule).ToString()) == null) continue;

                returnValue.Add(type);
            }

            return returnValue;
        }

    /// <summary>
        /// Has the assembly been loaded yet?
        /// </summary>
        private bool HasAssemblyBeenLoaded(string fileName, DateTime lastModified)
        {
            return LoadedAssemblies.Any(x => x.FileName == fileName);
        }

        /// <summary>
        /// Has the assembly been updated since it was loaded?
        /// </summary>
        private bool HasAssemblyBeenUpdated(string fileName, DateTime lastModified)
        {
            return LoadedAssemblies.Any(x => x.FileName == fileName && x.ModifiedOn == lastModified);
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
