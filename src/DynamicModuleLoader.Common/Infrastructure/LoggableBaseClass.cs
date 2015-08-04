using DynamicModuleLoader.Common.Contracts;

namespace DynamicModuleLoader.Common.Infrastructure
{
    public class LoggableBaseClass
    {
        #region Global Variables / Properties

        protected ILogger Logger { get; private set; }

        #endregion

        #region Constructor

        public LoggableBaseClass(ILogger logger)
        {
            Logger = logger;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Log an event to the logger.
        /// </summary>
        protected void LogEvent(string component, string value)
        {
            //If there is no logger then just return.
            if (Logger == null) return;

            Logger.LogEvent(string.Format("[{0}] {1}", component, value));
        }

        #endregion
    }
}
