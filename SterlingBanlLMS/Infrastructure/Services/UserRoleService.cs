using Microsoft.AspNet.Identity;
using SterlingBankLMS.Web.Infrastructure.Auth;
using SterlingBankLMS.Web.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.Infrastructure.Services
{
    public class UserRoleService : IUserRoleService
    {
        ApplicationRoleManager _roleManager;
        public UserRoleService(ApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        protected virtual IQueryable<ApplicationRole> Roles()
        {
            return _roleManager.Roles;
        }

        protected virtual IEnumerable<ApplicationRole> GetRoles(Expression<Func<ApplicationRole, bool>> filter)
        {
            return _roleManager.Roles.Where(filter).ToList();
        }

        protected virtual ApplicationRole FindBySystemName(string name)
        {
            var query = from cr in _roleManager.Roles
                        orderby cr.Id
                        where cr.SystemName == name
                        select cr;
            var role = query.FirstOrDefault();

            return role;
        }

        protected virtual Task<ApplicationRole> FindBySystemNameAsync(string name)
        {
            return _roleManager.Roles.FirstOrDefaultAsync(p => p.SystemName == name);
        }

        protected virtual bool RoleExists(string roleName)
        {
            return _roleManager.RoleExists(roleName);
        }

        protected virtual Task<bool> RoleExistsAsync(string roleName)
        {
            return _roleManager.RoleExistsAsync(roleName);
        }

        protected virtual Task<ApplicationRole> FindByIdAsync(int id)
        {
            return _roleManager.FindByIdAsync(id);
        }

        protected virtual ApplicationRole FindById(int id)
        {
            return _roleManager.FindById(id);
        }

        protected virtual Task<ApplicationRole> FindByNameAsync(string name)
        {
            return _roleManager.FindByNameAsync(name);
        }

        protected virtual ApplicationRole FindByName(string name)
        {
            return _roleManager.FindByName(name);
        }

        protected virtual Task<IdentityResult> CreateAsync(ApplicationRole role)
        {
            return _roleManager.CreateAsync(role);
        }

        protected virtual Task<IdentityResult> UpdateAsync(ApplicationRole role)
        {
            return _roleManager.UpdateAsync(role);
        }
        protected virtual IdentityResult Update(ApplicationRole role)
        {
            return _roleManager.Update(role);
        }
        protected virtual IdentityResult Delete(ApplicationRole role)
        {
            return _roleManager.Delete(role);
        }

        protected virtual Task<IdentityResult> DeleteAsync(ApplicationRole role)
        {
            return _roleManager.DeleteAsync(role);
        }

        protected virtual IdentityResult Create(ApplicationRole role)
        {
            return _roleManager.Create(role);
        }

        bool IUserRoleService.RoleExists(string roleName)
        {
            return RoleExists(roleName);
        }

        IdentityResult IUserRoleService.Create(ApplicationRole role)
        {
            return Create(role);
        }

        Task<IdentityResult> IUserRoleService.CreateAsync(ApplicationRole role)
        {
            return CreateAsync(role);
        }

        Task<ApplicationRole> IUserRoleService.FindByIdAsync(int id)
        {
            return FindByIdAsync(id);
        }

        ApplicationRole IUserRoleService.FindById(int id)
        {
            return FindById(id);
        }

        Task<ApplicationRole> IUserRoleService.FindByNameAsync(string name)
        {
            return FindByNameAsync(name);
        }

        Task<bool> IUserRoleService.RoleExistsAsync(string roleName)
        {
            return RoleExistsAsync(roleName);
        }

        ApplicationRole IUserRoleService.FindByName(string name)
        {

            return FindByName(name);
        }

        Task<IdentityResult> IUserRoleService.DeleteAsync(ApplicationRole role)
        {
            return DeleteAsync(role);
        }

        IdentityResult IUserRoleService.Delete(ApplicationRole role)
        {
            return Delete(role);
        }

        IdentityResult IUserRoleService.Update(ApplicationRole role)
        {
            return Update(role);
        }

        Task<IdentityResult> IUserRoleService.UpdateAsync(ApplicationRole role)
        {
            return UpdateAsync(role);
        }

        Task<ApplicationRole> IUserRoleService.FindBySystemNameAsync(string name)
        {
            return FindBySystemNameAsync(name);
        }

        ApplicationRole IUserRoleService.FindBySystemName(string name)
        {
            return FindBySystemName(name);
        }

        IQueryable<ApplicationRole> IUserRoleService.GetRoles()
        {
            return Roles();
        }

        IEnumerable<ApplicationRole> IUserRoleService.GetRoles(Expression<Func<ApplicationRole, bool>> filter)
        {
            return GetRoles(filter);
        }

    }
}