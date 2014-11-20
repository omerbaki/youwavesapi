using ForecastAnalysisReport;
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
        private readonly IForecastAnalysisReportCreator mForecastAnalysisReportCreator;

        public ForecastAnalysisController(IForecastAnalysisReportCreator forecastAnalysisReportCreator)
        {
            mForecastAnalysisReportCreator = forecastAnalysisReportCreator;
        }

        // POST: api/ForecastAnalysis
        public async Task Post()
        {
            await mForecastAnalysisReportCreator.CreateReports();
        }        
    }
}
