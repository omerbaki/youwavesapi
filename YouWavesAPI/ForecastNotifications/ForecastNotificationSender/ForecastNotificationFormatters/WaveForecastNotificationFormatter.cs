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

            int isramarForecastDuration =
                (int)(waveForecastNotification.IsramarForecastEndDate - waveForecastNotification.IsramarForecastStartDate).TotalDays;

            var emailModel = new EmailModel();
            if (waveForecastNotification.IsramarWavesStartDate == default(DateTime))
            {
                emailModel.Subject = string.Format("No waves in the coming {0} days", isramarForecastDuration);
                emailModel.Body = string.Format("No waves in the coming {0} days", isramarForecastDuration);
            }
            else if (waveForecastNotification.IsramarWavesStartDate == waveForecastNotification.IsramarForecastStartDate &&
                     waveForecastNotification.IsramarWavesEndDate == default(DateTime))
            {
                emailModel.Subject = 
                    string.Format(
                        "Waves continue today till after {0}",
                        waveForecastNotification.IsramarWavesStartDate.AddDays(isramarForecastDuration - 1).DayOfWeek);
                emailModel.Body =
                    string.Format(
                    "Waves continue past {0}",
                    waveForecastNotification.IsramarWavesStartDate.AddDays(isramarForecastDuration - 1).DayOfWeek);
            }
            else if (waveForecastNotification.IsramarWavesStartDate == DateTime.Today &&
                     waveForecastNotification.IsramarWavesEndDate != default(DateTime))
            {
                emailModel.Subject = string.Format("Waves continue till {0}", waveForecastNotification.IsramarWavesEndDate.DayOfWeek);
                emailModel.Body =
                    string.Format(
                    "Waves continue till {0} at {1}",
                    waveForecastNotification.IsramarWavesEndDate.DayOfWeek,
                    waveForecastNotification.IsramarWavesEndDate.Hour);
            }
            else if (waveForecastNotification.IsramarWavesEndDate == default(DateTime))
            {
                emailModel.Subject = string.Format("Waves coming on {0}", waveForecastNotification.IsramarWavesStartDate.DayOfWeek);
                emailModel.Body =
                    string.Format(
                    "Waves coming on {0} at {1} lasting past {2}",
                    waveForecastNotification.IsramarWavesStartDate.DayOfWeek,
                    waveForecastNotification.IsramarWavesStartDate.ToString("h tt"),
                    waveForecastNotification.IsramarWavesStartDate.AddDays(isramarForecastDuration - 1).DayOfWeek);
            }
            else
            {
                emailModel.Subject = string.Format("Waves coming on {0}", waveForecastNotification.IsramarWavesStartDate.DayOfWeek);
                emailModel.Body =
                    string.Format(
                    "Waves coming on {0} at {1} lasting till {2} at {3}",
                    waveForecastNotification.IsramarWavesStartDate.DayOfWeek,
                    waveForecastNotification.IsramarWavesStartDate.ToString("h tt"),
                    waveForecastNotification.IsramarWavesEndDate.DayOfWeek,
                    waveForecastNotification.IsramarWavesEndDate.ToString("h tt"));
            }

            return emailModel;
        }
    }
}
