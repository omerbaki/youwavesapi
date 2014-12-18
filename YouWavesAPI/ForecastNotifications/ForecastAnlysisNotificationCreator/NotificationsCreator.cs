using ForecastAnalysisNotificationCreator.WaveForecastNotificationCreators;
using ForecastAnalysisEntities;
using ForecastNotificaitonEntities;
using Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerFramework;

namespace ForecastAnalysisNotificationCreator
{
    public interface INotificationsCreator
    {
        Task CreateNotifications();
    }

    public class NotificationsCreator : INotificationsCreator
    {
        private readonly ILogger mLogger;
        private readonly IEnumerable<INotificationCreator> mNotificationCreators; 

        public NotificationsCreator(
            ILogger aLogger,
            IEnumerable<INotificationCreator> aNotificationCreators)
        {
            mLogger = aLogger;
            mNotificationCreators = aNotificationCreators;
        }

        public async Task CreateNotifications()
        {
            foreach (var notificationCreator in mNotificationCreators)
            {
                Exception thrownEx = null;

                try
                {
                    await notificationCreator.Create();
                }
                catch (Exception ex)
                {
                    thrownEx = ex;
                }

                if (thrownEx != null)
                {
                    await mLogger.Error(notificationCreator.GetType().Name, "Failed to create notification", thrownEx);
                    thrownEx = null;
                }

            }              
        }
    }
}
