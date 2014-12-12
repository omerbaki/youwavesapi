using ForecastAnalysisEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WaveAnalyzerCommon;
using Framework;

namespace IsramarWaveAnalyzer
{
    public class IsramarWaveForecastReportCreator : WaveForecastReportCreatorBase
    {
        private const string ISRAMAR_IMAGE_NAME = "isramar.{0}.gif";
        private const string ISRAMAR_IMAGE_URL = "http://isramar.ocean.org.il/isramar2009/wave_model/wave_maps/wam/{0}/coarse/{1}.windir.gif";

        private readonly IImageDownloader mImageDownloader;
        private readonly IImageAnalyzer mImageAnalyzer;


        public IsramarWaveForecastReportCreator(
            IImageDownloader imageDownloader,
            IImageAnalyzer imageAnalyzer, 
            IStorageAccessor<WaveForecastReportModel> aStorageAccessor) : base(aStorageAccessor)
        {
            mImageDownloader = imageDownloader;
            mImageAnalyzer = imageAnalyzer;
        }

        public override bool ShouldRun(DateTime now)
        {
            bool alreadyRanToday = mLastRunTime.Date == now.Date;
            return (now.Hour == 8) && !alreadyRanToday;
        }

        protected override void SetForecastTimeFrame(WaveForecastReportModel model)
        {
            model.ForecastStartDate = DateTime.Today.AddDays(1);
            model.ForecastEndDate = DateTime.Today.AddDays(5);            
        }

        protected override DateTime[] GetForecastDates(WaveForecastReportModel model)
        {
            var dates = new List<DateTime>();

            var forecastDurationDays =
                (int)(model.ForecastEndDate - model.ForecastStartDate).TotalDays;

            DateTime currentDate = model.ForecastStartDate;
            for (int i = 0; i < forecastDurationDays * 8; i++)
            {
                dates.Add(currentDate);           
                currentDate = currentDate.AddHours(3);
            }

            return dates.ToArray();
        }

        protected override async Task<WaveHeight> GetWaveHeight(DateTime forecastDate)
        {
            var downloadImageModel = CreateDownloadImageModel(forecastDate);
            var imageBytes = await mImageDownloader.DownloadImage(downloadImageModel);
            return mImageAnalyzer.GetWaveHeight(imageBytes);            
        }

        private DownloadImageModel CreateDownloadImageModel(DateTime forecastDate)
        {
            // Today format for URL - 1411110000
            string todayFormat = DateTime.Today.ToString("yyMMddHHmm");
            
            // Image date format - 14111112
            string imageDateFormat = forecastDate.ToString("yyMMddHH");

            return 
                new DownloadImageModel()
                {
                    ImageUrl = string.Format(ISRAMAR_IMAGE_URL, todayFormat, imageDateFormat),
                    ImageName = string.Format(ISRAMAR_IMAGE_NAME, imageDateFormat)
                };
        }
    }
}
