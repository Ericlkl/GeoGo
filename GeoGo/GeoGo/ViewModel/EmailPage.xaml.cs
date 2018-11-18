using System;
using System.IO;
using Plugin.Messaging;

using GeoGo.Model;
using Newtonsoft.Json;

using Xamarin.Forms;


namespace GeoGo.ViewModel
{
    public partial class EmailPage : ContentPage
    {
        private GeoData targetData;

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

            var geoJson = new 
            {
                type = "Feature",
                geometry = new {
                    type = targetData.GeometryShape,
                    coordinate = targetData.Coordinates
                },
                properties = new {
                    featureName = targetData.Name,
                    provider = targetData.Provider,
                    description = targetData.Description,
                    lastUpdate = targetData.LastUpdate,
                    attributes = targetData.Properties
                }
            };

            var jsonString = JsonConvert.SerializeObject(geoJson);

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "GeoData.json");

            // Write , the second parameter determine overwrite the file or not
            using (var streamWriter = new StreamWriter(filename, false))
            {
                streamWriter.Write(jsonString);
            }
            // Read 
            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(content);
                DisplayAlert("Message", content, "Okay");
            }

            var emailMessager = CrossMessaging.Current.EmailMessenger;

            if(emailMessager.CanSendEmailAttachments)
            {
                var email = new EmailMessageBuilder()
                    .To(receiver_entry.Text)
                    .Subject(Subject_Entry.Text)
                    .Body(body_Editor.Text)
                    //.WithAttachment(file)
                    .Build();


                emailMessager.SendEmail(email);
                DisplayAlert("Msg", "Success", "Okay");
            }
            else {
                DisplayAlert("Msg", "Fail to send a msg", "Okay");
            }
        }

    }
}
