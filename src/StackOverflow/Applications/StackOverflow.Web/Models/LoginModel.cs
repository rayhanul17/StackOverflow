using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StackOverflow.Services.DTOs.Membership;
using StackOverflow.Services.Services.Membership;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models;

public class LoginModel : BaseModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
    public string? ErrorMessage { get; set; }

    private IAccountService _accountService = null!;

    public LoginModel()
    {

    }

    public LoginModel(IMapper mapper, ILifetimeScope scope, IAccountService accountService)
        : base(mapper, scope)
    {
        _accountService = accountService;
    }
    public override void ResolveDependency(ILifetimeScope scope)
    {        
        _scope = scope;
        _accountService = _scope.Resolve<IAccountService>();
        base.ResolveDependency(scope);
    }

    public async Task<SignInResult> PasswordSignInAsync()
    {
        var user = _mapper.Map<ApplicationUser>(this);
        return await _accountService.PasswordSignInAsync(user);
    }

    public async Task<IList<string>> GetCurrentUserRolesAsync()
    {
        return await _accountService.GetCurrentUserRolesAsync(Email);
    }
}
