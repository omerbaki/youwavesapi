using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisModel
{
    public abstract class WaveAnalysisModel
    {
        private DateTime mCreatedAt;
        public WaveAnalysisModel()
        {
            mCreatedAt = DateTime.Now;
        }

        public DateTime CreatedAt { get { return mCreatedAt; } }

        public abstract void Update(float markedPixelsPercentage, string imagePath);
    }
}
