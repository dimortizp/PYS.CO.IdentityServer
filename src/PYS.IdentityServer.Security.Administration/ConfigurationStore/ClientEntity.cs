using IdentityServer4.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PYS.IdentityServer.Security.Administration.ConfigurationStore
{
    public class ClientEntity
    {
        [Key]
        public string ClientId { get; set; }

        public bool BackChannelLogoutSessionRequired { get; set; }

        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        public int IdentityTokenLifetime { get; set; }

        public int AccessTokenLifetime { get; set; }

        public int AuthorizationCodeLifetime { get; set; }

        public int AbsoluteRefreshTokenLifetime { get; set; }

        public int SlidingRefreshTokenLifetime { get; set; }

        public int? ConsentLifetime { get; set; }

        public TokenUsage RefreshTokenUsage { get; set; }

        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }


        public TokenExpiration RefreshTokenExpiration { get; set; }

        public AccessTokenType AccessTokenType { get; set; }

        public bool EnableLocalLogin { get; set; }

        [NotMapped]
        public ICollection<string> IdentityProviderRestrictions { get; set; }

        public bool IncludeJwtId { get; set; }

        [NotMapped]
        public ICollection<Claim> Claims { get; set; }

        public bool AlwaysSendClientClaims { get; set; }

        public string ClientClaimsPrefix { get; set; }

        public string PairWiseSubjectSalt { get; set; }

        [NotMapped]
        public ICollection<string> AllowedScopes { get; set; }

        public bool AllowOfflineAccess { get; set; }

        [NotMapped]
        public IDictionary<string, string> Properties { get; set; }

        public string BackChannelLogoutUri { get; set; }

        public bool Enabled { get; set; }

        public string ProtocolType { get; set; }

        [NotMapped]
        public ICollection<Secret> ClientSecrets { get; set; }

        public bool RequireClientSecret { get; set; }

        public string ClientName { get; set; }

        public string ClientUri { get; set; }

        public string LogoUri { get; set; }

        [NotMapped]
        public ICollection<string> AllowedCorsOrigins { get; set; }

        public bool RequireConsent { get; set; }

        [NotMapped]
        public ICollection<string> AllowedGrantTypes { get; set; }

        public bool RequirePkce { get; set; }

        public bool AllowPlainTextPkce { get; set; }

        public bool AllowAccessTokensViaBrowser { get; set; }

        [NotMapped]
        public ICollection<string> RedirectUris { get; set; }

        [NotMapped]
        public ICollection<string> PostLogoutRedirectUris { get; set; }

        public string FrontChannelLogoutUri { get; set; }

        public bool FrontChannelLogoutSessionRequired { get; set; }

        public bool AllowRememberConsent { get; set; }

        [NotMapped]
        public Client Client { get; set; }

        public void AddDataToEntity()
        {
            AbsoluteRefreshTokenLifetime = Client.AbsoluteRefreshTokenLifetime;
            AccessTokenLifetime = Client.AccessTokenLifetime;
            AccessTokenType = Client.AccessTokenType;
            AllowAccessTokensViaBrowser = Client.AllowAccessTokensViaBrowser;
            AllowedCorsOrigins = Client.AllowedCorsOrigins;
            AllowedGrantTypes = Client.AllowedGrantTypes;
            AllowedScopes = Client.AllowedScopes;
            AllowOfflineAccess = Client.AllowOfflineAccess;
            AllowPlainTextPkce = Client.AllowPlainTextPkce;
            AllowRememberConsent = Client.AllowRememberConsent;
            AlwaysIncludeUserClaimsInIdToken = Client.AlwaysIncludeUserClaimsInIdToken;
            AlwaysSendClientClaims = Client.AlwaysSendClientClaims;
            AuthorizationCodeLifetime = Client.AuthorizationCodeLifetime;
            BackChannelLogoutSessionRequired = Client.BackChannelLogoutSessionRequired;
            BackChannelLogoutUri = Client.BackChannelLogoutUri;
            Claims = Client.Claims;
            ClientClaimsPrefix = Client.ClientClaimsPrefix;
            ClientId = Client.ClientId;
            ClientName = Client.ClientName;
            ClientSecrets = Client.ClientSecrets;
            ClientUri = Client.ClientUri;
            ConsentLifetime = Client.ConsentLifetime;
            Enabled = Client.Enabled;
            EnableLocalLogin = Client.EnableLocalLogin;
            FrontChannelLogoutSessionRequired = Client.FrontChannelLogoutSessionRequired;
            FrontChannelLogoutUri = Client.FrontChannelLogoutUri;
            IdentityProviderRestrictions = Client.IdentityProviderRestrictions;
            IdentityTokenLifetime = Client.IdentityTokenLifetime;
            IncludeJwtId = Client.IncludeJwtId;
            LogoUri = Client.LogoUri;
            PairWiseSubjectSalt = Client.PairWiseSubjectSalt;
            PostLogoutRedirectUris = Client.PostLogoutRedirectUris;
            Properties = Client.Properties;
            ProtocolType = Client.ProtocolType;
            RedirectUris = Client.RedirectUris;
            RefreshTokenExpiration = Client.RefreshTokenExpiration;
            RefreshTokenUsage = Client.RefreshTokenUsage;
            RequireClientSecret = Client.RequireClientSecret;
            RequireConsent = Client.RequireConsent;
            RequirePkce = Client.RequirePkce;
            SlidingRefreshTokenLifetime = Client.SlidingRefreshTokenLifetime;
            UpdateAccessTokenClaimsOnRefresh = Client.UpdateAccessTokenClaimsOnRefresh;
            ClientId = Client.ClientId;
        }

        public void MapDataFromEntity()
        {
            Client = new Client()
            {
                AbsoluteRefreshTokenLifetime = AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = AccessTokenLifetime,
                AccessTokenType = AccessTokenType,
                AllowAccessTokensViaBrowser = AllowAccessTokensViaBrowser,
                //AllowedCorsOrigins = AllowedCorsOrigins,
                //AllowedGrantTypes = AllowedGrantTypes,
                //AllowedScopes = AllowedScopes,
                AllowOfflineAccess = AllowOfflineAccess,
                AllowPlainTextPkce = AllowPlainTextPkce,
                AllowRememberConsent = AllowRememberConsent,
                AlwaysIncludeUserClaimsInIdToken = AlwaysIncludeUserClaimsInIdToken,
                AlwaysSendClientClaims = AlwaysSendClientClaims,
                AuthorizationCodeLifetime = AuthorizationCodeLifetime,
                BackChannelLogoutSessionRequired = BackChannelLogoutSessionRequired,
                BackChannelLogoutUri = BackChannelLogoutUri,
                Claims = Claims,
                ClientClaimsPrefix = ClientClaimsPrefix,
                ClientId = ClientId,
                ClientName = ClientName,
                ClientSecrets = ClientSecrets,
                ClientUri = ClientUri,
                ConsentLifetime = ConsentLifetime,
                Enabled = Enabled,
                EnableLocalLogin = EnableLocalLogin,
                FrontChannelLogoutSessionRequired = FrontChannelLogoutSessionRequired,
                FrontChannelLogoutUri = FrontChannelLogoutUri,
                IdentityProviderRestrictions = IdentityProviderRestrictions,
                IdentityTokenLifetime = IdentityTokenLifetime,
                IncludeJwtId = IncludeJwtId,
                LogoUri = LogoUri,
                PairWiseSubjectSalt = PairWiseSubjectSalt,
                PostLogoutRedirectUris = PostLogoutRedirectUris,
                Properties = Properties,
                ProtocolType = ProtocolType,
                RedirectUris = RedirectUris, 
                RefreshTokenExpiration = RefreshTokenExpiration,
                RefreshTokenUsage = RefreshTokenUsage,
                RequireClientSecret = RequireClientSecret,
                RequireConsent = RequireConsent,
                RequirePkce = RequirePkce,
                SlidingRefreshTokenLifetime = SlidingRefreshTokenLifetime,
                UpdateAccessTokenClaimsOnRefresh = UpdateAccessTokenClaimsOnRefresh
            };
            ClientId = Client.ClientId;
        }
    }
}
