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
        private readonly IEnumerable<INotificationCreator> mNotificationCreators; 

        public NotificationsCreator(
            IEnumerable<INotificationCreator> aNotificationCreators)
        {           
            mNotificationCreators = aNotificationCreators;
        }

        public async Task CreateNotifications()
        {
            foreach (var notificationCreator in mNotificationCreators)
            {
                await notificationCreator.Create();
            }              
        }
    }
}
