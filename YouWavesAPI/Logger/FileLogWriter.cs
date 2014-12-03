using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class FileLogWriter : ILogWriter
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

        public Task Log(string severity, string category, string message)
        {
            throw new NotImplementedException();
        }
    }
}
