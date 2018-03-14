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
using IdentityServer4.Models;

namespace PYS.IdentityServer.Security.Administration.Authorize.Claims
{
    public class ClaimsController : Controller
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
        private readonly IResourceStore _resourceStore;

        #endregion

        #region contructors

        public ClaimsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            IEmailSender emailSender,
            ILogger<ClaimsController> logger,
            IResourceStore resourceStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _logger = logger;
            _emailSender = emailSender;
            _resourceStore = resourceStore;
        }


        #endregion

        #region CRUD

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string UserId, string Type, string Value)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(UserId);
                Claim claim = (await _userManager.GetClaimsAsync(user)).Where(x => x.Type == Type && x.Value == Value).FirstOrDefault();
                if(claim != null)
                {
                    IdentityResult result = await _userManager.RemoveClaimAsync(user, claim);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(GetUserClaims),new { Id = user.Id });

                    }
                }
                return View();

                // TODO: Add delete logic here

            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Extended Functionalities

        public async Task<IActionResult> GetUserClaims(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            List<Claim> res = (await _userManager.GetClaimsAsync(user)).ToList();

            ClaimsViewModel vm = new ClaimsViewModel() { User = user, Claims = res };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AddAplicationToUser(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            var resources = await  _resourceStore.GetAllEnabledResourcesAsync();

            AddClaimsToUserViewModel vm = new AddClaimsToUserViewModel()
            {
                Type = "Application",
                UserName = user.UserName,
                ApiResources = resources.ApiResources.ToList()
            };

            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAplicationToUser(AddClaimsToUserViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(vm.UserName);
                    Claim c = new Claim(vm.Type, vm.Value);
                    IdentityResult result = await _userManager.AddClaimAsync(user,c);

                    if(result.Succeeded)
                        return RedirectToAction(nameof(GetUserClaims),new { Id = user.Id });

                    AddErrors(result);

                }
                return View(vm);

            }
            catch(Exception e)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddClaimsToUser(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            var resources = await _resourceStore.GetAllEnabledResourcesAsync();

            AddClaimsToUserViewModel vm = new AddClaimsToUserViewModel()
            {
                UserName = user.UserName,
                ApiResources = resources.ApiResources.ToList()
            };

            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClaimsToUser(AddClaimsToUserViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(vm.UserName);
                    Claim c = new Claim(vm.Type, vm.Value);
                    IdentityResult result = await _userManager.AddClaimAsync(user, c);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(GetUserClaims), new { Id = user.Id });

                    AddErrors(result);

                }
                return View(vm);

            }
            catch (Exception e)
            {
                return View();
            }
        }


        #endregion


        #region private methods
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}