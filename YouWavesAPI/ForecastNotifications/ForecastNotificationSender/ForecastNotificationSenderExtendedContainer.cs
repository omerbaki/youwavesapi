using ForecastNotificaitonEntities;
using ForecastNotificationSender.ForecastNotificationFormatters;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastNotificationSender
{
    public class ForecastNotificationSenderExtendedContainer : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IForecastNotificationFormatterFactory, ForecastNotificationFormatterFactory>();
            Container.RegisterType<IForecastNotificationSender, ForecastNotificationSender>();
            Container.RegisterType<IEmailSender, EmailSender>();

            Container.RegisterType<IForecastNotificationFormatter, WaveForecastNotificationFormatter>(typeof(WaveForecastNotificationModel).Name);
        }
    }    
}
