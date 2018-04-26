using IdentityServer4.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PYS.IdentityServer.Security.Administration.ConfigurationStore
{
    public class ClientStore : IClientStoreExtended
    {
        private readonly ConfigurationStoreContext _context;
        private readonly ILogger _logger;

        public ClientStore(ConfigurationStoreContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("ClientStore");
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = _context.Clients.First(t => t.ClientId == clientId);
            client.MapDataFromEntity();
            return Task.FromResult(client.Client);
        }

        public IEnumerable<Client> FindAllActiveClientsAsync()
        {
            var client = _context.Clients.ToList();
            List<Client> clients = new List<Client>();
            foreach (var item in client)
            {
                item.MapDataFromEntity();
                clients.Add(item.Client);

            }
            return clients;
        }

        public void CreateClient(ClientEntity cli)
        {
            _context.Clients.Add(cli);
            _context.SaveChanges();
        }

        public void EditClient(ClientEntity cli)
        {
            _context.Clients.Update(cli);
            _context.SaveChanges();
        }
    }
}
