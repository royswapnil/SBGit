using Microsoft.AspNet.Identity;
using SterlingBankLMS.Web.Infrastructure.Auth;
using SterlingBankLMS.Web.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.Infrastructure.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly ApplicationUserManager _userManager;
        private bool _disposed;

        public UserAccountService(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        protected virtual IList<string> GetRoles(int userId)
        {
            return _userManager.GetRoles(userId);
        }

        protected virtual Task<IList<string>> GetRolesAsync(int userId)
        {
            return _userManager.GetRolesAsync(userId);
        }

        protected virtual ApplicationUser Find(string userName, string password)
        {
            return _userManager.Find(userName, password);
        }

        protected virtual Task<ApplicationUser> FindAsync(string userName, string password)
        {
            return _userManager.FindAsync(userName, password);
        }

        protected virtual ApplicationUser Find(UserLoginInfo info)
        {
            return _userManager.Find(info);
        }

        protected virtual Task<ApplicationUser> FindAsync(UserLoginInfo info)
        {
            return _userManager.FindAsync(info);
        }

        protected virtual IdentityResult CreateUser(ApplicationUser user, string password)
        {
            return _userManager.Create(user, password);
        }

        protected virtual Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        protected virtual IQueryable<ApplicationUser> GetAllUsers()
        {
            return _userManager.Users ?? new List<ApplicationUser>().AsQueryable();
        }

        protected virtual IEnumerable<ApplicationUser> GetAllUsers(Expression<Func<ApplicationUser, bool>> filter)
        {
            return _userManager.Users.Where(filter).ToList();
        }

        protected virtual IdentityResult CreateUser(ApplicationUser user)
        {
            return _userManager.Create(user);
        }

        protected virtual Task<IdentityResult> CreateUserAsync(ApplicationUser user)
        {
            return _userManager.CreateAsync(user);
        }

        protected virtual IdentityResult AddLogin(int userId, UserLoginInfo loginInfo)
        {
            return _userManager.AddLogin(userId, loginInfo);
        }

        protected virtual Task<IdentityResult> AddLoginAsync(int userId, UserLoginInfo loginInfo)
        {
            return _userManager.AddLoginAsync(userId, loginInfo);
        }

        protected virtual ApplicationUser FindUserById(int userId)
        {
            return _userManager.FindById(userId);
        }

        protected virtual Task<ApplicationUser> FindUserByIdAsync(int userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        protected virtual IdentityResult Update(ApplicationUser user)
        {
            return _userManager.Update(user);
        }

        protected virtual Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            return _userManager.UpdateAsync(user);
        }

        protected virtual Task<string> GenerateEmailConfirmationTokenAsync(int userId)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(userId);
        }

        protected virtual string GenerateEmailConfirmationToken(int userId)
        {
            return _userManager.GenerateEmailConfirmationToken(userId);
        }

        protected virtual IdentityResult ConfirmEmail(int userId, string token)
        {
            return _userManager.ConfirmEmail(userId, token);
        }

        protected virtual Task<IdentityResult> ConfirmEmailAsync(int userId, string token)
        {
            return _userManager.ConfirmEmailAsync(userId, token);
        }

        protected virtual Task<IdentityResult> SetLockoutEnabledAsync(int userId, bool enabled)
        {
            return _userManager.SetLockoutEnabledAsync(userId, enabled);
        }
        protected virtual IdentityResult SetLockoutEnabled(int userId, bool enabled)
        {
            return _userManager.SetLockoutEnabled(userId, enabled);
        }

        protected virtual IdentityResult AddToRole(int userId, string role)
        {
            return _userManager.AddToRole(userId, role);
        }

        protected virtual Task<IdentityResult> AddToRoleAsync(int userId, string role)
        {
            return _userManager.AddToRoleAsync(userId, role);
        }

        protected virtual Task<IdentityResult> RemoveFromRoleAsync(int userId, string role)
        {
            return _userManager.RemoveFromRoleAsync(userId, role);
        }

        protected virtual IdentityResult RemoveFromRole(int userId, string role)
        {
            return _userManager.RemoveFromRole(userId, role);
        }

        protected virtual IdentityResult UpdateSecurityStamp(int userId)
        {
            return _userManager.UpdateSecurityStamp(userId);
        }

        protected virtual async Task<IdentityResult> UpdateSecurityStampAsync(int userId)
        {
            return await _userManager.UpdateSecurityStampAsync(userId);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing) {
                if (_userManager != null)
                    _userManager.Dispose();
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async Task<ApplicationUser> ValidateAccountEmailAsync(int userId, string token)
        {
            var user = FindUserById(userId);
            if (user == null)
                return null;

            var result = await ConfirmEmailAsync(user.Id, token);
            if (result.Succeeded)
                return user;
            return null;
        }
        protected virtual ApplicationUser FindUserByName(string userName)
        {
            return _userManager.FindByName(userName);
        }

        protected virtual async Task<ApplicationUser> FindUserByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        protected virtual ApplicationUser FindEmail(string email)
        {
            return _userManager.FindByEmail(email);
        }

        protected virtual async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        protected virtual Task<string> GeneratePasswordResetTokenAsync(int userId)
        {
            return _userManager.GeneratePasswordResetTokenAsync(userId);
        }

        protected virtual string GeneratePasswordResetToken(int userId)
        {
            return _userManager.GeneratePasswordResetToken(userId);
        }

        protected virtual IdentityResult ResetPassword(int userId, string token, string newPassword)
        {
            return _userManager.ResetPassword(userId, token, newPassword);
        }

        protected virtual Task<IdentityResult> ResetPasswordAsync(int userId, string token, string newPassword)
        {
            return _userManager.ResetPasswordAsync(userId, token, newPassword);
        }

        protected virtual Task<IdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(userId, currentPassword, newPassword);
        }

        protected virtual Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            return _userManager.DeleteAsync(user);
        }

        protected virtual IdentityResult Delete(ApplicationUser user)
        {
            return _userManager.Delete(user);
        }

        protected virtual bool ValidPassword(ApplicationUser user, string password)
        {
            return _userManager.CheckPassword(user, password);
        }

        ApplicationUser IUserAccountService.FindUserByName(string userName)
        {
            return FindUserByName(userName);
        }

        Task<ApplicationUser> IUserAccountService.ValidateAccountEmailAsync(int userId, string token)
        {
            return ValidateAccountEmailAsync(userId, token);
        }

        IdentityResult IUserAccountService.Update(ApplicationUser user)
        {
            return Update(user);
        }

        Task<IdentityResult> IUserAccountService.UpdateAsync(ApplicationUser user)
        {
            return UpdateAsync(user);
        }

        Task<string> IUserAccountService.GenerateEmailConfirmationTokenAsync(int userId)
        {
            return GenerateEmailConfirmationTokenAsync(userId);
        }

        string IUserAccountService.GenerateEmailConfirmationToken(int userId)
        {
            return GenerateEmailConfirmationToken(userId);
        }

        IdentityResult IUserAccountService.ConfirmEmail(int userId, string token)
        {
            return ConfirmEmail(userId, token);
        }

        Task<IdentityResult> IUserAccountService.ConfirmEmailAsync(int userId, string token)
        {
            return ConfirmEmailAsync(userId, token);
        }

        Task<IdentityResult> IUserAccountService.SetLockoutEnabledAsync(int userId, bool enabled)
        {
            return SetLockoutEnabledAsync(userId, enabled);
        }
        Task<IdentityResult> IUserAccountService.AddLoginAsync(int userId, UserLoginInfo loginInfo)
        {
            return AddLoginAsync(userId, loginInfo);
        }

        IdentityResult IUserAccountService.AddLogin(int userId, UserLoginInfo loginInfo)
        {
            return AddLogin(userId, loginInfo);
        }
        IdentityResult IUserAccountService.CreateUser(ApplicationUser user, string password)
        {
            return CreateUser(user, password);
        }

        Task<IdentityResult> IUserAccountService.CreateUserAsync(ApplicationUser user, string password)
        {
            return CreateUserAsync(user, password);
        }

        IdentityResult IUserAccountService.CreateUser(ApplicationUser user)
        {
            return CreateUser(user);
        }
        IQueryable<ApplicationUser> IUserAccountService.GetAllUsers()
        {
            return GetAllUsers();
        }

        IEnumerable<ApplicationUser> IUserAccountService.GetAllUsers(Expression<Func<ApplicationUser, bool>> filter)
        {
            return GetAllUsers(filter);
        }

        Task<IdentityResult> IUserAccountService.CreateUserAsync(ApplicationUser user)
        {
            return CreateUserAsync(user);
        }

        ApplicationUser IUserAccountService.FindUserById(int id)
        {
            return FindUserById(id);
        }

        Task<ApplicationUser> IUserAccountService.FindUserByIdAsync(int userId)
        {
            return FindUserByIdAsync(userId);
        }

        IdentityResult IUserAccountService.SetLockoutEnabled(int userId, bool enabled)
        {
            return SetLockoutEnabled(userId, enabled);
        }

        IdentityResult IUserAccountService.AddToRole(int userId, string role)
        {
            return AddToRole(userId, role);
        }

        Task<IdentityResult> IUserAccountService.AddToRoleAsync(int userId, string role)
        {
            return AddToRoleAsync(userId, role);
        }

        Task<IdentityResult> IUserAccountService.RemoveFromRoleAsync(int userId, string role)
        {
            return RemoveFromRoleAsync(userId, role);
        }

        IdentityResult IUserAccountService.RemoveFromRole(int userId, string role)
        {
            return RemoveFromRole(userId, role);
        }

        Task<ApplicationUser> IUserAccountService.FindAsync(UserLoginInfo info)
        {
            return FindAsync(info);
        }

        ApplicationUser IUserAccountService.Find(UserLoginInfo info)
        {
            return Find(info);
        }

        ApplicationUser IUserAccountService.ValidateUser(string userName, string password)
        {
            return Find(userName, password);
        }

        Task<ApplicationUser> IUserAccountService.ValidateUserAsync(string userName, string password)
        {
            return FindAsync(userName, password);
        }

        IEnumerable<string> IUserAccountService.GetRoles(int userId)
        {
            return GetRoles(userId);
        }

        Task<string> IUserAccountService.GeneratePasswordResetTokenAsync(int userId)
        {
            return GeneratePasswordResetTokenAsync(userId);
        }

        string IUserAccountService.GeneratePasswordResetToken(int userId)
        {
            return GeneratePasswordResetToken(userId);
        }

        IdentityResult IUserAccountService.ResetPassword(int userId, string token, string newPassword)
        {
            return ResetPassword(userId, token, newPassword);
        }

        Task<IdentityResult> IUserAccountService.ResetPasswordAsync(int userId, string token, string password)
        {
            return ResetPasswordAsync(userId, token, password);
        }

        Task<IdentityResult> IUserAccountService.ChangePasswordAsync(int userId, string currentPassword, string newpassword)
        {
            return ChangePasswordAsync(userId, currentPassword, newpassword);
        }

        Task<IdentityResult> IUserAccountService.DeleteAsync(ApplicationUser user)
        {
            return DeleteAsync(user);
        }

        IdentityResult IUserAccountService.Delete(ApplicationUser user)
        {
            return Delete(user);
        }

        Task<ApplicationUser> IUserAccountService.FindUserByNameAsync(string userName)
        {
            return FindUserByNameAsync(userName);
        }

        protected virtual ApplicationUser FindUserByEmail(string email)
        {
            return _userManager.FindByEmail(email);
        }

        Task<ApplicationUser> IUserAccountService.FindUserByEmailAsync(string email)
        {
            return FindUserByEmailAsync(email);
        }

        ApplicationUser IUserAccountService.FindUserByEmail(string email)
        {
            return FindUserByEmail(email);
        }

        IdentityResult IUserAccountService.UpdateSecurityStamp(int userId)
        {
            return UpdateSecurityStamp(userId);
        }

        Task<IdentityResult> IUserAccountService.UpdateSecurityStampAsync(int userId)
        {
            return UpdateSecurityStampAsync(userId);
        }

        Task<ClaimsIdentity> IUserAccountService.CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
            return _userManager.CreateIdentityAsync(user, authenticationType);
        }

        bool IUserAccountService.ValidPassword(ApplicationUser user, string password)
        {
            return ValidPassword(user, password);
        }
    }
}