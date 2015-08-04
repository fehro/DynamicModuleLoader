using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicModuleLoader.Common.Delegates
{
    public delegate void Event<in T>(T paramaters);
}
