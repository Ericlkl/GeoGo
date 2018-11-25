using System;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Net.Security;

using GeoGo.Model;
using Newtonsoft.Json;

using Xamarin.Forms;
using System.Security.Cryptography.X509Certificates;

namespace GeoGo.ViewModel
{
    public partial class EmailPage : ContentPage
    {
        private GeoData targetData;

        string filename = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.Personal) , "GeoData.json");

        public EmailPage()
        {
            InitializeComponent();
        }

        public EmailPage(GeoData geoData)
        {
            targetData = geoData;
            InitializeComponent();
        }

        void SendBtn_Clicked(object sender, System.EventArgs e)
        {

            //var jsonString = GenerateGeoJsonString();
            //// Write , the second parameter determine overwrite the file or not
            //using (var streamWriter = new StreamWriter(filename, false))
            //{
            //    streamWriter.Write(jsonString);
            //}

            //var emailMessager = new EmailMessager(
            //    receiver_entry.Text, 
            //    body_Editor.Text, 
            //    Subject_Entry.Text, 
            //    filename);

            //string message = emailMessager.SendEmail();

            //DisplayAlert("Status", message, "Okay");
            SendEmail();

        }

        string GenerateGeoJsonString ()
        {
            var geoJson = new
            {
                type = "Feature",
                geometry = new
                {
                    type = targetData.GeometryShape,
                    coordinate = targetData.Coordinates
                },
                properties = new
                {
                    featureName = targetData.Name,
                    provider = targetData.Provider,
                    description = targetData.Description,
                    lastUpdate = targetData.LastUpdate,
                    attributes = targetData.Properties
                }
            };

            return JsonConvert.SerializeObject(geoJson);
        }

        void SendEmail()
        {
            try
            {
                Attachment json_file = new Attachment(filename);

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("geogo.system@gmail.com"),
                    Subject = Subject_Entry.Text,
                    Body = body_Editor.Text,
                    IsBodyHtml = true
                };

                mail.To.Add(receiver_entry.Text);
                mail.Attachments.Add(json_file);

                SmtpClient smtpServer = new SmtpClient()
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Port = 587,
                    Host = "smtp.gmail.com",
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("geogo.system@gmail.com", "geo5system"),
                    EnableSsl = true
                };

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                smtpServer.Send(mail);

                smtpServer.Dispose();
                mail.Dispose();

                DisplayAlert("Success", "Email has been sent successfully!", "Okay");
            }

            catch (Exception mailNotSent)
            {
                DisplayAlert("Status", "Unable to send Email. Error : " + mailNotSent, "Okay");
            }
        }


    }
}
