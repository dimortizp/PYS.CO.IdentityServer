using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Threading.Tasks;

namespace PYS.IdentityServer.Security.Administration.ConfigurationStore
{
    public interface IResourceStoreExtended : IResourceStore
    {
        Task<ApiResource> FindApiResourcesByNameAsync(string Name);
        void CreateApiResource(ApiResourceEntity res);
        void EditApiResource(ApiResourceEntity res);
        void ApiResourceChangeState(string name, bool enable);
    }
}
