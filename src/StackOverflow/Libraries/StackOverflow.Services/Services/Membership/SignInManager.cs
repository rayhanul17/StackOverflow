using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackOverflow.DAL.Entities.Membership;

namespace StackOverflow.Services.Services.Membership
{
    public class SignInManager : SignInManager<ApplicationUser>
    {
        public SignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes,
            IUserConfirmation<ApplicationUser> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }
    }
}
