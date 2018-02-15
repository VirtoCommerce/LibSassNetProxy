using System;
using LibSassNet;

namespace LibSassNetTest2
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                Compile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.HResult);
            }
        }

        private static void Compile()
        {
            var compiler = new SassCompiler();
            const string input = "$primary-color: #333; body {color: $primary-color;}";
            var output = compiler.Compile(input, OutputStyle.Compressed, false);
            Console.WriteLine(output);
        }
    }
}
