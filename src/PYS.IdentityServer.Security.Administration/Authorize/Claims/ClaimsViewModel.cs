using IdentityServerWithAspIdAndEF.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PYS.IdentityServer.Security.Administration.Authorize.Claims
{
    public class ClaimsViewModel : Pagination
    {
        [Display (Name = "")]
        public List<Claim> Claims { get; set; }
        [Display (Name = "")]
        public ApplicationUser User { get; set; }

    }
}
