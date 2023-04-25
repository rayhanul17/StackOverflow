using StackOverflow.DAL.Entities.Membership;
using StackOverflow.DAL.UnitOfWorks;

namespace StackOverflow.Services.Services.Membership;

public interface IUserRoleManager
{
    IList<string> GetRoles(ApplicationUser appUser);
    void AddToRoles(ApplicationUser appUser, string[] roles);
}

public class UserRoleManager : IUserRoleManager
{
    private readonly IApplicationUnitOfWork _unitOfWork;

    public UserRoleManager(IApplicationUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IList<string> GetRoles(ApplicationUser appUser)
    {
        var roles = _unitOfWork.UserRoleRepository.Find(x => x.ApplicationUser.Id == appUser.Id)
            .Select(x => x.Role.Name).ToList();

        return roles;
    }

    public void AddToRoles(ApplicationUser appUser, string[] roles)
    {
        roles.ToList().ForEach(x =>
        {
            var role = _unitOfWork.RoleRepository.Find(t => t.Name == x)
            .Select(x => new AppRole
            {
                Id = x.Id,
                Name = x.Name,
                NormalizedName = x.Name.ToUpper(),
            }).FirstOrDefault();

            if (role == null)
                return;

            var userRole = new UserRole
            {
                ApplicationUser = appUser,
                Role = role
            };
            _unitOfWork.UserRoleRepository.Merge(userRole);
        });
    }
}
