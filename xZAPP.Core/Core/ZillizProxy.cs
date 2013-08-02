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
            [Description("https://www.greenhillhost.nl/ws_zapp/clients/index.cfm")]
            Clients,
            [Description("https://www.greenhillhost.nl/ws_zapp/sessions/index.cfm")]
            Credentials,
            [Description("https://www.greenhillhost.nl/ws_zapp/dailyReports/index.cfm")]
            Reports,
            [Description("https://www.greenhillhost.nl/ws_zapp/reactions/index.cfm")]
            Comments
        }
 
        public ZillizProxy()
        {
        }
       
        // Synchronous call to Webservice and retreive JSON data
        public string GetJSON(ServiceURL wsUrl, string username = null, string password = null, string token = null, string wsAction = "POST", long clientId = -1)
        {
            var uploadstring = "";
            var client = new WebClient();

            try
            {
                var content = "";
                if(wsAction == "POST"){
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    uploadstring = GetUploadString(wsUrl, username, password, token, clientId);
                    content = client.UploadString(EnumExtensions.GetDescription<ServiceURL>(wsUrl), uploadstring);
                }
                else{
                    GetQueryString(client, wsUrl, username, password, token, clientId);
                    content = client.DownloadString(EnumExtensions.GetDescription<ServiceURL>(wsUrl));
                }

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
        public async Task<string> GetJSONAsync(ServiceURL wsUrl, string username = null, string password = null, string token = null, string wsAction = "POST", long clientId = -1)
        {
         
            var uploadstring = "";
            var client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

            try
            {

                Task<string> content;
                if(wsAction == "POST"){
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    uploadstring = GetUploadString(wsUrl, username, password, token, clientId);
                    content = client.UploadStringTaskAsync(EnumExtensions.GetDescription<ServiceURL>(wsUrl), uploadstring);
                }
                else{
                    GetQueryString(client, wsUrl, username, password, token, clientId);
                    content = client.DownloadStringTaskAsync(EnumExtensions.GetDescription<ServiceURL>(wsUrl));
                }

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



        private string GetUploadString(ServiceURL wsUrl, string username, string password, string token, long clientId)
        {
            var uploadstring = "";

            switch (wsUrl)
            {
                case ServiceURL.Credentials:                   
                    uploadstring = "username=" + username + "&password=" + password;
                    break;
                case ServiceURL.Clients:
                    uploadstring = "token=" + token;
                    break;
                case ServiceURL.Reports:               
                    uploadstring = "token=" + token + "&clientid=" + clientId.ToString() + "&maxrows=1000";
                    break;
                default:
                    break;
            }
            return uploadstring;
        }

        private void GetQueryString(WebClient wc, ServiceURL wsUrl, string username, string password, string token, long clientId)
        {
          switch (wsUrl)
            {
                case ServiceURL.Credentials:                   
                    wc.QueryString.Add("username" , username);
                    wc.QueryString.Add("password" , password);
                    break;
                case ServiceURL.Clients:
                    wc.QueryString.Add("token" , token);
                    break;
                case ServiceURL.Reports:       
                    wc.QueryString.Add("token" , token);
                    wc.QueryString.Add("clientid" , clientId.ToString());
                    wc.QueryString.Add("maxrows" , "1000");
                    break;
                default:
                    break;
            }
        }
    }
}

