using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;

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
            List<Client> clients =  JsonConvert.DeserializeObject<List<Client>>(clProxy.GetJSON(ZillizProxy.ServiceURL.Clients, token:token, wsAction:"GET"));

            return clients;
        }

        // Async version of GetClients
        public async Task<List<Client>> GetClientsAsync(string token)
        {
            List<Client> clients = null;
            ZillizProxy clProxy = new ZillizProxy();          

            // Call async proxy and use returned JSON string
            string jsonClients = await clProxy.GetJSONAsync(ZillizProxy.ServiceURL.Clients, token:token, wsAction:"GET");
            clients = JsonConvert.DeserializeObject<List<Client>>(jsonClients);

            return clients;
        }

        public Client ReadLatestClient()
        {
            // Read client id from disk, return null when no file is found
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            if(File.Exists(Path.Combine (documents, "LatestClient.json")))
            {
                var clientJson = File.ReadAllText(Path.Combine(documents, "LatestClient.json"));
                return JsonConvert.DeserializeObject<Client>(clientJson);
            }
            else
            {
                return null;
            }
        }

        public void WriteLatestClient(Client client)
        {
            // Store selected client on disk
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            var filename = Path.Combine (documents, "LatestClient.json");
            File.WriteAllText(filename, JsonConvert.SerializeObject(client));
        }
    }
}
