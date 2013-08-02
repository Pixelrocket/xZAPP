using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xZAPP.Core
{
    public interface IClient
    {
        List<Client> GetClients(string token);
        Task<List<Client>> GetClientsAsync(string token);
        Client ReadLatestClient();
        void WriteLatestClient(Client client);
    }
}