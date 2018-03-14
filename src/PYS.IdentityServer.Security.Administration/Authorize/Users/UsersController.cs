using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IdentityServerWithAspIdAndEF.Models;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using IdentityServerWithAspIdAndEF.Services;

namespace PYS.IdentityServer.Security.Administration.Authorize.Users
{
    public class UsersController : Controller
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

        public UsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            IEmailSender emailSender,
            ILogger<UsersController> logger)
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


        #region CRUD

        [HttpGet]
        public ActionResult Index()
        {
            List<ApplicationUser> users = _userManager.Users.ToList();
            UsersViewModel uvm = new UsersViewModel() { Users = users };
            return View(uvm);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            Task<ApplicationUser> user = _userManager.FindByIdAsync(id);
            return View(user.Result);
        }

        [HttpGet]
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

        [HttpGet]
        public ActionResult Edit(string id)
        {
            Task<ApplicationUser> user = _userManager.FindByIdAsync(id);
            return View(user.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser unmodifiedUser = await _userManager.FindByIdAsync(user.Id);
                    unmodifiedUser.Document = user.Document;
                    unmodifiedUser.Names = user.Names;
                    unmodifiedUser.Address = user.Address;
                    unmodifiedUser.Email = user.Email;
                    unmodifiedUser.PhoneNumber = user.PhoneNumber;

                    IdentityResult result = await _userManager.UpdateAsync(unmodifiedUser);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User updated", user);
                        return RedirectToAction(nameof(Index));

                    }
                    AddErrors(result);

                }
                // If we got this far, something failed, redisplay form
                return View(user);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Error editando el usuario ", user);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
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