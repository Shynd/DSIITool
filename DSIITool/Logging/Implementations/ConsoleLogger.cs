using DSIITool.Logging.Enums;
using DSIITool.Logging.Interfaces;
using System;

namespace DSIITool.Logging.Implementations
{
    internal class ConsoleLogger : ILogger
    {
        private static string Name { get; set; }

        internal ConsoleLogger(string name)
        {
            Name = name;
        }

        public void Log(LogLevel level, string msg)
        {
            var lvl = level.ToString().ToUpper();
            Console.WriteLine($"[{lvl}::{Name}] - {msg}");
        }
    }
}
