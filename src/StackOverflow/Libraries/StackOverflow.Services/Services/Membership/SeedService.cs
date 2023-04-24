using NHibernate;
using StackOverflow.DAL.Entities.Membership;
using StackOverflow.Services.Seeds;

namespace StackOverflow.Services.Services.Membership;

public class SeedService : ISeedService
{
    private readonly ISession _session;

    public SeedService(ISession session)
    {
        _session = session;
    }

    public void Seeds()
    {
        ApplicationUser();
        Role();
        AdminRole();
    }

    private void ApplicationUser()
    {
        var users = ApplicationUserSeed.Users;
        foreach (var user in users)
        {
            var count = _session.Query<ApplicationUser>().Where(t => t.UserName == user.UserName).Count();

            if (count > 0)
                continue;

            _session.Save(user);
            _session.Flush();
        }
    }

    private void Role()
    {
        var roles = RoleSeed.Roles;
        foreach (var role in roles)
        {
            var count = _session.Query<AppRole>().Where(t => t.Name == role.Name).Count();

            if (count > 0)
                continue;

            _session.Save(role);
            _session.Flush();
        }
    }

    private void AdminRole()
    {
        var count = _session.Query<UserRole>().Where(t => t.Role.Name == "Admin").Count();
        if (count > 0)
            return;

        var appUser = _session.Query<ApplicationUser>().Where(t => t.UserName == "admin@edx.com").FirstOrDefault();

        if (appUser == null)
            return;

        var role = _session.Query<AppRole>().Where(t => t.Name == "Admin").FirstOrDefault();

        if (role == null)
            return;

        var userRole = new UserRole
        {
            ApplicationUser = appUser,
            Role = role
        };
        _session.Query<UserRole>();
        _session.Save(userRole);
        _session.Flush();
    }
}
