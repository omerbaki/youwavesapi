using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveAnalyzerCommon;

namespace LevYamWaveAnalyzer.Lev
{
    internal class WindAlertLevImageDownloader : WindAlertImageDownloader
    {
        private const string LEV_YAM_IMAGE_NAME = "levyam.{0}.jpg";

        protected override string GetImageName()
        {
            return LEV_YAM_IMAGE_NAME;
        }

        protected override string GetDownloaderName()
        {
            return "Lev";
        }
    }
}
