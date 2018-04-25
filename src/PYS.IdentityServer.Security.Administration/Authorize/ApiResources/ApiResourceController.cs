using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.Services;
using IdentityServerWithAspIdAndEF.Models;
using Microsoft.AspNetCore.Identity;
using PYS.IdentityServer.Security.Administration.ConfigurationStore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using IdentityServerWithAspIdAndEF.Services;
using IdentityServer4.Models;
using PYS.IdentityServer.Security.Administration.Authorize.ApiResources;
using Microsoft.AspNetCore.Authorization;

namespace PYS.IdentityServer.Security.Administration.Authorize
{
    [Authorize(Policy = "AdministratorIS")]
    public class ApiResourceController : Controller
    {

        #region private variables
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStoreExtended _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;
        private readonly IResourceStoreExtended _resourceStoreExtended;

        #endregion

        #region contructors

        public ApiResourceController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStoreExtended clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            IEmailSender emailSender,
            ILogger<ApiResourceController> logger,
            IResourceStoreExtended resourceStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _logger = logger;
            _emailSender = emailSender;
            _resourceStoreExtended = resourceStore;
        }


        #endregion

        #region CRUD

        public async Task<IActionResult> Index()
        {
            Resources resources = (await _resourceStoreExtended.GetAllResourcesAsync());
            List<ApiResourceViewModel> list = new List<ApiResourceViewModel>();
            foreach (ApiResource res in resources.ApiResources)
            {
                list.Add(new ApiResourceViewModel() {
                    ApiSecrets = res.ApiSecrets,
                    Description = res.Description,
                    DisplayName = res.DisplayName,
                    Enabled = res.Enabled,
                    Name = res.Name,
                    Scopes = res.Scopes,
                    UserClaims = res.UserClaims
                });
            }
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApiResourceViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApiResourceEntity resource = new ApiResourceEntity()
                    {
                        //ApiSecrets = collection.ApiSecrets,
                        //Scopes = collection.Scopes,
                        Name = collection.Name,
                        Enabled = collection.Enabled,
                        Description = collection.Description,
                        DisplayName = collection.DisplayName,
                        //UserClaims = collection.UserClaims
                    };
                    // TODO: Add update logic here

                    _resourceStoreExtended.CreateApiResource(resource);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(collection);
                }

            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            ApiResource resource =await _resourceStoreExtended.FindApiResourcesByNameAsync(id);
            ApiResourceViewModel vm = new ApiResourceViewModel()
            {
                ApiSecrets = resource.ApiSecrets,
                UserClaims = resource.UserClaims,
                Description = resource.Description,
                DisplayName = resource.DisplayName,
                Enabled = resource.Enabled,
                Name = resource.Name,
                Scopes = resource.Scopes
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApiResourceViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApiResourceEntity resource = new ApiResourceEntity()
                    {
                        //ApiSecrets = collection.ApiSecrets,
                        //Scopes = collection.Scopes,
                        Name = collection.Name,
                        //Enabled = collection.Enabled,
                        Description = collection.Description,
                        DisplayName = collection.DisplayName,
                        //UserClaims = collection.UserClaims
                    };
                    _resourceStoreExtended.EditApiResource(resource);

                    return RedirectToAction(nameof(Index));
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Disable(string id)
        {
            _resourceStoreExtended.ApiResourceChangeState(id, false);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Enable(string id)
        {
            ApiResource resource = await _resourceStoreExtended.FindApiResourcesByNameAsync(id);

            ApiResourceEntity entity = new ApiResourceEntity()
            {
                ApiResource = resource
            };

            entity.AddDataToEntity();
            entity.Enabled = true;

            _resourceStoreExtended.EditApiResource(entity);

            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}