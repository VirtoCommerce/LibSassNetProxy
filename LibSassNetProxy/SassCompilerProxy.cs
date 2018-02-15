using System.Collections.Generic;
using LibSassNet;

namespace LibSassNetProxy
{
    public class SassCompilerProxy
    {
        private static readonly ISassCompiler _compiler = new SassCompiler();

        public string Compile(string source, OutputStyle outputStyle = OutputStyle.Compressed, bool includeSourceComments = false, int precision = 5, IEnumerable<string> includePaths = null)
        {
            var internalOutputStyle = (LibSassNet.OutputStyle)outputStyle;
            return _compiler.Compile(source, internalOutputStyle, includeSourceComments, precision, includePaths);
        }
    }

    public enum OutputStyle
    {
        Nested,
        Expanded,
        Compact,
        Compressed,
        Echo,
    }
}
