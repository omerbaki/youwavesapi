using ForecastNotificaitonEntities;
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
        private readonly IEmailSender mEmailSender;

        public ForecastNotificationSender(
            ILogger aLogger,
            IJsonSerializer jsonSerializer,
            IForecastNotificationFormatterFactory aForecastNotificationFormatterFactory,
            IEmailSender aEmailSender)
        {
            mLogger = aLogger;            
            mJsonSerializer = jsonSerializer;
            mForecastNotificationFormatterFactory = aForecastNotificationFormatterFactory;
            mEmailSender = aEmailSender;
        }

        public async Task SendNotifications(string notificationsDirectory)
        {
            Exception exThrown = null;
            try
            {
                string processedDirectory = CreateProcessedDirectory(notificationsDirectory);

                string[] notificationFilePaths = Directory.GetFiles(notificationsDirectory);
                if (notificationFilePaths.Length == 0) return;

                foreach (var notificationFilePath in notificationFilePaths)
                {
                    var notificationFormatter = GetForecastNotificationFormatterCreator(notificationFilePath);
                    await mEmailSender.Send(notificationFormatter);

                    string processedFilePath = Path.Combine(processedDirectory, notificationFilePath);
                    File.Move(notificationFilePath, processedFilePath);
                }
            }
            catch (Exception ex)
            {
                exThrown = ex;
            }

            if (exThrown != null)
            {
                await mLogger.Error("ForecastNotificationCreator", "Failed to create notifications", exThrown);
                exThrown = null;
            }
        }

        private string CreateProcessedDirectory(string notificationsDirectory)
        {
            string processedDirectory = notificationsDirectory + "_Processed";
            if(!Directory.Exists(processedDirectory))
            {
                Directory.CreateDirectory(processedDirectory);
            }

            return processedDirectory;
        }

        private IForecastNotificationFormatter GetForecastNotificationFormatterCreator(string analysisResult)
        {
            return mForecastNotificationFormatterFactory.Create(typeof(WaveForecastNotificationModel).Name);
        }
    }
}
