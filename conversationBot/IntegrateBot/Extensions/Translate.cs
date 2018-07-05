using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
// NOTE: Install the Newtonsoft.Json NuGet package.
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntegrateBots.Extensions
{
    public class Translate
    {
        static string host = "https://api.cognitive.microsofttranslator.com";
        static string path = "/translate?api-version=3.0";
        // Translate to Chinese
        public static string CHparams_ = "&to=zh-Hans";
        public static string ENparams_ = "&to=en";

        public static string CHuri = host + path + CHparams_;
        public static string ENuri = host + path + ENparams_;

        // NOTE: Replace this example key with a valid subscription key.
        static string key = "694cdc7986334ffdb7b8a4afcc5cedb6";

        public async static Task<string> TranslateCH(string inputEN)
        {
            System.Object[] body = new System.Object[] { new { Text = inputEN } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(CHuri);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseBody), Formatting.Indented);
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                dynamic jsonResponse = serializer.DeserializeObject(result);
                Console.WriteLine(jsonResponse["documents"][0]["translations"][0].text);
                return jsonResponse["documents"][0]["translations"][0].text;
            }
        }

        public async static Task<string> TranslateEN(string inputCH)
        {
            System.Object[] body = new System.Object[] { new { Text = inputCH } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(ENuri);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseBody), Formatting.Indented);
                string responseMsg = result.ToString();
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                dynamic jsonResponse = serializer.DeserializeObject(responseMsg);
                Console.WriteLine(jsonResponse["documents"][0]["translations"][0].text);
                return jsonResponse["documents"][0]["translations"][0].text;
            }
        }

        static void Main(string[] args)
        {
            string inputEN = "Hello world";
            string inputCH = "ÄãºÃ";

            //Test
            string str1 = "impact";
            string str2 = "How will I get impacted?";
            if (str2.Contains(str1) == true)
            {
                string result = "";
            }


                //Translate into Chinese
                var reponseA = TranslateCH(inputEN);
            //Translate back to English
            var reponseB = TranslateEN(inputCH);
            Console.ReadLine();
        }
    }
}
