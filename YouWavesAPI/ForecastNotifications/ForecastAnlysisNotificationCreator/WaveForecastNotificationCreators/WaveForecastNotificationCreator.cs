using ForecastAnalysisEntities;
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
    public interface IWaveForecastNotificationCreator
    {
        Task UpdateWaveForecastNotification(
            string reportsDirectory,
            string notificationsDirectory);
    }

    class WaveForecastNotificationCreator: IWaveForecastNotificationCreator
    {
        private IJsonSerializer mJsonSerializer;

        public WaveForecastNotificationCreator(IJsonSerializer jsonSerializer)
        {
            mJsonSerializer = jsonSerializer;
        }

        public async Task CreateNotification(string reportsDirectory, string notificationsDirectory)
        {
//            string reportFileName = CreateWaveForecastNotificationFileName(notificationsDirectory);
//            var waveForecastNotification = await GetWaveForecastNotificationModel(reportFileName, notificationsDirectory);
//
//            string[] analysisModels = Directory.GetFiles(reportsDirectory, typeof(IsramarWaveAnalysisModel).Name + "*");
//            if (analysisModels.Length == 0) return;
//
//            var isramarWaveAnalysisModel =
//                (await mJsonSerializer.Import(analysisModels[0], typeof(IsramarWaveAnalysisModel)))
//                as IsramarWaveAnalysisModel;
//
//            waveForecastNotification.IsramarForecastStartDate = isramarWaveAnalysisModel.ForecastStartDate;
//            waveForecastNotification.IsramarForecastEndDate = isramarWaveAnalysisModel.ForecastEndDate;
//            waveForecastNotification.IsramarWavesStartDate = isramarWaveAnalysisModel.WavesStartAt;
//            waveForecastNotification.IsramarWavesEndDate = isramarWaveAnalysisModel.WavesEndAt;
//           
//
//            await mJsonSerializer.Export(reportFileName, waveForecastNotification);
        }

        private string CreateWaveForecastNotificationFileName(string notificationsDirectory)
        {
            return Path.Combine(
                        notificationsDirectory,
                        typeof(WaveForecastNotificationModel).Name + "_" + DateTime.Now.ToString("yyyyMMdd") + ".json");
        }

        private async Task<WaveForecastNotificationModel> GetWaveForecastNotificationModel(string reportFileName, string notificationsDirectory)
        {
            if (!File.Exists(reportFileName))
            {
                return new WaveForecastNotificationModel();
            }

            var isramarWaveAnalysisModel =
                (await mJsonSerializer.Import(reportFileName, typeof(WaveForecastNotificationModel)))
                as WaveForecastNotificationModel;          

            return isramarWaveAnalysisModel;
        }      
    }
}
