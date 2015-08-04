using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicModuleLoader.Common.Attributes
{
    public class ModuleControl : System.Attribute
    {
        #region Global Variables / Properties

        /// <summary>
        /// The frequency the module should tick in seconds.
        /// </summary>
        public int TickFrequencySeconds { get; set; }

        #endregion

        #region Constructor

        public ModuleControl()
        {
            TickFrequencySeconds = 1;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the module control attribute for the provided type.
        /// </summary>
        public static ModuleControl GetModuleControlAttribute(Type type)
        {
            var attributes = System.Attribute.GetCustomAttributes(type);

            return attributes.OfType<ModuleControl>().FirstOrDefault();
        }

        #endregion
    }
}
