using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveAnalyzerCommon;

namespace LevYamWaveAnalyzer.Ben
{
    internal class WindAlertBenImageDownloader : WindAlertImageDownloader
    {
        private const string BEN_IMAGE_NAME = "ben.{0}.jpg";

        protected override string GetImageName()
        {
            return BEN_IMAGE_NAME;
        }

        protected override string GetDownloaderName()
        {
            return "Ben";
        }
    }
}
