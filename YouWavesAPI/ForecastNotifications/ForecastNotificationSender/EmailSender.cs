using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForecastNotificaitonEntities;
using System.Net.Mail;
using System.Net.Mime;
using ForecastNotificationSender.ForecastNotificationFormatters;

namespace ForecastNotificationSender
{
    public interface IEmailSender
    {
        Task Send(IForecastNotificationFormatter forecastNotificationFormatter);
    }

    public class EmailSender : IEmailSender
    {
        public async Task Send(IForecastNotificationFormatter forecastNotificationFormatter)
        {
            var emailModel = await forecastNotificationFormatter.GetEmailFormat();

            try
            {
                MailMessage mailMsg = new MailMessage();

                // To
                mailMsg.To.Add(new MailAddress("omerbaki@gmail.com", "Omer Baki"));

                // From
                mailMsg.From = new MailAddress("notification@youwaves.com", "YouWaves");

                // Subject and multipart/alternative Body
                mailMsg.Subject = emailModel.Subject;
                string text = emailModel.Body;
                //string html = @"<p>html body</p>";
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                //mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                // Init SmtpClient and send
                SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("omerbaki", "1234.com");
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
