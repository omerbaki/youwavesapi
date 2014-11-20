using ForecastAnalysisReport;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace YouWavesAPI.Controllers
{
    public class ForecastAnalysisController : ApiController
    {
        private readonly ILogger mLogger; 
        private readonly IForecastAnalysisReportCreator mForecastAnalysisReportCreator;

        public ForecastAnalysisController(
            ILogger aLogger, 
            IForecastAnalysisReportCreator forecastAnalysisReportCreator)
        {
            mLogger = aLogger;
            mForecastAnalysisReportCreator = forecastAnalysisReportCreator;
        }

        // POST: api/ForecastAnalysis
        public async Task Post()
        {
            await mLogger.Debug("ForecastAnalysisController", "Create Reports");
            await mForecastAnalysisReportCreator.CreateReports();

            await mLogger.Debug("ForecastAnalysisController", "Create Notification");
            //await mForecastAnalysisReportCreator.CreateReports();
        }        
    }
}
