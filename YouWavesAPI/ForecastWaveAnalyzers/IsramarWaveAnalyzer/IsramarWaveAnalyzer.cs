using ForecastAnalysisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveAnalyzerCommon;
using WaveAnalyzerCommon.Model;

namespace IsramarWaveAnalyzer
{
    internal class IsramarWaveAnalyzer : WaveAnalyzerBase
    {
        public IsramarWaveAnalyzer(
            IImageDownloader imageDownloader,
            IImageAnalyzer imageAnalyzer)
            : base(imageDownloader, imageAnalyzer)
        {
        }

        protected override void SetForecastEndDate(WaveForecastReportModel waveForecastReportModel)
        {
            waveForecastReportModel.ForecastEndDate = waveForecastReportModel.ForecastStartDate.AddDays(5);            
        }

        public override bool ShouldRun()
        {
            bool alreadyRanToday = mLastRunTime.Date == DateTime.Today;
            return (DateTime.Now.Hour == 8) && !alreadyRanToday;
        }

    }
}
