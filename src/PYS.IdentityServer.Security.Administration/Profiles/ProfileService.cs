using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServerWithAspIdAndEF.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerWithAspIdAndEF.Profiles
{
    public class ProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //>Processing
            var user = _userManager.GetUserAsync(context.Subject).Result;
            //var roles = _userManager.GetRolesAsync(user).Result;
            var claims = _userManager.GetClaimsAsync(user).Result;

            claims.Add(new Claim("user_name", user.UserName));

            context.IssuedClaims.AddRange(claims);

            //>Return
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            //>Processing
            var user = _userManager.GetUserAsync(context.Subject).Result;

            context.IsActive = (user != null);

            //>Return
            return Task.FromResult(0);
        }

    }
}
