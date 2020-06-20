using DSIITool.Logging.Enums;

namespace DSIITool.Logging.Interfaces
{
    internal interface ILogger
    {
        void Log(LogLevel level, string msg);
    }
}
