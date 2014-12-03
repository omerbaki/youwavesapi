using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Framework;
using LevYamWaveAnalyzer;
using WaveAnalyzerCommon;
using System.Reactive.Linq;
using Logger;

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
        private readonly IStorageAccessor mStorageAccessor;

        public ReportsCreator(
            ILogger aLogger, 
            IEnumerable<IReportCreator> aReportCreators,
            IStorageAccessor aStorageAccessor)
        {
            mLogger = aLogger;
            mReportCreators = aReportCreators;
            mStorageAccessor = aStorageAccessor;
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

                var createdReport = await reportCreator.Create();

                await mStorageAccessor.WriteReport(createdReport);
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
