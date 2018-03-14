using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IdentityServerWithAspIdAndEF.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using IdentityServerWithAspIdAndEF.Services;
using System.Security.Claims;
using PYS.IdentityServer.Security.Administration.Models.UserByClaimsViewModel;

namespace PYS.IdentityServer.Security.Administration.API
{
    [Produces("application/json")]
    [Route("api/UsersByClaims")]
    public class UsersByClaimsController : Controller
    {

        #region private variables
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;

        #endregion

        #region contructors

        public UsersByClaimsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            IEmailSender emailSender,
            ILogger<UsersByClaimsController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _logger = logger;
            _emailSender = emailSender;
        }


        #endregion
        
        // GET: api/UsersByClaims/5
        [HttpGet]
        public async Task<List<UserViewModel>> Get(string type,string value)
        {
            Claim claim = new Claim(type, value);
            List<UserViewModel> users = new List<UserViewModel>();
            List<ApplicationUser> list = (await _userManager.GetUsersForClaimAsync(claim)).ToList();
            foreach(ApplicationUser user in list) {
                users.Add(new UserViewModel() { Email = user.Email, UserName = user.UserName });
            }
            return users;
        }
    }
}
