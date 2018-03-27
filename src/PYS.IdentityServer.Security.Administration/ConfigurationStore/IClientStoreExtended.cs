using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PYS.IdentityServer.Security.Administration.ConfigurationStore
{
    public interface IClientStoreExtended : IClientStore
    {
        IEnumerable<Client> FindAllActiveClientsAsync();

        void CreateClient(ClientEntity cli);

        void EditClient(ClientEntity cli);
    }
}
