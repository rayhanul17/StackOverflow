namespace StackOverflow.Services.DTOs.Membership;

public class AppplicationUser
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool EmailConfirmed { get; set; }
    public string Password { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string Code { get; set; } = null!;
    public bool RememberMe { get; set; }
    public string ReturnUrl { get; set; } = null!;
}
