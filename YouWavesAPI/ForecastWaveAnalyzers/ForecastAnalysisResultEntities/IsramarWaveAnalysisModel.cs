using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisModel
{
    public class IsramarWaveAnalysisModel : WaveAnalysisModel
    {
        public DateTime ForecastStartDate { get; set; }
        public DateTime ForecastEndDate { get; set; }
        public DateTime WavesStartAt { get; set; }
        public DateTime WavesEndAt { get; set; }

        public override void Update(float markedPixelsPercentage, string imagePath)
        {
            if (markedPixelsPercentage >= 0.85f && WavesStartAt == DateTime.MinValue)
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
