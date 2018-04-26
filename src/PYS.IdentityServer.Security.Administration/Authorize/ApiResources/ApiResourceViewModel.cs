using IdentityServer4.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PYS.IdentityServer.Security.Administration.Authorize.ApiResources
{
    public class ApiResourceViewModel : ResourceViewModel
    {
        [Display (Name = "")]
        public ICollection<Secret> ApiSecrets { get; set; }

        [Display (Name = "")]
        public ICollection<Scope> Scopes { get; set; }
    }
}
