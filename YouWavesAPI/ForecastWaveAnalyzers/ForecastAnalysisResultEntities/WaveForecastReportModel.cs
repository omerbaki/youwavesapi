using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisModel
{
    public class WaveForecastReportModel : BaseReportModel
    {
        public DateTime ForecastStartDate { get; set; }
        public DateTime ForecastEndDate { get; set; }
        public List<WavesTiming> WavesTiming { get; set; }

        public WaveForecastReportModel()
        {
            this.WavesTiming = new List<WavesTiming>();
        }

        public void AddWaveTiming(DateTime forecastDate, float waveHeight)
        {
            this.WavesTiming.Add(
                new WavesTiming()
                {
                    Time = forecastDate,
                    Height = waveHeight
                });
        }
    }

    public class WavesTiming
    {
        public DateTime Time { get; set; }
        public float Height { get; set; }
    }
}
