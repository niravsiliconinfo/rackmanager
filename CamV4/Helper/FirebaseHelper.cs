using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;

namespace CamV4.Helper
{
    public class FirebaseHelper
    {
        private static IFirebaseClient firebaseClient;
        private static bool _isInitialized = false;


        //private static readonly string apnsKeyId = "TRTK356FLK";
        //private static readonly string apnsTeamId = "99T4384U3M";
        //private static readonly string apnsBundleId = "com.siliconinfo.camindustrial";        

        static FirebaseHelper()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "AAAAiF7Xco0:APA91bE-mM7n-Av33pDCcKGrDuSc6urYUOAoChgztSfgtmj3Rc7NWdv3hwwpLadSzxDrnZnJfKzOMiPmqgR-rnLwzGRnMSnZfjvxLlTPuQ77dbdeRsbELM-gAIX8fvoK-L1gA4tWQ2kB", // You may find this in the Firebase console or use a service account token
                BasePath = "https://rn-x-698a9-default-rtdb.firebaseio.com/"
            };
            firebaseClient = new FireSharp.FirebaseClient(config);
        }

        // To write data to Firebase
        public static void SetData(string path, object data)
        {
            SetResponse response = firebaseClient.Set(path, data);
        }

        // To get data from Firebase
        public static T GetData<T>(string path)
        {
            FirebaseResponse response = firebaseClient.Get(path);
            return JsonConvert.DeserializeObject<T>(response.Body);
        }

        public static void InitializeFirebase()
        {
            if (!_isInitialized)
            {
                string path = HttpContext.Current.Server.MapPath("~/") + "Keys\\rn-x-698a9-firebase-adminsdk-815g1-096d08a7c3.json";                    
                var credential = GoogleCredential.FromFile(path);                
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = credential
                });
                Console.WriteLine("Firebase Admin SDK Initialized.");
                _isInitialized = true;
            }
        }

        public static string GetFilePath()
        {
            string strPath = "";
            strPath = HttpContext.Current.Server.MapPath("~/") + "/rn-x-698a9-firebase-adminsdk-815g1-096d08a7c3.json";

            return strPath;
        }

        public async Task SendAndroidNotificationAsync(string deviceToken, string title, string body)
        {
            var message = new Message()
            {
                Token = deviceToken,
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },
                Data = new Dictionary<string, string>
            {
                { "key1", "value1" },
                { "key2", "value2" }
            }
            };

            try
            {
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine($"Successfully sent Android message: {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending Android notification: {ex.Message}");
            }
        }

        //public async Task SendIOSNotificationAsync(string deviceToken, string title, string body)
        //{
        //    try
        //    {
        //        // Create APNs request
        //        var apnsMessage = new
        //        {
        //            aps = new
        //            {
        //                alert = new
        //                {
        //                    title = title,
        //                    body = body
        //                },
        //                sound = "default"
        //            }
        //        };

        //        var httpClient = new HttpClient();
        //        httpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {GenerateApnsJwt()}");
        //        httpClient.DefaultRequestHeaders.Add("apns-topic", apnsBundleId);

        //        var jsonContent = JsonConvert.SerializeObject(apnsMessage);
        //        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        //        var response = await httpClient.PostAsync($"https://api.push.apple.com/3/device/" + deviceToken, content);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            Console.WriteLine("Successfully sent iOS notification");
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Failed to send iOS notification: {response.ReasonPhrase}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error sending iOS notification: {ex.Message}");
        //    }
        //}

        //private string GenerateApnsJwt()
        //{
        //    string apnsKeyPath = HttpContext.Current.Server.MapPath("~/") + "Keys\\AuthKey_KF94QW8VK6.p8";
        //    var header = new
        //    {
        //        alg = "ES256",
        //        kid = apnsKeyId
        //    };

        //    var payload = new
        //    {
        //        iss = apnsTeamId,
        //        iat = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        //    };

        //    var headerJson = JsonConvert.SerializeObject(header);
        //    var payloadJson = JsonConvert.SerializeObject(payload);

        //    // Base64 URL encode header and payload
        //    var encodedHeader = Base64UrlEncode(Encoding.UTF8.GetBytes(headerJson));
        //    var encodedPayload = Base64UrlEncode(Encoding.UTF8.GetBytes(payloadJson));

        //    // Create the signature using your APNs .p8 key
        //    var privateKey = File.ReadAllText(apnsKeyPath);
        //    var signature = GenerateApnsSignature(encodedHeader, encodedPayload, privateKey);

        //    return $"{encodedHeader}.{encodedPayload}.{signature}";
        //}

        //private string Base64UrlEncode(byte[] input)
        //{
        //    var base64 = Convert.ToBase64String(input);
        //    return base64.TrimEnd('=').Replace('+', '-').Replace('/', '_');
        //}

        //private string GenerateApnsSignature(string header, string payload, string privateKey)
        //{
        //    try
        //    {
        //        // Ensure private key is in the correct format (PKCS8 format) and read it correctly
        //        byte[] privateKeyBytes = Convert.FromBase64String(privateKey);

        //        using (var ecdsa = new System.Security.Cryptography.ECDsaCng(System.Security.Cryptography.CngKey.Import(privateKeyBytes, System.Security.Cryptography.CngKeyBlobFormat.Pkcs8PrivateBlob)))
        //        {
        //            var data = Encoding.UTF8.GetBytes($"{header}.{payload}");
        //            var signature = ecdsa.SignData(data);
        //            return Base64UrlEncode(signature);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error generating signature: {ex.Message}");
        //        throw;
        //    }
        //}
    }
}