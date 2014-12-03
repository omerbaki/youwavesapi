using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForecastAnalysisModel;
using WaveAnalyzerCommon.Model;

namespace WaveAnalyzerCommon
{
    public interface IImageAnalyzer
    {
        WaveHeight GetWaveHeight(byte[] imageBytes);
    }
}
