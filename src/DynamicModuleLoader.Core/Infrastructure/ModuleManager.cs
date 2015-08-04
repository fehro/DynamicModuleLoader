using System;
using DynamicModuleLoader.Common.Contracts;
using DynamicModuleLoader.Common.Delegates;
using DynamicModuleLoader.Common.Infrastructure;
using DynamicModuleLoader.Core.Contracts;

namespace DynamicModuleLoader.Core.Infrastructure
{
    internal class ModuleManager: LoggableClass
    {
        #region Global Variables / Properties

        public EventEmitter<Type> OnModuleLoadedEventEmitter;

        #endregion

        #region Constructor

        public ModuleManager(ILogger logger = null)
            :base(logger)
        {
            OnModuleLoadedEventEmitter = new EventEmitter<Type>();
        }

        #endregion

        #region Implemented IModuleManager Members

        /// <summary>
        /// Start the module manager.
        /// </summary>
        public void LoadModulesForAssembly(ref ILoadedAssembly assembly)
        {
            //Load the module types from the assembly.
            var moduleTypes = AssemblyLoader.LoadAssemblyClasses<IModule>(assembly.FileName);

            if (moduleTypes.Count == 0)
            {
                //There were no module types found in the assembly.

                //Write to the log.
                base.LogEvent(string.Format("No modules found in assembly {0}", assembly.FileName));

                return;
            }

            foreach (var moduleType in moduleTypes)
            {
                try
                {
                    //Add the module to the assembly and emit an event.
                    AddModuleToAssembly(ref assembly, moduleType);

                    //Write to the log.
                    base.LogEvent(string.Format("Module {0} has been loaded from assembly {1}", moduleType.Name, assembly.FileName));
                }
                catch (Exception ex)
                {
                    //Write to the log.
                    base.LogEvent(string.Format("Exception whilst loading module {0} from assembly {1}", moduleType.Name, assembly.FileName));
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Add the module to the assembly and emit an event.
        /// </summary>
        private void AddModuleToAssembly(ref ILoadedAssembly assembly, Type moduleType)
        {
            //Add the module to the assembly.
            assembly.ModuleTypes.Add(moduleType);

            //Emit the event.
            OnModuleLoadedEventEmitter.Emit(moduleType);
        }

        #endregion
    }
}
