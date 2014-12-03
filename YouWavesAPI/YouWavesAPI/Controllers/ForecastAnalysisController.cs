using ForecastAnalysisNotificationCreator;
using ForecastAnalysisReportCreator;
using ForecastNotificationSender;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Logger;

namespace YouWavesAPI.Controllers
{
    public class ForecastAnalysisController : ApiController
    {
        private readonly ILogger mLogger; 
        private readonly IReportsCreator mForecastAnalysisReportCreator;
        private readonly IForecastNotificationCreator mForecastNotificationCreator;
        private readonly IForecastNotificationSender mForecastNotificationSender;

        public ForecastAnalysisController(
            ILogger aLogger, 
            IReportsCreator forecastAnalysisReportCreator,
            IForecastNotificationCreator aForecastNotificationCreator,
            IForecastNotificationSender aForecastNotificationSender)
        {
            mLogger = aLogger;
            mForecastAnalysisReportCreator = forecastAnalysisReportCreator;
            mForecastNotificationCreator = aForecastNotificationCreator;
            mForecastNotificationSender = aForecastNotificationSender;
        }

        // POST: api/ForecastAnalysis
        public async Task Post()
        {
            await mLogger.Debug("ForecastAnalysisController", "Create Reports");
            await mForecastAnalysisReportCreator.CreateReports();

//            await mLogger.Debug("ForecastAnalysisController", "Create Notification");
//            await mForecastNotificationCreator.CreateNotifications();

//            await mLogger.Debug("ForecastAnalysisController", "Create Notification");
//            await mForecastNotificationSender.SendNotifications();
        }        
    }
}
