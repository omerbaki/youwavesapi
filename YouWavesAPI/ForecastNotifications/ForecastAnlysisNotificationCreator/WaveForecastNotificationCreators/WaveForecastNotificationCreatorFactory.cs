using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisNotificationCreator.WaveForecastNotificationCreators
{
    public interface IWaveForecastNotificationCreatorFactory
    {
        IWaveForecastNotificationCreator Create(string name);
    }

    class WaveForecastNotificationCreatorFactory : IWaveForecastNotificationCreatorFactory
    {
        private readonly IUnityContainer mContainer;

        public WaveForecastNotificationCreatorFactory(IUnityContainer container)
        {
            mContainer = container;
        }

        public IWaveForecastNotificationCreator Create(string name)
        {
            return mContainer.Resolve<IWaveForecastNotificationCreator>(name);
        }
    }
}
