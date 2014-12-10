using ForecastAnalysisModel;
using Framework;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveAnalyzerCommon
{
    public class WaveAnalyzerCommonExtendedContainer: UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IEnumerable<IReportCreator>, IReportCreator[]>();
            Container.RegisterType<IStorageAccessor<WaveForecastReportModel>, StorageAccessor<WaveForecastReportModel>>();
        }
    }
}
