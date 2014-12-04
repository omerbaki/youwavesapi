using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerFramework
{
    public class MailLogWriter : ILogWriter
    {
        public Task Log(string severity, string category, string message)
        {
            throw new NotImplementedException();
        }
    }
}
