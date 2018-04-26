using IdentityServer4.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PYS.IdentityServer.Security.Administration.ConfigurationStore
{
    public class IdentityResourceEntity
    {
        public bool Enabled { get; set; }

        [Key]
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public List<string> UserClaims { get; set; }

        public bool Required { get; set; }

        public bool Emphasize { get; set; }

        public bool ShowInDiscoveryDocument { get; set; }

        [NotMapped]
        public IdentityResource IdentityResource { get; set; }

        public void AddDataToEntity()
        {
            Description = IdentityResource.Description;
            DisplayName = IdentityResource.DisplayName;
            Emphasize = IdentityResource.Emphasize;
            Enabled = IdentityResource.Enabled;
            Name = IdentityResource.Name;
            Required = IdentityResource.Required;
            ShowInDiscoveryDocument = IdentityResource.ShowInDiscoveryDocument;
            UserClaims = IdentityResource.UserClaims.ToList();
        }

        public void MapDataFromEntity()
        {
            IdentityResource = new IdentityResource()
            {
                Description = Description,
                DisplayName = DisplayName,
                Emphasize = Emphasize,
                Enabled = Enabled,
                Name = Name,
                Required = Required,
                ShowInDiscoveryDocument = ShowInDiscoveryDocument,
                UserClaims = UserClaims
            };
            Name = IdentityResource.Name;
        }
    }
}