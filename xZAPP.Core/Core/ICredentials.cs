using System;
using System.Threading.Tasks;

namespace xZAPP.Core
{
    public interface ICredentials
    {
        Credentials CheckCredentials(string username = null, string password = null);
        Task<Credentials> CheckCredentialsAsync(string username = null, string password = null);
    }
}

