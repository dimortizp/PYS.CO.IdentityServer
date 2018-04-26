using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IdentityServerWithAspIdAndEF.Models;
using System.Security.Claims;
using PYS.IdentityServer.Security.Administration.Extensions;
using PYS.IdentityServer.Security.Administration.Models.UserByClaimsViewModel;

namespace PYS.IdentityServer.Security.Administration.API
{
    [Produces("application/json")]
    [Route("api/UsersByClaims")]
    public class UsersByClaimsController : Controller
    {

        #region private variables
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region contructors

        public UsersByClaimsController(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        #endregion

        #region public methods

        [HttpGet]
        public PaginatedList<UserViewModel> Get(string type,string value,int pageIndex = 1, int pageSize = 10, string userName = null)
        {
            var claim = new Claim(type, value);
            var users = new List<UserViewModel>();
            var list = (_userManager.GetUsersForClaimAsync(claim).Result).ToList();
            foreach(var user in list) {
                users.Add(new UserViewModel() { Email = user.Email, UserName = user.UserName });
            }
            if(userName != null)
            users = users.Where(x => x.UserName.ToLowerInvariant().Contains(userName.ToLowerInvariant())).ToList();
            return PaginatedList<UserViewModel>.Create(users, pageIndex, pageSize);
        }
    #endregion

    }
}
