using System;
using System.Threading;

namespace DynamicModuleLoader.Console
{
    class Program
    {
        private const string ModulesFolder = "";
        private const string ModulesFileExtension = "*.SampleModules.dll";

        static void Main(string[] args)
        {
            var moduleManager = new DynamicModuleLoader.Core.ModuleManager(ModulesFolder, ModulesFileExtension, new Logger());

            moduleManager.RegisterOnModuleAddedListener(TestListener);

            moduleManager.Start();

            System.Console.WriteLine("Press any key to exit");

            System.Console.ReadLine();
        }

        public static void TestListener(Object sender, Type type)
        {

            System.Console.WriteLine("Callback 1 " + type.Name);
            Thread.Sleep(1000);
            System.Console.WriteLine("Callback 1 " + type.Name + " done");
        }
    }
}
