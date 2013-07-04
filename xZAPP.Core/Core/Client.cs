using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace xZAPP.Core
{
    public class Client : IClient
    {
        public long ClientId {get; set;}
        [JsonProperty(PropertyName="clientNameInformal")]
        public string InformalName {get; set;}
        [JsonProperty(PropertyName="clientNameformal")]
        public string FormalName {get; set;}
        public long Gender {get; set;}
        public string Photo { get; set; }
        [JsonProperty(PropertyName="aantalDagRapportages")]
        public long NumberOfDailyReports { get; set; }

        public Client()
        {

        }

        public List<Client> GetClients(string token)
        {
            ZillizProxy clProxy = new ZillizProxy();
            List<Client> clients =  JsonConvert.DeserializeObject<List<Client>>(clProxy.GetJSON(ZillizProxy.ServiceURL.Clients, token:token));

            return clients;
        }

        // Async version of GetClients
        public async Task<List<Client>> GetClientsAsync(string token)
        {
            List<Client> clients = null;
            ZillizProxy clProxy = new ZillizProxy();          

            // Call async proxy and use returned JSON string
            string jsonClients = await clProxy.GetJSONAsync(ZillizProxy.ServiceURL.Clients, token:token);
            clients = JsonConvert.DeserializeObject<List<Client>>(jsonClients);

            return clients;
        }
    }
}
