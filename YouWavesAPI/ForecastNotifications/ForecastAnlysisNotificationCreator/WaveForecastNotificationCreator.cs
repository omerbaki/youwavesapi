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
        Task<string> CreateNotifications(string reportsDirectory);
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

        public async Task<string> CreateNotifications(string reportsDirectory)
        {
            string notificationsDirectory = CreateDailyNotificationsDirectory();

            Exception exThrown = null;
            try
            {
                string processedDirectory = reportsDirectory + "_Processed";

                string[] analysisResults = Directory.GetFiles(reportsDirectory);
                if (analysisResults.Length == 0) return notificationsDirectory;

                foreach (var analysisResult in analysisResults)
                {
                    var notificationCreator = GetWaveForecastNotificationCreator(analysisResult);
                    await notificationCreator.UpdateWaveForecastNotification(reportsDirectory, notificationsDirectory);

                    string processedFilePath = Path.Combine(processedDirectory, analysisResult);
                    File.Move(analysisResult, processedFilePath);
                }              
            }
            catch(Exception ex)
            {
                exThrown = ex;                
            }

            if (exThrown != null)
            {
                await mLogger.Error("ForecastNotificationCreator", "Failed to create notifications", exThrown);
                exThrown = null;
            }

            return notificationsDirectory;
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
