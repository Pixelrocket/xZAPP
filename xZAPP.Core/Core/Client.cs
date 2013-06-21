using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace xZAPP.Core
{
    public class Client
    {
        public long clientId {get; set;}
        public string clientNameInformal {get; set;}
        public string clientNameFormal {get; set;}
        public long gender {get; set;}
        public string photo { get; set; }

        public Client()
        {

        }


        public List<Client> GetClients()
        {
            ClientProxy clProxy = new ClientProxy();
            List<Client> clients =  JsonConvert.DeserializeObject<List<Client>>(clProxy.GetJSON());

            return clients;
        }
    }
}
