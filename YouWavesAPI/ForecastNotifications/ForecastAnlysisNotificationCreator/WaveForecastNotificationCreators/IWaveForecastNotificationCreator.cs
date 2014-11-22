using ForecastNotificaitonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisNotificationCreator.WaveForecastNotificationCreators
{
    public interface IWaveForecastNotificationCreator
    {
        Task UpdateWaveForecastNotification(
            string reportsDirectory,
            string notificationsDirectory);
    }
}
