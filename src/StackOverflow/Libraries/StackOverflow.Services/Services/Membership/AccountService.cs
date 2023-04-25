using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using NHibernate;
using StackOverflow.DAL.Entities.Membership;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using ApplicationUserDto = StackOverflow.Services.DTOs.Membership.ApplicationUser;

namespace StackOverflow.Services.Services.Membership;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserRoleManager _userRoleManager;
    private ISession _session;
    private readonly IUrlHelper _urlHelper;
    private readonly IActionContextAccessor _contextAccessor;
    private readonly IMapper _mapper;

    public AccountService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserRoleManager userRoleManager,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor contextAccessor,
        IMapper mapper, ISession session)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userRoleManager = userRoleManager;
        _urlHelper = urlHelperFactory.GetUrlHelper(contextAccessor.ActionContext!);
        _contextAccessor = contextAccessor;
        _mapper = mapper;
        _session = session;
    }

    public async Task<IdentityResult> CreateUserAsync(ApplicationUserDto user)
    {
        var userEntity = _mapper.Map<ApplicationUser>(user);
        var result = await _userManager.CreateAsync(userEntity, user.Password);
        if (result.Succeeded)
        {
            _userRoleManager.AddToRoles(userEntity, new string[] { "User" });
        }
        return result;
    }

    public async Task<IList<string>> GetCurrentUserRolesAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var roles = _userRoleManager.GetRoles(user);
        return roles;
    }

    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<ApplicationUser> GetUserAsync()
    {
        return await _userManager.FindByIdAsync(GetUserId());
    }

    public string GetUserId()
    {
        return GetUser!.FindFirstValue(ClaimTypes.NameIdentifier);
    }
    public ClaimsPrincipal GetUser => _contextAccessor.ActionContext!.HttpContext.User;

    public bool IsAuthenticated()
    {
        return GetUser!.Identity!.IsAuthenticated;
    }

    public async Task<SignInResult> PasswordSignInAsync(ApplicationUserDto user)
    {
        return await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe,
            lockoutOnFailure: false);
    }

    public async Task SignInAsync(string email)
    {
        await _signInManager.SignInAsync(await _userManager.FindByEmailAsync(email), isPersistent: false);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
