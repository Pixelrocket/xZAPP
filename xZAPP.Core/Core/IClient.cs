using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xZAPP.Core
{
    public interface IClient
    {
        List<Client> GetClients();
        Task<List<Client>> GetClientsAsync();
    }
}

