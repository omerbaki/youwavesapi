using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveAnalyzerCommon;

namespace IsramarWaveAnalyzer
{
    public class IsramarWaveAnalyzerExtendedContainer : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IImageDownloader, IsramarImageDownloader>("IsramarImageDownloader");
            Container.RegisterType<IImageAnalyzer, IsramarImageAnalyzer>("IsramarImageAnalyzer");

            Container.RegisterType<IWaveAnalyzer, IsramarWaveAnalyzer>(
                "IsramarWaveAnalyzer",
                new InjectionConstructor(
                    new ResolvedParameter<IImageDownloader>("IsramarImageDownloader"),
                    new ResolvedParameter<IImageAnalyzer>("IsramarImageAnalyzer")));
        }
    }
}
