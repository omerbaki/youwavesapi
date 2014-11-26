using ForecastAnalysisNotificationCreator.WaveForecastNotificationCreators;
using ForecastAnalysisModel;
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
                string processedDirectory = CreateProcessedDirectory(reportsDirectory);

                string[] analysisModels = Directory.GetFiles(reportsDirectory);
                if (analysisModels.Length == 0) return notificationsDirectory;

                foreach (var analysisModel in analysisModels)
                {
                    var notificationCreator = GetWaveForecastNotificationCreator(analysisModel);
                    await notificationCreator.UpdateWaveForecastNotification(reportsDirectory, notificationsDirectory);

                    string processedFilePath = Path.Combine(processedDirectory, new FileInfo(analysisModel).Name);
                    File.Move(analysisModel, processedFilePath);
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

        private string CreateProcessedDirectory(string reportsDirectory)
        {
            string processedDirectory = reportsDirectory + "_Processed";
            if (!Directory.Exists(processedDirectory))
            {
                Directory.CreateDirectory(processedDirectory);
            }

            return processedDirectory;
        }

        private IWaveForecastNotificationCreator GetWaveForecastNotificationCreator(string analysisModel)
        {
            return mWaveForecastNotificationCreatorFactory.Create(typeof(IsramarWaveAnalysisModel).Name);           
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
