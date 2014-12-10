using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Framework;
using LevYamWaveAnalyzer;
using WaveAnalyzerCommon;
using System.Reactive.Linq;
using LoggerFramework;
using ForecastAnalysisModel;

namespace ForecastAnalysisReportCreator
{
    public interface IReportsCreator
    {
        Task CreateReports();        
    }

    public class ReportsCreator : IReportsCreator
    {
        private readonly ILogger mLogger; 
        private readonly IEnumerable<IReportCreator> mReportCreators;        

        public ReportsCreator(
            ILogger aLogger, 
            IEnumerable<IReportCreator> aReportCreators)
        {
            mLogger = aLogger;
            mReportCreators = aReportCreators;            
        }

        public async Task CreateReports()
        {
            foreach (var reportCreator in mReportCreators)
            {
                if (!reportCreator.ShouldRun(DateTime.Now)) continue;

                await CreateReport(reportCreator);
            }
        }

        private async Task CreateReport(IReportCreator reportCreator)
        {
            Exception exThrown = null;
            try
            {
                await mLogger.Debug(typeof(ReportsCreator).Name, "Create Report " + reportCreator.GetType().Name);
                await reportCreator.Create();                
            }
            catch (Exception ex)
            {
                exThrown = ex;
            }

            if (exThrown != null)
            {
                await mLogger.Error(
                   typeof(ReportsCreator).Name,
                   "Failed to create report " + reportCreator.GetType().Name,
                   exThrown);

                exThrown = null;
            }
        }
    }
}
