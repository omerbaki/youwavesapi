using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerFramework
{
    public interface ILogWriter
    {
        Task Log(string severity, string category, string message);
    }
}
