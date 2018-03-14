using IdentityServerWithAspIdAndEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PYS.IdentityServer.Security.Administration.Authorize.Claims
{
    public class ClaimsViewModel : Pagination
    {
        public List<Claim> Claims { get; set; }
        public ApplicationUser User { get; set; }

    }
}
