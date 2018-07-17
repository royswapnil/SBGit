using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Web.Infrastructure.Auth;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SterlingBankLMS.Web.Migrations.Seed
{
    public class PermissionSeed
    {
        public static void InsertDefaultRolesAndPermissions(DbContext dbCtxt, int orgId)
        {
            var systemPermissions = PermissionProvider.GetSystemDefaultRoles();

            if (systemPermissions == null || !systemPermissions.Any())
                return;

            var roleStore = new ApplicationRoleStore(dbCtxt);
            var roleManager = new ApplicationRoleManager(roleStore);

            foreach (var systemPermission in systemPermissions) {

                var role = roleManager.FindByNameAsync(systemPermission.Key).Result;

                if (role == null) {
                    role = new ApplicationRole
                    {
                        Name = systemPermission.Key,
                        SystemName = systemPermission.Key,
                        IsActive = true,
                        OrganizationId = orgId
                    };

                    var r = roleManager.CreateAsync(role).Result;
                }

                foreach (var permision1 in systemPermission.Value) {
                    var permission = dbCtxt.Set<Permission>().FirstOrDefault(x => x.SystemName.Equals(permision1.SystemName));

                    if (permission == null) {
                        permission = new Permission
                        {
                            Name = permision1.Name,
                            SystemName = permision1.SystemName,
                            CreatedDate = DateTime.UtcNow,
                        };
                    }

                    var alreadyExist = permission.Roles.Any(x => x.SystemName.Equals(role.SystemName));

                    if (!alreadyExist) {
                        permission.Roles.Add(role);
                    }

                    dbCtxt.Set<Permission>().AddOrUpdate(x => x.SystemName, permission);
                }
                dbCtxt.SaveChanges();
            }

        }
    }
}