using IdentityServer4.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PYS.IdentityServer.Security.Administration.ConfigurationStore
{
    public class ResourceStore : IResourceStoreExtended
    {
        private readonly ConfigurationStoreContext _context;
        private readonly ILogger _logger;

        public ResourceStore(ConfigurationStoreContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("ResourceStore");
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            var apiResource = _context.ApiResources.First(t => t.Name == name);
            apiResource.MapDataFromEntity();
            return Task.FromResult(apiResource.ApiResource);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));


            var apiResources = new List<ApiResource>();
            var apiResourcesEntities = from i in _context.ApiResources
                                       where scopeNames.Contains(i.Name)
                                       select i;

            foreach (var apiResourceEntity in apiResourcesEntities)
            {
                apiResourceEntity.MapDataFromEntity();

                apiResources.Add(apiResourceEntity.ApiResource);
            }

            return Task.FromResult(apiResources.AsEnumerable());
        }

        public Task<ApiResource> FindApiResourcesByNameAsync(string Name)
        {
            if (Name == null) throw new ArgumentNullException(nameof(Name));


            var apiResources = new List<ApiResource>();
            var apiResourcesEntities = from i in _context.ApiResources
                                       where i.Name.Equals(Name)
                                       select i;

            foreach (var apiResourceEntity in apiResourcesEntities)
            {
                apiResourceEntity.MapDataFromEntity();

                apiResources.Add(apiResourceEntity.ApiResource);
            }

            return Task.FromResult(apiResources.FirstOrDefault());
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var identityResources = new List<IdentityResource>();
            var identityResourcesEntities = from i in _context.IdentityResources
                                            where scopeNames.Contains(i.Name)
                                            select i;

            foreach (var identityResourceEntity in identityResourcesEntities)
            {
                identityResourceEntity.MapDataFromEntity();

                identityResources.Add(identityResourceEntity.IdentityResource);
            }

            return Task.FromResult(identityResources.AsEnumerable());
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            var apiResourcesEntities = _context.ApiResources.ToList();
            var identityResourcesEntities = _context.IdentityResources.ToList();

            var apiResources = new List<ApiResource>();
            var identityResources = new List<IdentityResource>();

            foreach (var apiResourceEntity in apiResourcesEntities)
            {
                apiResourceEntity.MapDataFromEntity();

                apiResources.Add(apiResourceEntity.ApiResource);
            }

            foreach (var identityResourceEntity in identityResourcesEntities)
            {
                identityResourceEntity.MapDataFromEntity();

                identityResources.Add(identityResourceEntity.IdentityResource);
            }

            var result = new Resources(identityResources, apiResources);
            return Task.FromResult(result);
        }

        public void CreateApiResource(ApiResourceEntity res)
        {
            _context.ApiResources.Add(res);
            _context.SaveChanges();
        }

        public void EditApiResource(ApiResourceEntity res)
        {
            _context.ApiResources.Update(res);
            _context.SaveChanges();
        }

        public void ApiResourceChangeState(string name, bool enable)
        {
            var apiResourcesEntity = (from i in _context.ApiResources
                                       where i.Name.Equals(name)
                                       select i).FirstOrDefault();
            apiResourcesEntity.ApiResource.Enabled = enable;
            _context.Update(apiResourcesEntity);
            _context.SaveChanges();
        }
    }
}
