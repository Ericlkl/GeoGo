using System;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace GeoGo.Model
{
    public class EmailMessager
    {
        public List<string>  Receivers;
        public string Body;
        public string Subject;
        public List<string> Attachments;


        public EmailMessager( string receiver, string body, string subject, string attachment_path)
        {
            Receivers.Add(receiver);
            Body = body;
            Subject = subject;
            Attachments.Add(attachment_path);
        }

        public EmailMessager(List<string> receivers, string body, string subject, List<string> attachments_path)
        {
            Receivers = receivers;
            Body = body;
            Subject = subject;
            Attachments = attachments_path;
        }

        public void AddReceiver(string receiver)
        {
            Receivers.Add(receiver);
        }

        public void AddAttachment(string attachment_path)
        {
            Attachments.Add(attachment_path);
        }


        public string SendEmail()
        {
            try
            {

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("geogo.system@gmail.com"),
                    Subject = Subject,
                    Body = Body,
                    IsBodyHtml = true
                };

                Receivers.ForEach ((string obj) => mail.To.Add(obj));
                Attachments.ForEach((string attachmentFile) => mail.Attachments.Add( new Attachment(attachmentFile) ));


                SmtpClient smtpServer = new SmtpClient()
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Port = 587,
                    Host = "smtp.gmail.com",
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("geogo.system@gmail.com", "geo5system"),
                    EnableSsl = true
                };

                //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                //{
                //    return true;
                //};

                smtpServer.Send(mail);

                smtpServer.Dispose();
                mail.Dispose();

                return "Email has been sent successfully!";
            }

            catch (Exception error)
            {
                return error.ToString();
            }
        }
    }
}
