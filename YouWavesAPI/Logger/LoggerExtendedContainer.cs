using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class LoggerExtendedContainer : UnityContainerExtension
    {
        protected override void Initialize()
        {
//            Container.RegisterType<ILogWriter, FileLogWriter>("FileLogWriter");
//            Container.RegisterType<ILogWriter, MailLogWriter>("MailLogWriter");
            Container.RegisterType<ILogger, Logger>();
            
        }        
    }
}
