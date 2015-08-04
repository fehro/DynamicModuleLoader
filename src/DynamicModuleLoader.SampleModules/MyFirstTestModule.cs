using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicModuleLoader.Common.Contracts;

namespace DynamicModuleLoader.SampleModules
{
    public class MyFirstTestModule: IModule
    {
        public void Tick()
        {
            throw new NotImplementedException();
        }
    }
}
