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
    internal class IsramarWaveAnalyzer : WaveForecastAnalyzerBase
    {
        private const string ISRAMAR_IMAGE_NAME = "isramar.{0}.gif";
        private const string ISRAMAR_IMAGE_URL = "http://isramar.ocean.org.il/isramar2009/wave_model/wave_maps/wam/{0}/coarse/{1}.windir.gif";

        public IsramarWaveAnalyzer(
            IImageDownloader imageDownloader,
            IImageAnalyzer imageAnalyzer)
            : base(imageDownloader, imageAnalyzer)
        {
        }

        protected override void SetForecastDates(WaveForecastReportModel model)
        {
            model.ForecastStartDate = DateTime.Today.AddDays(1);
            model.ForecastEndDate = DateTime.Today.AddDays(5);            
        }

        protected override DownloadImageModel[] GetImageModels(BaseReportModel waveAnalysisModel)
        {
            var isramarWaveAnalysisModel = waveAnalysisModel as IsramarWaveAnalysisModel;

            int forecastDurationDays =
                (int)(isramarWaveAnalysisModel.ForecastEndDate - isramarWaveAnalysisModel.ForecastStartDate).TotalDays;

            // Today format for URL - 1411110000
            string todayFormat = DateTime.Today.ToString("yyMMddHHmm");

            DateTime currentDate = isramarWaveAnalysisModel.ForecastStartDate;

            var imageModels = new List<DownloadImageModel>();
            for (int i = 0; i < forecastDurationDays * 8; i++)
            {
                // Image date format - 14111112
                string imageDateFormat = currentDate.ToString("yyMMddHH");

                imageModels.Add(new DownloadImageModel()
                {
                    ImageUrl = string.Format(ISRAMAR_IMAGE_URL, todayFormat, imageDateFormat),
                    ImageName = string.Format(ISRAMAR_IMAGE_NAME, imageDateFormat)
                });

                currentDate = currentDate.AddHours(3);
            }

            return imageModels.ToArray();
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

        public override bool ShouldRun()
        {
            bool alreadyRanToday = mLastRunTime.Date == DateTime.Today;
            return (DateTime.Now.Hour == 8) && !alreadyRanToday;
        }

    }
}
