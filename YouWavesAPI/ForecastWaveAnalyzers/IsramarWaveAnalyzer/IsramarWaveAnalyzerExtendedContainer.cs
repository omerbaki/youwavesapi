using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveAnalyzerCommon;
using Framework;

namespace IsramarWaveAnalyzer
{
    public class IsramarWaveAnalyzerExtendedContainer : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IImageAnalyzer, IsramarImageAnalyzer>("IsramarImageAnalyzer");

            Container.RegisterType<IReportCreator, IsramarWaveForecastReportCreator>(
                "IsramarWaveAnalyzer",
                new InjectionConstructor(
                    new ResolvedParameter<IImageDownloader>(),
                    new ResolvedParameter<IImageAnalyzer>("IsramarImageAnalyzer")));
        }
    }
}
