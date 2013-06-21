using System;
using System.Net;
using System.IO;

namespace xZAPP.Core
{
    public class ClientProxy
    {

        private string wsURL = "https://www.greenhillhost.nl/ws_zapp/getClients/";

        public ClientProxy()
        {
        }
       

        public string GetJSON()
        {
            var request = HttpWebRequest.Create(wsURL);
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if(string.IsNullOrWhiteSpace(content)) {
                        Console.Out.WriteLine("Response contained empty body...");
                    }
                    else {
                        Console.Out.WriteLine("Response Body: \r\n {0}", content);
                        return content;
                    }
                }
            }

            return "";
        }
    }
}

