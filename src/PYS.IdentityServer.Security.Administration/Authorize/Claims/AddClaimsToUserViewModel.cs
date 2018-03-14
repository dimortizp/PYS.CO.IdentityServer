using IdentityServer4.Models;
using IdentityServerWithAspIdAndEF.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PYS.IdentityServer.Security.Administration.Authorize.Claims
{
    public class AddClaimsToUserViewModel
    {
        [Display(Name ="Tipo")]
        public string Type { get; set; }

        [Display(Name ="Aplicación")]
        [Required(ErrorMessage = "Seleccione una aplicaciónf")]
        public string Value { get; set; }
        public string UserName { get; set; }
        public List<ApiResource> ApiResources { get; set; }
        




    }
}
