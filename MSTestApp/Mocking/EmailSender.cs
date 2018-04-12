using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MSTestApp.Mocking
{
    public interface IEmailSender
    {
        void EmailFile(string emailAddress, string emailBody, string filename, string subject);
    }

    public class EmailSender : IEmailSender
    {
        public void EmailFile(string emailAddress, string emailBody, string filename, string subject)
        {

            var client = new SmtpClient(SystemSettingHelper.EmailSmtpHost)
            {
                Port = SystemSettingHelper.EmailPort,
                Credentials = new NetworkCredential(
                    SystemSettingHelper.EmailUsername,
                    SystemSettingHelper.EmailPassword)
            };

            var from = new MailAddress(SystemSettingHelper.EmailFromEmail, SystemSettingHelper.EmailFrom);
            var to = new MailAddress(emailAddress);

            var message = new MailMessage(from, to)
            {
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
                Body = emailBody,
                BodyEncoding = Encoding.UTF8
            };

            message.Attachments.Add(new Attachment(filename));
            client.Send(message);
            message.Dispose();

            File.Delete(filename);

        }
    }
}
