using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace xZAPP.Core
{
    public class Client : IClient
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

        // Async version of GetClients
        public async Task<List<Client>> GetClientsAsync()
        {
            List<Client> clients = null;
            ClientProxy clProxy = new ClientProxy();          

            // Call async proxy and use returned JSON string
            string jsonClients = await clProxy.GetJSONAsync();
            clients = JsonConvert.DeserializeObject<List<Client>>(jsonClients);

            return clients;
        }
    }
}
