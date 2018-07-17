using Microsoft.AspNet.Identity;
using SterlingBankLMS.Web.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.Infrastructure.Services
{
    public interface IUserRoleService
    {
        bool RoleExists(string roleName);
        IdentityResult Create(ApplicationRole role);
        Task<IdentityResult> CreateAsync(ApplicationRole role);
        Task<ApplicationRole> FindByIdAsync(int id);
        ApplicationRole FindById(int id);
        Task<ApplicationRole> FindByNameAsync(string name);
        Task<ApplicationRole> FindBySystemNameAsync(string name);
        Task<bool> RoleExistsAsync(string roleName);
        ApplicationRole FindByName(string name);
        ApplicationRole FindBySystemName(string name);
        Task<IdentityResult> DeleteAsync(ApplicationRole role);
        IdentityResult Delete(ApplicationRole role);
        IdentityResult Update(ApplicationRole role);
        Task<IdentityResult> UpdateAsync(ApplicationRole role);
        IQueryable<ApplicationRole> GetRoles();
        IEnumerable<ApplicationRole> GetRoles(Expression<Func<ApplicationRole, bool>> filter);
    }
}