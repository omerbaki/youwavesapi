using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Framework;
using LevYamWaveAnalyzer;
using WaveAnalyzerCommon;
using System.Reactive.Linq;

namespace ReportsCreator
{
    public interface IReportsCreator
    {
        Task CreateReports();        
    }

    class ReportsCreator : IReportsCreator
    {
        private readonly ILogger mLogger; 
        private readonly IJsonSerializer mJsonSerializer;
        private readonly IEnumerable<IReportCreator> mReportCreators;
        private readonly IStorageAccessor mStorageAccessor;

        public ReportsCreator(
            ILogger aLogger, 
            IEnumerable<IReportCreator> aReportCreators,
            IJsonSerializer aJsonSerializer,
            IStorageAccessor aStorageAccessor)
        {
            mLogger = aLogger;
            mReportCreators = aReportCreators;
            mJsonSerializer = aJsonSerializer;
            mStorageAccessor = aStorageAccessor;
        }

        public async Task CreateReports()
        {
            foreach (var reportCreator in mReportCreators)
            {
                Exception exThrown = null;
                try
                {
                    if (!reportCreator.ShouldRun()) continue;

                    await mLogger.Debug(typeof(ReportsCreator).Name, "Create Report " + reportCreator.GetType().Name);

                    var baseReportModel = await reportCreator.Create();
                    
                    await mStorageAccessor.WriteReport(baseReportModel);
                }
                catch (Exception ex)
                {
                    mLogger.Error(
                           "ForecastAnalysisReportCreator",
                           "Failed to run analyzer " + reportCreator.GetType().Name,
                           ex);                                      
                }
            }
        }
    }
}
