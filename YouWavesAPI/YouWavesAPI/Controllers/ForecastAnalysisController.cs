﻿using ForecastAnalysisNotificationCreator;
using ForecastAnalysisReportCreator;
using LoggerFramework;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace YouWavesAPI.Controllers
{
    public class ForecastAnalysisController : ApiController
    {
        private readonly ILogger mLogger; 
        private readonly IReportsCreator mForecastAnalysisReportCreator;
        private readonly INotificationsCreator mForecastNotificationCreator;
        //private readonly IForecastNotificationSender mForecastNotificationSender;

        public ForecastAnalysisController(
            ILogger aLogger,
            IReportsCreator forecastAnalysisReportCreator,
            INotificationsCreator aForecastNotificationCreator)
//            IForecastNotificationSender aForecastNotificationSender)
        {
            mLogger = aLogger;
            mForecastAnalysisReportCreator = forecastAnalysisReportCreator;
            mForecastNotificationCreator = aForecastNotificationCreator;
//            mForecastNotificationSender = aForecastNotificationSender;
        }

        // POST: api/ForecastAnalysis
        public async Task Post()
        {
            await mLogger.Debug("ForecastAnalysisController", "Create Reports");
            await mForecastAnalysisReportCreator.CreateReports();

            await mLogger.Debug("ForecastAnalysisController", "Create Notification");
            await mForecastNotificationCreator.CreateNotifications();

//            await mLogger.Debug("ForecastAnalysisController", "Create Notification");
//            await mForecastNotificationSender.SendNotifications();
        }        
    }
}
