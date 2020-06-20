using DSIITool.Logging.Enums;
using DSIITool.Logging.Implementations;
using DSIITool.Logging.Interfaces;
using System.Linq;
using System.Runtime.CompilerServices;

// TODO: Filter log messages based on level.

namespace DSIITool.Logging
{
    internal static class LogManager
    {
        internal static LogLevel CurrentLogLevel { get; set; }

        internal static ILogger GetLogger([CallerFilePath] string name = "")
        {
            var caller = name.Split('\\').LastOrDefault().Split('.')[0];
            return new ConsoleLogger(caller);
        }
    }
}
