using ForecastAnalysisEntities;
using ForecastNotificaitonEntities;
using Framework;
using LoggerFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisNotificationCreator.WaveForecastNotificationCreators
{
    class WaveForecastComparisonNotificationCreator : INotificationCreator
    {
        private readonly IStorageAccessor<WaveForecastReportModel> mWavesForecastStorageAccessor;

        public WaveForecastComparisonNotificationCreator(IStorageAccessor<WaveForecastReportModel> aWavesForecastStorageAccessor)
        {
            mWavesForecastStorageAccessor = aWavesForecastStorageAccessor;
        }

        public async Task Create()
        {
            var waveForecastComparisonModel = new WaveForecastComparisonNotificationModel();

            var waveForecastReportModels = await mWavesForecastStorageAccessor.ReadAllReports(DateTime.Today);
            foreach(var waveForecastReportModel in waveForecastReportModels)
            {
                waveForecastComparisonModel.AddForecastResource(waveForecastReportModel);
            }
        }
    }
}
