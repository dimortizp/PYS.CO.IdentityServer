using IdentityServerWithAspIdAndEF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
