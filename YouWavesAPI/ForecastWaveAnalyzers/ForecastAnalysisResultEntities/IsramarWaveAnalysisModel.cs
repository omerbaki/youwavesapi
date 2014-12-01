using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisModel
{
    public class IsramarWaveAnalysisModel : BaseReportModel
    {
        public DateTime ForecastStartDate { get; set; }
        public DateTime ForecastEndDate { get; set; }
        public DateTime WavesStartAt { get; set; }
        public DateTime WavesEndAt { get; set; }

                
    }
}
