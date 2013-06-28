using System;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace xZAPP.Core
{
    public class ClientProxy
    {

        private string wsURL = "https://www.greenhillhost.nl/ws_zapp/getClients/";

        public ClientProxy()
        {
        }
       
        // Synchronous call to Webservice and retreive JSON data
        public string GetJSON()
        {
            var client = new WebClient ();

            try
            {
                var content = client.DownloadString(wsURL);  
                if(string.IsNullOrWhiteSpace(content)) {
                    Console.Out.WriteLine("Response contained empty body...");
                }
                else {
                    Console.Out.WriteLine("Response Body: \r\n {0}", content);
                    return content;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error fetching data. Server returned error: {0}", ex.Message);
            }

            client.Dispose();
            return "";
        }

        // ASynchronous call to Webservice and retreive JSON data
        public async Task<string> GetJSONAsync()
        {
            var request = HttpWebRequest.Create(wsURL);
            request.ContentType = "application/json";
            request.Method = "GET";

            var httpClient = new HttpClient();

            // HttpClient is by default asynchronous ...
            HttpResponseMessage response = await httpClient.GetAsync(wsURL);
           
            try
            {
                response.EnsureSuccessStatusCode();

                // Read the results from de Webservice call
                var content = response.Content.ReadAsStringAsync();
                if(string.IsNullOrWhiteSpace(await content)) {
                    Console.Out.WriteLine("Response contained empty body...");
                    return "";
                }
                else {
                    Console.Out.WriteLine("Response Body: \r\n {0}", await content);
                    return await content;
                }
            }
            catch (WebException webEx)
            {
                // Catch webexception to retreive HttpHeaders for Statuscode ...
                Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", ((HttpWebResponse)webEx.Response).StatusCode);
                return "";
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error fetching data. Error: {0}", ex.Message);
                return "";
            }
            finally
            {
                httpClient.Dispose();
            }
        }       
    }
}

