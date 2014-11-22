using ForecastAnalysisResultEntities;
using ForecastNotificaitonEntities;
using Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisNotificationCreator.WaveForecastNotificationCreators
{
    class IsramarWaveForecastNotificationCreator: IWaveForecastNotificationCreator
    {
        private IJsonSerializer mJsonSerializer;

        public IsramarWaveForecastNotificationCreator(IJsonSerializer jsonSerializer)
        {
            mJsonSerializer = jsonSerializer;
        }

        public async Task UpdateWaveForecastNotification(string reportsDirectory, string notificationsDirectory)
        {
            string reportFileName = CreateWaveForecastNotificationFileName(notificationsDirectory);
            var waveForecastNotification = await GetWaveForecastNotificationModel(reportFileName, notificationsDirectory);

            string[] analysisResults = Directory.GetFiles(reportsDirectory, typeof(IsramarWaveAnalysisResult).Name + "*");
            if (analysisResults.Length == 0) return;

            var isramarWaveAnalysisResult =
                (await mJsonSerializer.Import(analysisResults[0], typeof(IsramarWaveAnalysisResult)))
                as IsramarWaveAnalysisResult;

            waveForecastNotification.IsramarStartDate = isramarWaveAnalysisResult.StartDate;
            waveForecastNotification.IsramarEndDate = isramarWaveAnalysisResult.EndDate;

            await mJsonSerializer.Export(reportFileName, waveForecastNotification);
        }

        private string CreateWaveForecastNotificationFileName(string notificationsDirectory)
        {
            return Path.Combine(
                        notificationsDirectory,
                        typeof(WaveForecastNotificationModel).Name + "_" + DateTime.Now.ToString("yyyyMMdd") + ".json");
        }

        private async Task<WaveForecastNotificationModel> GetWaveForecastNotificationModel(string reportFileName, string notificationsDirectory)
        {
            var isramarWaveAnalysisResult =
                (await mJsonSerializer.Import(reportFileName, typeof(WaveForecastNotificationModel)))
                as WaveForecastNotificationModel;

            if(isramarWaveAnalysisResult == null)
            {
                isramarWaveAnalysisResult = new WaveForecastNotificationModel();
            }

            return isramarWaveAnalysisResult;
        }      
    }
}
