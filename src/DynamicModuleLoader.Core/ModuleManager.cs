using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DynamicModuleLoader.Core.Contracts;

namespace DynamicModuleLoader.Core
{
    public class ModuleManager : IModuleManager
    {
        #region Global Variables / Properties

        public const int ModuleScanFrequency = 1000;

        private Timer _timer;

        private string _modulesDirectory;

        private const string ModulesFolder = "";

        #endregion

        #region Constructor

        public ModuleManager()
        {
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
            //Setup the timer to perform a rescan of the modules.
            _timer = new Timer(ScanModules, null, TimeSpan.Zero, TimeSpan.FromSeconds(ModuleScanFrequency));

            //Calculate the modules directory.
            _modulesDirectory = AppDomain.CurrentDomain.BaseDirectory + ModulesFolder;
        }

        /// <summary>
        /// Scan for new, updated or deleted modules.
        /// </summary>
        private void ScanModules(object state)
        {
            //Get all the file names in the modules directory.
            var fileNames = Directory.GetFiles(_modulesDirectory, "*.dll");

            foreach (var fileName in fileNames)
            {
                //Get the file info for the current file name.
                var fileInfo = new FileInfo(fileName);
            }
        }

        #endregion
    }
}
