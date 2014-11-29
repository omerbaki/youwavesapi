using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ILogger
    {
        Task Debug(string categry, string message);
        Task Error(string categry, string message);
        Task Error(string categry, string message, Exception ex);
    }

    class Logger : ILogger
    {
        private readonly IEnumerable<ILogWriter> mLogWriters;

        public Logger(IEnumerable<ILogWriter> logWriters)
        {
            mLogWriters = logWriters;
        }

        public async Task Debug(string categry, string message)
        {
            await Log("D", categry, message);
        }

        public async Task Error(string categry, string message)
        {
            await Log("E", categry, message);
        }

        public async Task Error(string categry, string message, Exception ex)
        {
            message += "\r\n" + ex.ToString();

            await Log("E", categry, message);
        }

        private async Task Log(string severity, string category, string message)
        {
            var logWriterTasks = new List<Task>();
            foreach(ILogWriter logWriter in mLogWriters)
            {
                logWriterTasks.Add(logWriter.Log(severity, category, message));
            }

            try
            {
                await Task.WhenAll(logWriterTasks);
            }
            catch { }
        }
    }
}
