using IdentityServerWithAspIdAndEF.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PYS.IdentityServer.Security.Administration.Authorize.Users
{
    public class UsersViewModel : Pagination
    {
        [Display (Name = "Usuarios")]
        public List<ApplicationUser> Users { get; set; }

    }
}
