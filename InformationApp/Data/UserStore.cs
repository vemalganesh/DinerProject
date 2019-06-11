using InformationApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InformationApp.DataAccessLayer;
namespace InformationApp.Data
{
    public class UserStore : IUserStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IUserPhoneNumberStore<ApplicationUser>,
    IUserTwoFactorStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserRoleStore<ApplicationUser>
    {
        private readonly IConfiguration configuration;
        private readonly UserDataAccessLayer objuser;
        private readonly RoleDataAccessLayer objRole;
        public UserStore(IConfiguration configuration)
       {
            this.configuration = configuration;
            objuser = new UserDataAccessLayer(configuration);
            objRole = new RoleDataAccessLayer(configuration);
        }
        ApplicationRole role = new ApplicationRole();
    public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

                    user.ID = Guid.NewGuid().ToString();
                    user.Email = user.Email.ToLower();
                    var result = await objuser.AddUser(user);

        return result;
    }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password, CancellationToken cancellationToken)
        {
            user.Password = password;
            await objuser.FindUserByPassword(user);
            return true;
        }

    public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

          var result =  await objuser.DeleteUserManager(user.ID);
          return result;
    }

    public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
          
            var user = await objuser.GetUserData(userId);
            return user;
        }


    public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.ID);
    }

    public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Name);
    }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Name);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Name = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
    {
        user.Name = userName;
        return Task.FromResult(0);
    }

    public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
           cancellationToken.ThrowIfCancellationRequested();
           var result = await objuser.UpdateUserUserStore(user);

            return result;
    }

    public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
    {
        user.Email = email;
        return Task.FromResult(0);
    }

    public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Email);
    }

    public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.EmailVerified);
    }

    public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
    {
        user.EmailVerified = confirmed;
        return Task.FromResult(0);
    }

    public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
            var user = await objuser.GetUserDataByemail(normalizedEmail);
            return user;

        }

    public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Email);
    }

    public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
    {
        user.Email = normalizedEmail;
        return Task.FromResult(0);
    }

    public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
    {
        user.PhoneNumber = phoneNumber;
        return Task.FromResult(0);
    }

    public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PhoneNumber);
    }

    public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PhoneNumberConfirmed);
    }

    public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
    {
        user.PhoneNumberConfirmed = confirmed;
        return Task.FromResult(0);
    }

    public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
    {
        user.TwoFactorEnabled = enabled;
        return Task.FromResult(0);
    }

    public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.TwoFactorEnabled);
    }

    public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
    {
        user.Password = passwordHash;
        return Task.FromResult(0);
    }

    public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Password);
    }

    public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Password != null);
    }

        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            role.RoleName = roleName;
            role.UserId = user.ID;
            await objRole.AddRole(role);

        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            role.RoleName = roleName;
            role.UserId = user.ID;
            await objRole.DeleteRole(role);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            role.UserId = user.ID;
            var listOfRoles = await objRole.GetAllUserRole(role);

            return listOfRoles;
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var listOfUsers = await objRole.GetUsersInRole(roleName);

            return listOfUsers;
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            role.RoleName = roleName;
            role.UserId = user.ID;
            var result = await objRole.GetIsInRole(role);
            return result;
        }


        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await objuser.GetUserDataByName(normalizedUserName);
            return user;
        }

        public void Dispose()
    {
        // Nothing to dispose.
    }
       
    }
}
