using IdentityServer4.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PYS.IdentityServer.Security.Administration.ConfigurationStore
{
    public class ApiResourceEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string DisplayName { get; set; }

        public bool Enabled { get; set; }

        [NotMapped]
        public ApiResource ApiResource { get; set; }

        public void AddDataToEntity()
        {
            Description = ApiResource.Description;
            DisplayName = ApiResource.DisplayName;
            Enabled = ApiResource.Enabled;
            Name = ApiResource.Name;
        }

        public void MapDataFromEntity()
        {
            ApiResource = new ApiResource();
            ApiResource.Description = Description;
            ApiResource.DisplayName = DisplayName;
            ApiResource.Enabled = Enabled;
            ApiResource.Name = Name;
        }
    }
}