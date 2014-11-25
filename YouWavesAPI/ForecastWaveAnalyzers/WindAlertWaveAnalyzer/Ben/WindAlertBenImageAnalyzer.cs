using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveAnalyzerCommon;

namespace LevYamWaveAnalyzer.Ben
{
    internal class WindAlertBenImageAnalyzer : WindAlertImageAnalyzer
    {
        protected override Rectangle GetRelevantArea()
        {
            return new Rectangle(250, 210, 380, 30);
        }
    }
}
