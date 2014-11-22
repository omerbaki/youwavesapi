using ForecastAnalysisNotificationCreator;
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
        private readonly IForecastNotificationCreator mForecastNotificationCreator;

        public ForecastAnalysisController(
            ILogger aLogger, 
            IForecastAnalysisReportCreator forecastAnalysisReportCreator,
            IForecastNotificationCreator aForecastNotificationCreator)
        {
            mLogger = aLogger;
            mForecastAnalysisReportCreator = forecastAnalysisReportCreator;
            mForecastNotificationCreator = aForecastNotificationCreator;
        }

        // POST: api/ForecastAnalysis
        public async Task Post()
        {
            await mLogger.Debug("ForecastAnalysisController", "Create Reports");
            string reportsDirectory = await mForecastAnalysisReportCreator.CreateReports();

            await mLogger.Debug("ForecastAnalysisController", "Create Notification");
            await mForecastNotificationCreator.CreateNotifications(reportsDirectory);
        }        
    }
}
