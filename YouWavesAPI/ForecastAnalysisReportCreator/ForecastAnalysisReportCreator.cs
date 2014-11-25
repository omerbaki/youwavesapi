using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Framework;
using LevYamWaveAnalyzer;
using WaveAnalyzerCommon;
using System.Reactive.Linq;

namespace ForecastAnalysisReport
{
    public interface IForecastAnalysisReportCreator
    {
        Task<string> CreateReports();        
    }

    class ForecastAnalysisReportCreator : IForecastAnalysisReportCreator
    {
        private readonly ILogger mLogger; 
        private readonly IJsonSerializer mJsonSerializer;
        private readonly IEnumerable<IWaveAnalyzer> mWaveAnalyzers;

        public ForecastAnalysisReportCreator(
            ILogger aLogger, 
            IEnumerable<IWaveAnalyzer> aWaveAnalyzers,
            IJsonSerializer aJsonSerializer)
        {
            mLogger = aLogger;
            mWaveAnalyzers = aWaveAnalyzers;
            mJsonSerializer = aJsonSerializer;
        }

        public async Task<string> CreateReports()
        {
            string directory = CreateReportDirectory();

            foreach (var waveAnalyzer in mWaveAnalyzers)
            {
                Exception exThrown = null;
                try
                {
                    if (!waveAnalyzer.ShouldRun()) continue;

                    await mLogger.Debug("ForecastAnalysisReportCreator", "Running waveAnalyzer " + waveAnalyzer.GetType().Name);

                    var waveAnalysisModel = await waveAnalyzer.Analyze();

                    string reportFileName =
                        Path.Combine(directory, waveAnalysisModel.GetType().Name + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".json");

                    await mJsonSerializer.Export(reportFileName, waveAnalysisModel);
                }
                catch (Exception ex)
                {
                    exThrown = ex;                                       
                }

                if (exThrown != null)
                {
                    await mLogger.Error(
                            "ForecastAnalysisReportCreator",
                            "Failed to run analyzer " + waveAnalyzer.GetType().Name,
                            exThrown);

                    exThrown = null;
                }
            }

            return directory;
        }

        private string CreateReportDirectory()
        {
            string directory = Path.Combine("Reports", DateTime.Now.ToString("yyyyMMdd_HHmm"));

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }


        
    }
}
