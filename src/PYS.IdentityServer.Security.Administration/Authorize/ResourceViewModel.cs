using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PYS.IdentityServer.Security.Administration.Authorize
{
    public class ResourceViewModel
    {
        [Display (Name = "¿Habilitado?")]
        public bool Enabled { get; set; }

        [Display (Name = "Nombre")]
        public string Name { get; set; }

        [Display (Name = "Nombre a mostrar")]
        public string DisplayName { get; set; }

        [Display (Name = "Descripción")]
        public string Description { get; set; }

        [Display (Name = "Características")]
        public ICollection<string> UserClaims { get; set; }
    }
}
