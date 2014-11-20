using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface ILogger
    {
        Task Debug(string categry, string message);
        Task Error(string categry, string message);
        Task Error(string categry, string message, Exception ex);
    }

    class Logger : ILogger
    {
        private const string LOGS_DIRECTORY = "Logs";
        private const string MESSAGE_FORMAT = "[{0}] [{1}] - [{2}] - {3}";

        public Logger()
        {
            if (!Directory.Exists(LOGS_DIRECTORY))
            {
                Directory.CreateDirectory(LOGS_DIRECTORY);
            }
        }

        public async Task Debug(string categry, string message)
        {
            await WriteMessage("D", categry, message);
        }

        public async Task Error(string categry, string message)
        {
            await WriteMessage("E", categry, message);
        }

        public async Task Error(string categry, string message, Exception ex)
        {
            message += "\r\n" + ex.ToString();

            await WriteMessage("E", categry, message);
        }

        private async Task WriteMessage(string severity, string category, string message)
        {
            string fileName = Path.Combine(LOGS_DIRECTORY, DateTime.Now.ToString("yyyy_MM_dd_HH_mm"));

            using (StreamWriter writer = File.AppendText(fileName))
            {
                await writer.WriteAsync(
                    string.Format(
                        MESSAGE_FORMAT,
                        severity,
                        DateTime.Now.ToString("yyyy_MM_dd_HH_mm"),
                        category,
                        message));
            }
        }
    }
}
