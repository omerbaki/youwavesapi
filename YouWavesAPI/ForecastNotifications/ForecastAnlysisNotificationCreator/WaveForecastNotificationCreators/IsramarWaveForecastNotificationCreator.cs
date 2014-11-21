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

        public async Task UpdateWaveForecastNotification(string reportsDirectory, WaveForecastNotificationModel waveForecastNotification)
        {
            string[] analysisResults = Directory.GetFiles(reportsDirectory, typeof(IsramarWaveAnalysisResult).Name + "*");
            if (analysisResults.Length == 0) return;

            var isramarWaveAnalysisResult =
                (await mJsonSerializer.Import(analysisResults[0], typeof(IsramarWaveAnalysisResult)))
                as IsramarWaveAnalysisResult;

            waveForecastNotification.IsramarStartDate = isramarWaveAnalysisResult.StartDate;
            waveForecastNotification.IsramarEndDate = isramarWaveAnalysisResult.EndDate;
        }
    }
}
