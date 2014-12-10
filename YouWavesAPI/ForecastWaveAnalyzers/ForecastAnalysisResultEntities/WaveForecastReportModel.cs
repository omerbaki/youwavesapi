using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisModel
{
    public enum WaveHeight
    {
        None,
        Small,
        Medium,
        Big
    }

    [Storage("WaveForecastReport")]
    public class WaveForecastReportModel : BaseReportModel
    {
        public string Source { get; set; }
        public DateTime ForecastStartDate { get; set; }
        public DateTime ForecastEndDate { get; set; }
        public List<WavesTiming> WavesTiming { get; set; }

        public WaveForecastReportModel(string source)
        {
            this.Source = source;
            this.WavesTiming = new List<WavesTiming>();
        }

        public void AddWaveTiming(DateTime forecastDate, WaveHeight waveHeight)
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
        public WaveHeight Height { get; set; }
    }
}
