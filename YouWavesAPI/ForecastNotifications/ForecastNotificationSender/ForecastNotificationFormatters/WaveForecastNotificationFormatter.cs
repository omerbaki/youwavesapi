using ForecastNotificaitonEntities;
using Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastNotificationSender.ForecastNotificationFormatters
{
    class WaveForecastNotificationFormatter : IForecastNotificationFormatter
    {
        private IJsonSerializer mJsonSerializer;

        public WaveForecastNotificationFormatter(IJsonSerializer jsonSerializer)
        {
            mJsonSerializer = jsonSerializer;
        }

        public async Task<EmailModel> GetEmailFormat()
        {
            var notificationsDirectory = Path.Combine("Notifications", DateTime.Now.ToString("yyyyMMdd"));
            string[] waveForecastNotifications = Directory.GetFiles(notificationsDirectory, typeof(WaveForecastNotificationModel).Name + "*");
            var waveForecastNotification =
               (await mJsonSerializer.Import(waveForecastNotifications[0], typeof(WaveForecastNotificationModel)))
               as WaveForecastNotificationModel;

            var emailModel = new EmailModel();
            if (waveForecastNotification.IsramarStartDate == default(DateTime))
            {
                emailModel.Subject = "No waves in the coming 3 days";
                emailModel.Body = "No waves in the coming 3 days";
            }
            else if (waveForecastNotification.IsramarStartDate == DateTime.Today &&
                     waveForecastNotification.IsramarEndDate == default(DateTime))
            {
                emailModel.Subject = string.Format("Waves continue today till after {0}", waveForecastNotification.IsramarStartDate.AddDays(2).DayOfWeek);
                emailModel.Body =
                    string.Format(
                    "Waves continue past {0}",
                    waveForecastNotification.IsramarStartDate.AddDays(2).DayOfWeek);
            }
            else if (waveForecastNotification.IsramarStartDate == DateTime.Today &&
                     waveForecastNotification.IsramarEndDate != default(DateTime))
            {
                emailModel.Subject = string.Format("Waves continue till {0}", waveForecastNotification.IsramarEndDate.DayOfWeek);
                emailModel.Body =
                    string.Format(
                    "Waves continue till {0} at {1}",
                    waveForecastNotification.IsramarEndDate.DayOfWeek,
                    waveForecastNotification.IsramarEndDate.Hour);
            }
            else if (waveForecastNotification.IsramarEndDate == default(DateTime))
            {
                emailModel.Subject = string.Format("Waves coming on {0}", waveForecastNotification.IsramarStartDate.DayOfWeek);
                emailModel.Body =
                    string.Format(
                    "Waves coming on {0} at {1} lasting past {2}",
                    waveForecastNotification.IsramarStartDate.DayOfWeek,
                    waveForecastNotification.IsramarStartDate.ToString("h tt"),
                    waveForecastNotification.IsramarStartDate.AddDays(2).DayOfWeek);
            }
            else
            {
                emailModel.Subject = string.Format("Waves coming on {0}", waveForecastNotification.IsramarStartDate.DayOfWeek);
                emailModel.Body =
                    string.Format(
                    "Waves coming on {0} at {1} lasting till {2} at {3}",
                    waveForecastNotification.IsramarStartDate.DayOfWeek,
                    waveForecastNotification.IsramarStartDate.ToString("h tt"),
                    waveForecastNotification.IsramarEndDate.DayOfWeek,
                    waveForecastNotification.IsramarEndDate.ToString("h tt"));
            }

            return emailModel;
        }
    }
}
