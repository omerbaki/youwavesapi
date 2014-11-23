using ForecastNotificationSender.ForecastNotificationFormatters;
using Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastNotificationSender
{
    public interface IForecastNotificationSender
    {
        Task SendNotifications(string notificationsDirectory);
    }

    class ForecastNotificationSender : IForecastNotificationSender 
    {
        private readonly ILogger mLogger;
        private readonly IJsonSerializer mJsonSerializer;
        private readonly IForecastNotificationFormatterFactory mForecastNotificationFormatterFactory;

        public ForecastNotificationSender(
            ILogger aLogger,
            IJsonSerializer jsonSerializer,
            IForecastNotificationFormatterFactory aForecastNotificationFormatterFactory)
        {
            mLogger = aLogger;            
            mJsonSerializer = jsonSerializer;
            mForecastNotificationFormatterFactory = aForecastNotificationFormatterFactory;
        }

        public async Task SendNotifications(string notificationsDirectory)
        {
            Exception exThrown = null;
            try
            {
                string processedDirectory = notificationsDirectory + "_Processed";

                string[] analysisResults = Directory.GetFiles(reportsDirectory);
                if (analysisResults.Length == 0) return;

                foreach (var analysisResult in analysisResults)
                {
                    var notificationCreator = GetWaveForecastNotificationCreator(analysisResult);
                    await notificationCreator.UpdateWaveForecastNotification(reportsDirectory, notificationsDirectory);

                    File.Move(analysisResult, processedDirectory);
                }
            }
            catch (Exception ex)
            {
                exThrown = ex;
            }

            await mLogger.Error("ForecastNotificationCreator", "Failed to create notifications", exThrown);
        }
    }
}
