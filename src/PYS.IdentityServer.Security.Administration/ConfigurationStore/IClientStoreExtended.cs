using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Collections.Generic;

namespace PYS.IdentityServer.Security.Administration.ConfigurationStore
{
    public interface IClientStoreExtended : IClientStore
    {
        IEnumerable<Client> FindAllActiveClientsAsync();

        void CreateClient(ClientEntity cli);

        void EditClient(ClientEntity cli);
    }
}
