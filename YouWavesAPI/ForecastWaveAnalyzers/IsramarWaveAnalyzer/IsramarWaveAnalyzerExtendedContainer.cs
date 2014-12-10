using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveAnalyzerCommon;
using Framework;
using ForecastAnalysisModel;

namespace IsramarWaveAnalyzer
{
    public class IsramarWaveAnalyzerExtendedContainer : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IImageAnalyzer, IsramarImageAnalyzer>("IsramarImageAnalyzer");

            var imageDownloader = Container.Resolve<ResolvedParameter<IImageDownloader>>();
            var storageAccessor = Container.Resolve<IStorageAccessor<WaveForecastReportModel>>();

            Container.RegisterType<IReportCreator, IsramarWaveForecastReportCreator>(
                "IsramarWaveAnalyzer",
                new InjectionConstructor(
                    imageDownloader,
                    new ResolvedParameter<IImageAnalyzer>("IsramarImageAnalyzer"),
                    storageAccessor));
        }
    }
}
