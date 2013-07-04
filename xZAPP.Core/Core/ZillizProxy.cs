using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace xZAPP.Core
{
    public class ZillizProxy
    {

        public enum ServiceURL
        {
            [Description("https://www.greenhillhost.nl/ws_zapp/getClients/index.cfm")]
            Clients,
            [Description("https://www.greenhillhost.nl/ws_zapp/getCredentials/index.cfm")]
            Credentials,
            [Description("https://www.greenhillhost.nl/ws_zapp/getDailyReports/index.cfm")]
            Reports
        }
 
        public ZillizProxy()
        {
        }
       
        // Synchronous call to Webservice and retreive JSON data
        public string GetJSON(ServiceURL wsUrl, string username = null, string password = null, string token = null, long clientId = -1)
        {
            var client = new WebClient();

            try
            {
                switch (wsUrl) {
                    case ServiceURL.Credentials:
                        client.QueryString.Add("frmUsername", username);
                        client.QueryString.Add("frmPassword", password);
                        break;
                    case ServiceURL.Clients:
                        client.QueryString.Add("token", token);
                        break;
                    case ServiceURL.Reports:
                        client.QueryString.Add("token", token);
                        client.QueryString.Add("clientId", clientId.ToString());
                        break;
                    default:
                        break;
                }

                var content = client.DownloadString(EnumExtensions.GetDescription<ServiceURL>(wsUrl));  

                if(string.IsNullOrWhiteSpace(content)) {
                    Console.Out.WriteLine("Response contained empty body...");
                }
                else {
                    Console.Out.WriteLine("Response Body: \r\n {0}", content);
                    return content;
                }
            }

            catch (WebException webEx)
            {
                Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", ((HttpWebResponse)webEx.Response).StatusCode);
                throw webEx;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error fetching data. Server returned error: {0}", ex.Message);
                return "";
           }

            client.Dispose();
            return "";
        }

        // ASynchronous call to Webservice and retreive JSON data
        public async Task<string> GetJSONAsync(ServiceURL wsUrl, string username = null, string password = null, string token = null, long clientId = -1)
        {
            var client = new WebClient();

            try
            {
                switch (wsUrl) {
                    case ServiceURL.Credentials:
                        client.QueryString.Add("frmUsername", username);
                        client.QueryString.Add("frmPassword", password);
                        break;
                    case ServiceURL.Clients:
                        client.QueryString.Add("token", token);
                        break;
                    case ServiceURL.Reports:
                        client.QueryString.Add("token", token);
                        client.QueryString.Add("clientId", clientId.ToString());
                        break;
                    default:
                        break;
                }

                // Read the results from de Webservice call
                var content = client.DownloadStringTaskAsync(EnumExtensions.GetDescription<ServiceURL>(wsUrl));

                if(string.IsNullOrWhiteSpace(await content)) {
                    Console.Out.WriteLine("Response contained empty body...");
                    return "";
                }
                else {
                    Console.Out.WriteLine("Response Body: \r\n {0}", content);
                    return await content;
                }
            }
            catch (WebException webEx)
            {
                // Catch webexception to retreive HttpHeaders for Statuscode ...
                Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", ((HttpWebResponse)webEx.Response).StatusCode);
                throw webEx;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error fetching data. Error: {0}", ex.Message);
                return "";
            }
            finally
            {
                client.Dispose();
            }
        }   
    }
}

