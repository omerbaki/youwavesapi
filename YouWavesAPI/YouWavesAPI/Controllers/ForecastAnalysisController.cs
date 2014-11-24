using ForecastAnalysisNotificationCreator;
using ForecastAnalysisReport;
using ForecastNotificationSender;
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
        private readonly IForecastNotificationSender mForecastNotificationSender;

        public ForecastAnalysisController(
            ILogger aLogger, 
            IForecastAnalysisReportCreator forecastAnalysisReportCreator,
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
            string reportsDirectory = await mForecastAnalysisReportCreator.CreateReports();

            await mLogger.Debug("ForecastAnalysisController", "Create Notification");
            string notificationsDirectory = await mForecastNotificationCreator.CreateNotifications(reportsDirectory);

            await mLogger.Debug("ForecastAnalysisController", "Create Notification");
            await mForecastNotificationSender.SendNotifications(notificationsDirectory);
        }        
    }
}
