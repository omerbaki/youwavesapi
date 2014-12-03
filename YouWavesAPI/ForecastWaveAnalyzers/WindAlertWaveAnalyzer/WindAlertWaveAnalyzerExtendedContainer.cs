using LevYamWaveAnalyzer.Ben;
using LevYamWaveAnalyzer.Lev;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveAnalyzerCommon;

namespace LevYamWaveAnalyzer
{
    public class WindAlertWaveAnalyzerExtendedContainer : UnityContainerExtension
    {
        protected override void Initialize()
        {
//            Container.RegisterType<IImageAnalyzer, WindAlertLevImageAnalyzer>("WindAlertLevImageAnalyzer");
//            Container.RegisterType<IImageDownloader, WindAlertLevImageDownloader>("WindAlertLevImageDownloader");
//
//            Container.RegisterType<IImageAnalyzer, WindAlertBenImageAnalyzer>("WindAlertBenImageAnalyzer");
//            Container.RegisterType<IImageDownloader, WindAlertBenImageDownloader>("WindAlertBenImageDownloader");
//
//            Container.RegisterType<IReportCreator, WindAlertLevWaveAnalyzer>(
//                "LevWaveAnalyzer",
//                new InjectionConstructor(
//                    new ResolvedParameter<IImageDownloader>("WindAlertLevImageDownloader"),
//                    new ResolvedParameter<IImageAnalyzer>("WindAlertLevImageAnalyzer")));
//            
//            Container.RegisterType<IReportCreator, WindAlertBenWaveAnalyzer>(
//                "BenWaveAnalyzer",
//                new InjectionConstructor(
//                    new ResolvedParameter<IImageDownloader>("WindAlertBenImageDownloader"),
//                    new ResolvedParameter<IImageAnalyzer>("WindAlertBenImageAnalyzer")));
        }
    }
}
