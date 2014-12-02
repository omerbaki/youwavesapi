using ForecastAnalysisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveAnalyzerCommon;
using WaveAnalyzerCommon.Model;

namespace IsramarWaveAnalyzer
{
    internal class IsramarWaveAnalyzer : WaveForecastReportCreatorBase
    {
        private const string ISRAMAR_IMAGE_NAME = "isramar.{0}.gif";
        private const string ISRAMAR_IMAGE_URL = "http://isramar.ocean.org.il/isramar2009/wave_model/wave_maps/wam/{0}/coarse/{1}.windir.gif";

        private readonly IImageDownloader mImageDownloader;
        private readonly IImageAnalyzer mImageAnalyzer;


        public IsramarWaveAnalyzer(
            IImageDownloader imageDownloader,
            IImageAnalyzer imageAnalyzer)
        {
            mImageDownloader = imageDownloader;
            mImageAnalyzer = imageAnalyzer;
        }

        public override bool ShouldRun()
        {
            bool alreadyRanToday = mLastRunTime.Date == DateTime.Today;
            return (DateTime.Now.Hour == 8) && !alreadyRanToday;
        }

        protected override void SetForecastTimeFrame(WaveForecastReportModel model)
        {
            model.ForecastStartDate = DateTime.Today.AddDays(1);
            model.ForecastEndDate = DateTime.Today.AddDays(5);            
        }

        protected abstract DateTime[] GetForecastDates(WaveForecastReportModel model)
        {
            var dates = new List<DateTime>();

            int forecastDurationDays =
                (int)(model.ForecastEndDate - model.ForecastStartDate).TotalDays;

            DateTime currentDate = model.ForecastStartDate;
            for (int i = 0; i < forecastDurationDays * 8; i++)
            {
                dates.Add(currentDate);           
                currentDate = currentDate.AddHours(3);
            }

            return dates.ToArray();
        }

        protected abstract async Task<float> GetWaveHeight(DateTime forecastDate)
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

        

        public override void UpdateReportModel(float analysisValue, WaveForecastReportModel model)
        {
            if (analysisValue >= 0.85f && WavesStartAt == DateTime.MinValue)
            {
                string dateFromFileName = Path.GetFileNameWithoutExtension(imagePath).Replace("isramar.", "");
                WavesStartAt = DateTime.ParseExact(dateFromFileName, "yyMMddHH", CultureInfo.InvariantCulture);
            }
            else if (markedPixelsPercentage < 0.5 && WavesStartAt > DateTime.MinValue && WavesEndAt == DateTime.MinValue)
            {
                string dateFromFileName = Path.GetFileNameWithoutExtension(imagePath).Replace("isramar.", "");
                WavesEndAt = DateTime.ParseExact(dateFromFileName, "yyMMddHH", CultureInfo.InvariantCulture);
            }
        }

       

    }
}
