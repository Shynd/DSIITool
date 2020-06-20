using DSIITool.Logging.Enums;
using DSIITool.Logging.Implementations;
using DSIITool.Logging.Interfaces;
using System.Linq;
using System.Runtime.CompilerServices;

// TODO: Filter log messages based on level.
// ^ By this I mean, create a function like 'SetLogLevel(LogLevel level)'
//   in this class, and only log/display messages above or at the
//   same level as defined by CurrentLoglevel.
// I suck at formulating documentation by the way, but this should be obvious.

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
