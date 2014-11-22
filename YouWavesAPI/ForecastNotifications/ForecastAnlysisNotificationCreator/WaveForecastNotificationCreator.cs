using ForecastAnalysisNotificationCreator.WaveForecastNotificationCreators;
using ForecastAnalysisResultEntities;
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

namespace ForecastAnalysisNotificationCreator
{
    public interface IForecastNotificationCreator
    {
        Task CreateNotifications(string reportsDirectory);
    }

    public class ForecastNotificationCreator : IForecastNotificationCreator
    {
        private readonly ILogger mLogger; 
        private readonly IJsonSerializer mJsonSerializer;
        private readonly IWaveForecastNotificationCreatorFactory mWaveForecastNotificationCreatorFactory; 

        public ForecastNotificationCreator(
            ILogger aLogger,
            IJsonSerializer jsonSerializer,
            IWaveForecastNotificationCreatorFactory aWaveForecastNotificationCreatorFactory)
        {
            mLogger = aLogger;            
            mJsonSerializer = jsonSerializer;
            mWaveForecastNotificationCreatorFactory = aWaveForecastNotificationCreatorFactory;
        }

        public async Task CreateNotifications(string reportsDirectory)
        {
            Exception exThrown = null;
            try
            {
                string[] analysisResults = Directory.GetFiles(reportsDirectory);
                if (analysisResults.Length == 0) return;

                string notificationsDirectory = CreateDailyNotificationsDirectory();

                foreach (var analysisResult in analysisResults)
                {
                    var notificationCreator = GetWaveForecastNotificationCreator(analysisResult);
                    await notificationCreator.UpdateWaveForecastNotification(notificationsDirectory);
                }              
            }
            catch(Exception ex)
            {
                exThrown = ex;                
            }

            await mLogger.Error("ForecastNotificationCreator", "Failed to create notifications", exThrown);
        }        

        private IWaveForecastNotificationCreator GetWaveForecastNotificationCreator(string analysisResult)
        {
            return mWaveForecastNotificationCreatorFactory.Create(typeof(IsramarWaveAnalysisResult).Name);           
        }

        private string CreateDailyNotificationsDirectory()
        {
            string directory = Path.Combine("Notifications", DateTime.Now.ToString("yyyyMMdd"));

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }


      
    }
}
