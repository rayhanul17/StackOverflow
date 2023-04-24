using Microsoft.AspNetCore.Identity;
using StackOverflow.DAL.Entities.Membership;
using ApplicationUserDto = StackOverflow.Services.DTOs.Membership.AppplicationUser;

namespace StackOverflow.Services.Services.Membership
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUserDto user);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserAsync();
        Task<SignInResult> PasswordSignInAsync(ApplicationUserDto user);
        Task<IList<string>> GetCurrentUserRolesAsync(string email);
        Task SignInAsync(string email);
        Task SignOutAsync();
        bool IsAuthenticated();
        string GetUserId();
    }
}
