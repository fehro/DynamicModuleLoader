using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DynamicModuleLoader.Core.Infrastructure
{
    internal static class AssemblyLoader
    {
        #region Public Methods

        /// <summary>
        /// Load the class types in the assembly with the provided file name that implement the generic interface.
        /// </summary>
        public static List<Type> LoadAssemblyClasses<TInterface>(FileSystemInfo fileInfo)
        {
            //Load the assembly.
            var assembly = Assembly.LoadFrom(fileInfo.Name);

            //Return the implementing class types from the assembly.
            return GetInstantiatingClassesFromAssembly<TInterface>(assembly);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get any class types in the assembly that implement the generic interface.
        /// </summary>
        private static List<Type> GetInstantiatingClassesFromAssembly<TInterface>(Assembly assembly)
        {
            var returnValue = new List<Type>();

            //Load the types defined in the assembly.
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.GetInterface(typeof(TInterface).ToString()) == null) continue;

                returnValue.Add(type);
            }

            return returnValue;
        }

        #endregion
    }
}
