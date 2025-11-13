using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CamV4.Helper
{
    public class FirebaseNotificationHelper
    {
        private static readonly string FirebaseServerKey = "AAAAiF7Xco0:APA91bE-mM7n-Av33pDCcKGrDuSc6urYUOAoChgztSfgtmj3Rc7NWdv3hwwpLadSzxDrnZnJfKzOMiPmqgR-rnLwzGRnMSnZfjvxLlTPuQ77dbdeRsbELM-gAIX8fvoK-L1gA4tWQ2kB"; // You can find this in your Firebase console under Project Settings > Cloud Messaging.

        public static async Task SendNotification(string deviceToken, string title, string body)
        {
            var message = new
            {
                to = deviceToken,
                notification = new
                {
                    title = title,
                    body = body
                }
            };

            var jsonMessage = JsonConvert.SerializeObject(message);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=" + FirebaseServerKey);
                var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

                //var response = await client.PostAsync("https://fcm.googleapis.com/fcm/send", content);
                //var response = await client.PostAsync("https://fcm.googleapis.com/v1/projects/myproject-b5ae1/messages:send", content);
                var response = await client.PostAsync("https://fcm.googleapis.com/v1/projects/rn-x-698a9/messages:send", content);

                if (response.IsSuccessStatusCode)
                {
                    // Handle successful notification
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Notification sent successfully: " + responseBody);
                }
                else
                {
                    // Handle failure
                    Console.WriteLine("Error sending notification: " + response.StatusCode);
                }
            }
        }
    }
}



//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Services;
//using Google.Apis.Util.Store;
//using Newtonsoft.Json;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using System;
//using System.Threading;
//using System.Web;
//using System.IO;
//using System.Configuration;

//namespace CamV4.Helper
//{

//    public class FirebaseNotificationHelper
//    {
//        private static readonly string FirebaseProjectId = "AAAAiF7Xco0:APA91bE-mM7n-Av33pDCcKGrDuSc6urYUOAoChgztSfgtmj3Rc7NWdv3hwwpLadSzxDrnZnJfKzOMiPmqgR-rnLwzGRnMSnZfjvxLlTPuQ77dbdeRsbELM-gAIX8fvoK-L1gA4tWQ2kB";
//        private static readonly string FirebaseOAuth2Endpoint = "https://oauth2.googleapis.com/token";
//        private static string[] Scopes = { "https://www.googleapis.com/auth/firebase" };
//        private string appleKeyId = ConfigurationManager.AppSettings["AppleKeyId"];
//        private string appleTeamId = ConfigurationManager.AppSettings["AppleTeamId"];
//        private string appId = ConfigurationManager.AppSettings["AppId"];
//        private string appleAuthKeyFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["AppleAuthKeyFile"]);

//        public async Task SendNotification(string deviceToken, string title, string body)
//        {


//            var apns = new IOSPushNotificationHandler(appleKeyId, appleTeamId, appId, appleAuthKeyFile, true);
//            apns.JwtAPNsPush("e4qMwZQIOUH0tvX9bM1gZs:APA91bF4oPcnbMaaXemi6b8dsxSA8S14SgChHaLTcGKHE3GhZLAvl2D1UKmb35psRYpXb0dAxFOVOcKRU-iZ7_cqeaw46MpPZ2QPwEHYrabmyaIQH_l8q54", "CAM", "This is test");

//            //// Path to your service account JSON file
//            //string path = HttpContext.Current.Server.MapPath("~/") + "/rn-x-698a9-firebase-adminsdk-815g1-096d08a7c3.json";

//            //// Load service account credentials
//            //GoogleCredential credential;
//            //using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
//            //{
//            //    credential = GoogleCredential.FromStream(stream)
//            //                .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
//            //}

//            //// Get the access token from the service account credentials
//            //var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();  // Correct method

//            //// Prepare the message payload
//            //var message = new
//            //{
//            //    message = new
//            //    {
//            //        token = deviceToken,
//            //        notification = new
//            //        {
//            //            title = title,
//            //            body = body
//            //        }
//            //    }
//            //};

//            //// Convert the message to JSON
//            //var jsonMessage = JsonConvert.SerializeObject(message);

//            //// Send the notification via FCM
//            //using (var client = new HttpClient())
//            //{
//            //    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + accessToken); // accessToken
//            //    var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

//            //    var response = await client.PostAsync($"https://fcm.googleapis.com/v1/projects/rn-x-698a9/messages:send", content);

//            //    if (response.IsSuccessStatusCode)
//            //    {
//            //        string responseBody = await response.Content.ReadAsStringAsync();
//            //        //Console.WriteLine("Notification sent successfully: " + responseBody);
//            //    }
//            //    else
//            //    {
//            //        //Console.WriteLine("Error sending notification: " + response.StatusCode);
//            //    }
//            //}
//        }
//    }
//}


