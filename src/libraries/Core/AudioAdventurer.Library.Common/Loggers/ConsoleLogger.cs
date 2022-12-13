using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string message)
        {
            WriteLog("DBG", message);
        }

        public void Info(string message)
        {
            WriteLog("INF", message);
        }

        public void Warning(string message)
        {
            WriteLog("WRN", message);
        }

        public void Error(string message)
        {
            WriteLog("ERR", message);
        }

        public void Exception(Exception ex)
        {
            WriteLog("EXP", ex.ToString());
        }

        public void WriteLog(
            string type,
            string message)
        {
            DateTime now = DateTime.UtcNow;

            Console.WriteLine($"{now:s} - {type} - {message}");
        }
    }
}
