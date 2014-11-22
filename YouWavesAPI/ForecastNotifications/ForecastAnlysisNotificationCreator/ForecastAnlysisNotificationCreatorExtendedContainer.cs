using ForecastAnalysisNotificationCreator.WaveForecastNotificationCreators;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisNotificationCreator
{
    public class ForecastAnlysisNotificationCreatorExtendedContainer : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IWaveForecastNotificationCreatorFactory, WaveForecastNotificationCreatorFactory>();
            Container.RegisterType<IForecastNotificationCreator, ForecastNotificationCreator>();

            Container.RegisterType<IWaveForecastNotificationCreator, IsramarWaveForecastNotificationCreator>(typeof(IsramarWaveAnalysisResult).Name);
        }
    }
}
