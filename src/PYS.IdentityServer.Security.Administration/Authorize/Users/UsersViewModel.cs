using IdentityServerWithAspIdAndEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PYS.IdentityServer.Security.Administration.Authorize.Users
{
    public class UsersViewModel : Pagination
    {
        public List<ApplicationUser> Users { get; set; }

    }
}
