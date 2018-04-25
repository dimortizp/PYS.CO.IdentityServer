using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IdentityServerWithAspIdAndEF.Models;
using IdentityServer4.Services;
using PYS.IdentityServer.Security.Administration.ConfigurationStore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using IdentityServerWithAspIdAndEF.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;

namespace PYS.IdentityServer.Security.Administration.Authorize.Clients
{
    [Authorize(Policy = "AdministratorIS")]
    public class ClientController : Controller
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

        #endregion

        #region contructors

        public ClientController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStoreExtended clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            IEmailSender emailSender,
            ILogger<ClientController> logger)
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
        public ActionResult Index(string returnUrl)
        {
            List<Client> list = _clientStore.FindAllActiveClientsAsync().ToList();
            List<ClientViewModel> vm = new List<ClientViewModel>();
            if(list.Count > 0)
            {
                foreach (Client c in list)
                {
                    vm.Add(new ClientViewModel() { ClientId = c.ClientId, ClientUri = c.ClientUri });
                }
            }

            return View(vm);
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
        public ActionResult Create(ClientViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClientEntity clientEntity = new ClientEntity()
                    {
                        AbsoluteRefreshTokenLifetime = collection.AbsoluteRefreshTokenLifetime,
                        AccessTokenLifetime = collection.AccessTokenLifetime,
                        AccessTokenType = collection.AccessTokenType,
                        AllowAccessTokensViaBrowser = collection.AllowAccessTokensViaBrowser,
                        AllowedCorsOrigins = collection.AllowedCorsOrigins,
                        AllowedGrantTypes = collection.AllowedGrantTypes,
                        AllowedScopes = collection.AllowedScopes,
                        AllowOfflineAccess = collection.AllowOfflineAccess,
                        AllowPlainTextPkce = collection.AllowPlainTextPkce,
                        AllowRememberConsent = collection.AllowRememberConsent,
                        AlwaysIncludeUserClaimsInIdToken = collection.AlwaysIncludeUserClaimsInIdToken,
                        AlwaysSendClientClaims = collection.AlwaysSendClientClaims,
                        AuthorizationCodeLifetime = collection.AuthorizationCodeLifetime,
                        BackChannelLogoutSessionRequired = collection.BackChannelLogoutSessionRequired,
                        BackChannelLogoutUri = collection.BackChannelLogoutUri,
                        Claims = collection.Claims,
                        ClientClaimsPrefix = collection.ClientClaimsPrefix,
                        ClientId = collection.ClientId,
                        ClientName = collection.ClientName,
                        ClientSecrets = collection.ClientSecrets,
                        ClientUri = collection.ClientUri,
                        ConsentLifetime = collection.ConsentLifetime,
                        Enabled = collection.Enabled,
                        EnableLocalLogin = collection.EnableLocalLogin,
                        FrontChannelLogoutSessionRequired = collection.FrontChannelLogoutSessionRequired,
                        FrontChannelLogoutUri = collection.FrontChannelLogoutUri,
                        IdentityProviderRestrictions = collection.IdentityProviderRestrictions,
                        IdentityTokenLifetime = collection.IdentityTokenLifetime,
                        IncludeJwtId = collection.IncludeJwtId,
                        LogoUri = collection.LogoUri,
                        PairWiseSubjectSalt = collection.PairWiseSubjectSalt,
                        PostLogoutRedirectUris = collection.PostLogoutRedirectUris,
                        Properties = collection.Properties,
                        ProtocolType = collection.ProtocolType,
                        RedirectUris = collection.RedirectUris,
                        RefreshTokenExpiration = collection.RefreshTokenExpiration,
                        RefreshTokenUsage = collection.RefreshTokenUsage,
                        RequireClientSecret = collection.RequireClientSecret,
                        RequireConsent = collection.RequireConsent,
                        RequirePkce = collection.RequirePkce,
                        SlidingRefreshTokenLifetime = collection.SlidingRefreshTokenLifetime,
                        UpdateAccessTokenClaimsOnRefresh = collection.UpdateAccessTokenClaimsOnRefresh
                    };

                    _clientStore.CreateClient(clientEntity);

                    return RedirectToAction(nameof(Index));
                }

                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(string id)
        {
            Task<Client> clientResponse = _clientStore.FindClientByIdAsync(id);
            Client client = clientResponse.Result;

            if(client!= null)
            {
                ClientViewModel vm = new ClientViewModel()
                {
                    AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime,
                    AccessTokenLifetime = client.AccessTokenLifetime,
                    AccessTokenType = client.AccessTokenType,
                    AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
                    AllowedCorsOrigins = client.AllowedCorsOrigins,
                    AllowedGrantTypes = client.AllowedGrantTypes,
                    AllowedScopes = client.AllowedScopes,
                    AllowOfflineAccess = client.AllowOfflineAccess,
                    AllowPlainTextPkce = client.AllowPlainTextPkce,
                    AllowRememberConsent = client.AllowRememberConsent,
                    AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken,
                    AlwaysSendClientClaims = client.AlwaysSendClientClaims,
                    AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
                    BackChannelLogoutSessionRequired = client.BackChannelLogoutSessionRequired,
                    BackChannelLogoutUri = client.BackChannelLogoutUri,
                    Claims = client.Claims,
                    ClientClaimsPrefix = client.ClientClaimsPrefix,
                    ClientId = client.ClientId,
                    ClientName = client.ClientName,
                    ClientSecrets = client.ClientSecrets,
                    ClientUri = client.ClientUri,
                    ConsentLifetime = client.ConsentLifetime,
                    Enabled = client.Enabled,
                    EnableLocalLogin = client.EnableLocalLogin,
                    FrontChannelLogoutSessionRequired = client.FrontChannelLogoutSessionRequired,
                    FrontChannelLogoutUri = client.FrontChannelLogoutUri,
                    IdentityProviderRestrictions = client.IdentityProviderRestrictions,
                    IdentityTokenLifetime = client.IdentityTokenLifetime,
                    IncludeJwtId = client.IncludeJwtId,
                    LogoUri = client.LogoUri,
                    PairWiseSubjectSalt = client.PairWiseSubjectSalt,
                    PostLogoutRedirectUris = client.PostLogoutRedirectUris,
                    Properties = client.Properties,
                    ProtocolType = client.ProtocolType,
                    RedirectUris = client.RedirectUris,
                    RefreshTokenExpiration = client.RefreshTokenExpiration,
                    RefreshTokenUsage = client.RefreshTokenUsage,
                    RequireClientSecret = client.RequireClientSecret,
                    RequireConsent = client.RequireConsent,
                    RequirePkce = client.RequirePkce,
                    SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
                    UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh
                };
                return View(vm);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClientEntity clientEntity = new ClientEntity()
                    {
                        AbsoluteRefreshTokenLifetime = collection.AbsoluteRefreshTokenLifetime,
                        AccessTokenLifetime = collection.AccessTokenLifetime,
                        AccessTokenType = collection.AccessTokenType,
                        AllowAccessTokensViaBrowser = collection.AllowAccessTokensViaBrowser,
                        AllowedCorsOrigins = collection.AllowedCorsOrigins,
                        AllowedGrantTypes = collection.AllowedGrantTypes,
                        AllowedScopes = collection.AllowedScopes,
                        AllowOfflineAccess = collection.AllowOfflineAccess,
                        AllowPlainTextPkce = collection.AllowPlainTextPkce,
                        AllowRememberConsent = collection.AllowRememberConsent,
                        AlwaysIncludeUserClaimsInIdToken = collection.AlwaysIncludeUserClaimsInIdToken,
                        AlwaysSendClientClaims = collection.AlwaysSendClientClaims,
                        AuthorizationCodeLifetime = collection.AuthorizationCodeLifetime,
                        BackChannelLogoutSessionRequired = collection.BackChannelLogoutSessionRequired,
                        BackChannelLogoutUri = collection.BackChannelLogoutUri,
                        Claims = collection.Claims,
                        ClientClaimsPrefix = collection.ClientClaimsPrefix,
                        ClientId = collection.ClientId,
                        ClientName = collection.ClientName,
                        ClientSecrets = collection.ClientSecrets,
                        ClientUri = collection.ClientUri,
                        ConsentLifetime = collection.ConsentLifetime,
                        Enabled = collection.Enabled,
                        EnableLocalLogin = collection.EnableLocalLogin,
                        FrontChannelLogoutSessionRequired = collection.FrontChannelLogoutSessionRequired,
                        FrontChannelLogoutUri = collection.FrontChannelLogoutUri,
                        IdentityProviderRestrictions = collection.IdentityProviderRestrictions,
                        IdentityTokenLifetime = collection.IdentityTokenLifetime,
                        IncludeJwtId = collection.IncludeJwtId,
                        LogoUri = collection.LogoUri,
                        PairWiseSubjectSalt = collection.PairWiseSubjectSalt,
                        PostLogoutRedirectUris = collection.PostLogoutRedirectUris,
                        Properties = collection.Properties,
                        ProtocolType = collection.ProtocolType,
                        RedirectUris = collection.RedirectUris,
                        RefreshTokenExpiration = collection.RefreshTokenExpiration,
                        RefreshTokenUsage = collection.RefreshTokenUsage,
                        RequireClientSecret = collection.RequireClientSecret,
                        RequireConsent = collection.RequireConsent,
                        RequirePkce = collection.RequirePkce,
                        SlidingRefreshTokenLifetime = collection.SlidingRefreshTokenLifetime,
                        UpdateAccessTokenClaimsOnRefresh = collection.UpdateAccessTokenClaimsOnRefresh
                    };

                    _clientStore.EditClient(clientEntity);

                    return RedirectToAction(nameof(Index));
                }

                return View(collection);

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

    }
}