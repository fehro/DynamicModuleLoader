using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicModuleLoader.Common.Contracts;

namespace DynamicModuleLoader.Console
{
    public class Logger : ILogger
    {
        /// <summary>
        /// Log an event to the console window.
        /// </summary>
        public void LogEvent(string value)
        {
            System.Console.WriteLine(value);
        }
    }
}
