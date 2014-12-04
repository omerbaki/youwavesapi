using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerFramework
{
    public class FileLogWriter : ILogWriter
    {
        private const string LOGS_DIRECTORY = "Logs";
        private const string MESSAGE_FORMAT = "[{0}] [{1}] - [{2}] - {3}";

        public FileLogWriter()
        {
            if (!Directory.Exists(LOGS_DIRECTORY))
            {
                Directory.CreateDirectory(LOGS_DIRECTORY);
            }
        }

        public async Task Log(string severity, string category, string message)
        {
            string fileName = Path.Combine(LOGS_DIRECTORY, DateTime.Now.ToString("yyyy_MM_dd_HH"));

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
