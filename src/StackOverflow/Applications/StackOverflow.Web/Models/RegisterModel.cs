using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StackOverflow.Services.DTOs.Membership;
using StackOverflow.Services.Services.Membership;
using StackOverflow.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models
{
    public class RegisterModel : BaseModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;
        public string? ReturnUrl { get; set; } = null!;

        private IAccountService _accountService = null!;

        public RegisterModel()
        {

        }

        public RegisterModel(IMapper mapper, ILifetimeScope scope, IAccountService accountService)
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

        public async Task<IdentityResult> CreateAccountAsync()
        {
            var user = _mapper.Map<ApplicationUser>(this);
            var result = await _accountService.CreateUserAsync(user);
            return result;
        }

        public async Task SignInAsync()
        {
            await _accountService.SignInAsync(Email);
        }
    }
}
