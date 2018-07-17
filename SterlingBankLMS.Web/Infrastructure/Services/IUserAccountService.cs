using Microsoft.AspNet.Identity;
using SterlingBankLMS.Web.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.Infrastructure.Services
{
    public interface IUserAccountService : IDisposable
    {
        IdentityResult CreateUser(ApplicationUser user, string password);
        IdentityResult CreateUser(ApplicationUser user);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user);
        IQueryable<ApplicationUser> GetAllUsers();
        IEnumerable<ApplicationUser> GetAllUsers(Expression<Func<ApplicationUser, bool>> filter);
        Task<IdentityResult> AddLoginAsync(int userId, UserLoginInfo login);
        IdentityResult AddLogin(int userId, UserLoginInfo login);
        ApplicationUser FindUserById(int userId);
        Task<ApplicationUser> FindUserByIdAsync(int userId);
        Task<ApplicationUser> FindAsync(UserLoginInfo info);
        ApplicationUser Find(UserLoginInfo info);
        ApplicationUser FindUserByName(string userName);
        Task<ApplicationUser> FindUserByNameAsync(string userName);
        ApplicationUser FindUserByEmail(string email);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
        IdentityResult Update(ApplicationUser user);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        ApplicationUser ValidateUser(string userName, string password);
        Task<ApplicationUser> ValidateUserAsync(string userName, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(int userId);
        Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType);
        string GenerateEmailConfirmationToken(int userId);
        IdentityResult ConfirmEmail(int userId, string token);
        bool ValidPassword(ApplicationUser user, string password);
        Task<IdentityResult> ConfirmEmailAsync(int userId, string token);
        Task<IdentityResult> SetLockoutEnabledAsync(int userId, bool enabled);
        IdentityResult SetLockoutEnabled(int userId, bool enabled);
        IEnumerable<string> GetRoles(int userId);
        Task<ApplicationUser> ValidateAccountEmailAsync(int userId, string token);
        IdentityResult AddToRole(int userId, string role);
        Task<IdentityResult> AddToRoleAsync(int userId, string role);
        Task<IdentityResult> RemoveFromRoleAsync(int userId, string role);
        IdentityResult RemoveFromRole(int userId, string role);
        Task<string> GeneratePasswordResetTokenAsync(int userId);
        string GeneratePasswordResetToken(int userId);
        IdentityResult ResetPassword(int userId, string token, string newPassword);
        Task<IdentityResult> ResetPasswordAsync(int userId, string token, string newPassword);
        Task<IdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
        IdentityResult Delete(ApplicationUser user);
        IdentityResult UpdateSecurityStamp(int userId);
        Task<IdentityResult> UpdateSecurityStampAsync(int userId);
    }
}