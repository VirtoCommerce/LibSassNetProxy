using System;
using System.IO;
using System.Reflection;
using LibSassNetProxy;

namespace LibSassNetTest
{
    class Program
    {
        static void Main()
        {
            MultiplatformAssemblyLoader.Enable = true;

            Console.WriteLine("Is64BitProcess {0}", Environment.Is64BitProcess);

            var compiler = new SassCompilerProxy();
            const string input = "$primary-color: #333; body {color: $primary-color;}";
            var output = compiler.Compile(input);

            Console.WriteLine(output);
        }
    }

    public static class MultiplatformAssemblyLoader
    {
        private static bool _isEnabled;

        public static bool Enable
        {
            get { return _isEnabled; }
            set
            {
                lock (typeof(MultiplatformAssemblyLoader))
                {
                    if (_isEnabled != value)
                    {
                        if (value)
                            AppDomain.CurrentDomain.AssemblyResolve += Resolve;
                        else
                            AppDomain.CurrentDomain.AssemblyResolve -= Resolve;
                        _isEnabled = value;
                    }
                }
            }
        }

        /// Will attempt to load missing assembly from either x86 or x64 subdir
        private static Assembly Resolve(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name);
            var fileName = assemblyName.Name + ".dll";
            var appDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var assemblyPath = Path.Combine(appDirectory, "assemblies", Environment.Is64BitProcess ? "x64" : "x86", fileName);
            Console.WriteLine(assemblyPath);

            return File.Exists(assemblyPath)
                ? Assembly.LoadFrom(assemblyPath)
                : null;
        }
    }
}
