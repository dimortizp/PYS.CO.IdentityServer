using IdentityServer4.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PYS.IdentityServer.Security.Administration.Authorize.Clients
{
    public class ClientViewModel
    {
        [Display(Name ="¿Id de usuario requerido para cierre de sesión?")]
        public bool BackChannelLogoutSessionRequired { get; set; }

        [Display(Name ="¿Incluir Claims de usuario en token?")]
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        [Display(Name ="Tiempo de vida de identity token (segundos)")]
        public int IdentityTokenLifetime { get; set; }

        [Display(Name ="Tiempo de vida de access token (segundos)")]
        public int AccessTokenLifetime { get; set; }

        [Display(Name ="Tempo de vida de authorization code (segundos)")]
        public int AuthorizationCodeLifetime { get; set; }

        [Display(Name ="Máximo tiempo de vida del refresh token (segundos)")]
        public int AbsoluteRefreshTokenLifetime { get; set; }

        [Display(Name ="Tiempo extendido de vida de refresh token (segundos)")]
        public int SlidingRefreshTokenLifetime { get; set; }

        [Display(Name ="Duración de concentimiento de usuario (segundos) (dejar vacío para no expirar nunca)")]
        public int? ConsentLifetime { get; set; }

        [Display(Name ="Uso del Refresh Token")]
        public TokenUsage RefreshTokenUsage { get; set; }

        [Display(Name ="Indicativo para actualizar refresh token")]
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        [Display(Name ="Expiración del Token")]
        public TokenExpiration RefreshTokenExpiration { get; set; }

        [Display(Name ="Tipo de Access Token")]
        public AccessTokenType AccessTokenType { get; set; }

        [Display(Name ="Habilitar login local")]
        public bool EnableLocalLogin { get; set; }

        [Display(Name ="")]
        public ICollection<string> IdentityProviderRestrictions { get; set; }

        [Display(Name ="Incluir JWT Id en access token")]
        public bool IncludeJwtId { get; set; }

        [Display(Name ="")]
        public ICollection<Claim> Claims { get; set; }

        [Display(Name ="¿Enviar claims del cliente?")]
        public bool AlwaysSendClientClaims { get; set; }

        [Display(Name ="Prefijo de los claims del cliente")]
        public string ClientClaimsPrefix { get; set; }

        [Display(Name ="Valor de Pair-Wise")]
        public string PairWiseSubjectSalt { get; set; }

        [Display(Name ="")]
        public ICollection<string> AllowedScopes { get; set; }

        [Display(Name ="¿Permite acceso offline?")]
        public bool AllowOfflineAccess { get; set; }

        [Display(Name ="")]
        public IDictionary<string, string> Properties { get; set; }

        [Display(Name ="URL Logout")]
        public string BackChannelLogoutUri { get; set; }

        [Display(Name ="Habilitado")]
        public bool Enabled { get; set; }

        [Display(Name ="ID de Cliente")]
        public string ClientId { get; set; }

        [Display(Name ="Tipo de protocolo")]
        public string ProtocolType { get; set; }

        [Display(Name ="")]
        public ICollection<Secret> ClientSecrets { get; set; }

        [Display(Name ="¿Requiere cliente?")]
        public bool RequireClientSecret { get; set; }

        [Display(Name ="Nombre de cliente")]
        public string ClientName { get; set; }

        [Display(Name ="URL Cliente")]
        public string ClientUri { get; set; }

        [Display(Name ="URL Logout")]
        public string LogoUri { get; set; }

        [Display(Name ="")]
        public ICollection<string> AllowedCorsOrigins { get; set; }

        [Display(Name ="¿Requiere consentimiento del usuario?")]
        public bool RequireConsent { get; set; }

        [Display(Name ="")]
        public ICollection<string> AllowedGrantTypes { get; set; }

        [Display(Name ="¿Requiere clave de autorización del usuario?")]
        public bool RequirePkce { get; set; }

        [Display(Name ="¿Permite texto plano en clave? (no recomendado)")]
        public bool AllowPlainTextPkce { get; set; }

        [Display(Name ="¿Permite generar códigos por navegador? (usado para autenticación por redirección)")]
        public bool AllowAccessTokensViaBrowser { get; set; }

        [Display(Name ="")]
        public ICollection<string> RedirectUris { get; set; }

        [Display(Name ="")]
        public ICollection<string> PostLogoutRedirectUris { get; set; }

        [Display(Name ="URL de redirección en Logout")]
        public string FrontChannelLogoutUri { get; set; }

        [Display(Name ="¿Debe enviar id de cliente en logut?")]
        public bool FrontChannelLogoutSessionRequired { get; set; }

        [Display(Name ="¿Permite recordar consetimiento de usuario?")]
        public bool AllowRememberConsent { get; set; }
    }
}
