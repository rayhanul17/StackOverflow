using Microsoft.AspNetCore.Identity;
using StackOverflow.DAL.Entities.Membership;

namespace StackOverflow.Services.Seeds;

public static class ApplicationUserSeed
{
    public static ApplicationUser[] Users
    {
        get
        {
            var rootUser = new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "",
                UserName = "admin@sof.com",
                NormalizedUserName = "ADMIN@SOF.COM",
                Email = "admin@sof.com",
                NormalizedEmail = "ADMIN@SOF.COM",
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true
            };
            var password = new PasswordHasher<ApplicationUser>();
            var rootHashed = password.HashPassword(rootUser, "admin@sof");
            rootUser.PasswordHash = rootHashed;

            return new ApplicationUser[]
            {
                rootUser,
            };
        }
    }
}
