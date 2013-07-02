using System;
using xZAPP.Core;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace xZAPP.Core
{
    public class Credentials : ICredentials
    {
        public string firstName {get; set;}
        public string lastName {get; set;}
        public string infix {get; set;}
        public string emailAddress {get; set;}
        public long noOfClients {get; set;}
        public string token {get; set;}

        public Credentials()
        {
        }

        public Credentials CheckCredentials(string username = null, string password = null)
        {
            ZillizProxy clProxy = new ZillizProxy();
            List<Credentials> cr =  JsonConvert.DeserializeObject<List<Credentials>>(clProxy.GetJSON(ZillizProxy.ServiceURL.Credentials, username:username, password:password));

            return cr.Count == 0 ? null : cr[0];

        }

        // Async version of GetClients
        public async Task<Credentials> CheckCredentialsAsync(string username = null, string password = null)
        {
            List<Credentials> cr = null;
            ZillizProxy clProxy = new ZillizProxy();          

            // Call async proxy and use returned JSON string
            string jsonCredentials = await clProxy.GetJSONAsync(ZillizProxy.ServiceURL.Credentials, username:username, password:password);
            cr = JsonConvert.DeserializeObject<List<Credentials>>(jsonCredentials);

            return cr.Count == 0 ? null : cr[0];
        }


    }
}

