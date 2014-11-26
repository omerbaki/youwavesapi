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

        protected override WaveAnalysisModel CreateWaveAnalysisModel()
        {
            var isramarWaveAnalysisModel = new IsramarWaveAnalysisModel();
            isramarWaveAnalysisModel.ForecastStartDate = DateTime.Today.AddDays(1);
            isramarWaveAnalysisModel.ForecastEndDate = DateTime.Today.AddDays(4);
            return isramarWaveAnalysisModel;
        }

        public override bool ShouldRun()
        {
            bool alreadyRanToday = mLastRunTime.Date == DateTime.Today;
            return (DateTime.Now.Hour == 8) && !alreadyRanToday;
        }

    }
}
