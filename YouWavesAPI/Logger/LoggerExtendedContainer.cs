using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerFramework
{
    public class LoggerExtendedContainer : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<ILogWriter, FileLogWriter>("FileLogWriter");
            //Container.RegisterType<ILogWriter, MailLogWriter>("MailLogWriter");
            Container.RegisterType<IEnumerable<ILogWriter>, ILogWriter[]>();
            Container.RegisterType<ILogger, Logger>();
            
        }        
    }
}
