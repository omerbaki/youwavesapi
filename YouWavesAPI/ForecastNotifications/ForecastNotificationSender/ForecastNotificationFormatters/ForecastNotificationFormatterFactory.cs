using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastNotificationSender.ForecastNotificationFormatters
{
    public interface IForecastNotificationFormatterFactory
    {
        IForecastNotificationFormatter Create(string name);
    }

    class ForecastNotificationFormatterFactory : IForecastNotificationFormatterFactory
    {
        private readonly IUnityContainer mContainer;

        public ForecastNotificationFormatterFactory(IUnityContainer container)
        {
            mContainer = container;
        }

        public IForecastNotificationFormatter Create(string name)
        {
            return mContainer.Resolve<IForecastNotificationFormatter>(name);
        }
    }
}
