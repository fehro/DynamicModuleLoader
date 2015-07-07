using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicModuleLoader.Common.Contracts;

namespace DynamicModuleLoader.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var moduleManager = new DynamicModuleLoader.Core.ModuleManager(new Logger());

            System.Console.WriteLine("Press any key to exit");

            System.Console.ReadLine();
        }
    }
}
