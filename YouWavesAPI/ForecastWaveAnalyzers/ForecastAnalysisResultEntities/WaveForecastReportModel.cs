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
    }

    public class WavesTiming
    {
        public DateTime Time { get; set; }
        public float Height { get; set; }
    }
}
