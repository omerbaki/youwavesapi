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
        Task CreateNotification(string reportsDirectory);
    }

    public class ForecastNotificationCreator : IForecastNotificationCreator
    {
        private readonly ILogger mLogger; 
        private IJsonSerializer mJsonSerializer;
        private readonly IEnumerable<IWaveForecastNotificationCreator> mWaveForecastNotificationCreators;

        public ForecastNotificationCreator(
            ILogger aLogger,
            IJsonSerializer jsonSerializer,
            IEnumerable<IWaveForecastNotificationCreator> waveForecastNotificationCreators)
        {
            mLogger = aLogger;            
            mJsonSerializer = jsonSerializer;
            mWaveForecastNotificationCreators = waveForecastNotificationCreators;
        }

        public async Task CreateNotification(string reportsDirectory)
        {
            string notificationsDirectory = CreateNotificationsDirectory();

            await CreateWavesForecastNotification(reportsDirectory, notificationsDirectory);
        }

        private async Task CreateWavesForecastNotification(string reportsDirectory, string notificationsDirectory)
        {  
            var waveForecastNotification = new WaveForecastNotificationModel();

            foreach (var waveForecastNotificationCreator in mWaveForecastNotificationCreators)
            {
                await waveForecastNotificationCreator.UpdateWaveForecastNotification(
                        reportsDirectory, 
                        waveForecastNotification);               
            }

            string reportFileName = CreateWaveForecastNotificationFileName(notificationsDirectory);                  

            await mJsonSerializer.Export(reportFileName, waveForecastNotification);
        }

        private string CreateWaveForecastNotificationFileName(string notificationsDirectory)
        {
            return Path.Combine(
                        notificationsDirectory,
                        typeof(WaveForecastNotificationModel).Name + "_" + DateTime.Now.ToString("yyyyMMdd") + ".json");
        }

        private string CreateNotificationsDirectory()
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
