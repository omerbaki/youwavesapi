using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisReportCreator
{
    public class ReportsCreatorExtendedContainer : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IReportsCreator, ReportsCreator>();            
        }
    }
}
