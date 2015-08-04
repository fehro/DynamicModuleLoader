using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicModuleLoader.Common.Contracts;

namespace DynamicModuleLoader.Common.Infrastructure
{
    public class LoggableClass
    {
        #region Global Variables / Properties

        private readonly ILogger _logger;

        #endregion

        #region Constructor

        public LoggableClass(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Log an event to the logger.
        /// </summary>
        protected void LogEvent(string value)
        {
            //If there is no logger then just return.
            if (_logger == null) return;

            _logger.LogEvent(value);
        }

        #endregion
    }
}
