using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicModuleLoader.Common.Contracts
{
    public interface ILogger
    {
        void LogEvent(string value);
    }
}
