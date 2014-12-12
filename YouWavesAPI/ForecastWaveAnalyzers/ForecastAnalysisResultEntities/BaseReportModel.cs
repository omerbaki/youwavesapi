using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisEntities
{
    public abstract class BaseReportModel
    {
        private DateTime mCreatedAt;
        public BaseReportModel()
        {
            mCreatedAt = DateTime.Now;
        }

        public DateTime CreatedAt { get { return mCreatedAt; } }
    }
}
