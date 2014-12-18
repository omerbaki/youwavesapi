using ForecastAnalysisEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastNotificaitonEntities
{
    public class WaveForecastComparisonNotificationModel
    {
        private List<WaveForeacastDailyData> mWaveForecastResources;
        private static readonly int[] HOURS = new int[] { 6, 9, 12, 15 };

        public WaveForecastComparisonNotificationModel()
        {
            mWaveForecastResources = new List<WaveForeacastDailyData>();
        }

        public WaveForeacastDailyData[] WaveForecastResources
        {
            get
            {
                return mWaveForecastResources.ToArray();
            }
        }

        public void AddForecastResource(WaveForecastReportModel waveForecastReportModel)
        {
            var waveForeacastDailyData = new WaveForeacastDailyData();
            waveForeacastDailyData.ForecastResource = waveForecastReportModel.Source;

            FillHoursFromReport(waveForeacastDailyData, waveForecastReportModel);

            mWaveForecastResources.Add(waveForeacastDailyData);
        
        }

        private void FillHoursFromReport(WaveForeacastDailyData waveForeacastDailyData, WaveForecastReportModel waveForecastReportModel)
        {
            DateTime currentDate = waveForecastReportModel.ForecastStartDate.Date;
            while (currentDate <= waveForecastReportModel.ForecastEndDate)
            {
                WaveHeight lastValue = waveForecastReportModel.WavesTiming.First().Height;
                foreach (var hour in HOURS)
                {
                    var waveHeightFromReport = waveForecastReportModel.WavesTiming.SingleOrDefault(x => x.Time == currentDate.AddHours(hour));
                    lastValue = waveHeightFromReport == null ? lastValue : waveHeightFromReport.Height;

                    waveForeacastDailyData.WavesHeight[currentDate.AddHours(hour)] = lastValue;
                }

                currentDate = currentDate.AddDays(1);
            }
        }
    }
    
    public class WaveForeacastDailyData
    {
        public string ForecastResource { get; set; }

        public Dictionary<DateTime, WaveHeight> WavesHeight { get; set; }
    }
}
