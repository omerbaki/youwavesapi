using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveAnalyzerCommon;

namespace LevYamWaveAnalyzer.Lev
{
    internal class WindAlertLevImageAnalyzer : WindAlertImageAnalyzer
    {
        protected override Rectangle GetRelevantArea()
        {
            return new Rectangle(200, 320, 300, 150);
        }        
    }
}
