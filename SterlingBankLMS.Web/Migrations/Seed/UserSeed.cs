using Microsoft.AspNet.Identity;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.Infrastructure.Auth;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.Migrations.Seed
{
    public class UserSeed
    {

        private static async Task<ApplicationRole> CreateAdminRole(DbContext dbCtxt)
        {
            var roleStore = new ApplicationRoleStore(dbCtxt);
            var roleManager = new ApplicationRoleManager(roleStore);

            var role = new ApplicationRole
            {
                Name = AppConstants.Role.Admin,
                SystemName = AppConstants.Role.Admin,
                IsActive = true
            };

            var dbRole = roleManager.FindByNameAsync(role.Name).Result;

            if (dbRole == null) {
                var result = roleManager.CreateAsync(role).Result;
                return result.Succeeded ? role : null;
            }
            return dbRole;
        }

        public static async Task InsertOtherRoleUsers(DbContext dbCtxt, int orgId)
        {
            var userStore = new ApplicationUserStore(dbCtxt);

            var userManager = new ApplicationUserManager(userStore);

            var roleStore = new ApplicationRoleStore(dbCtxt);
            var roleManager = new ApplicationRoleManager(roleStore);

            var hrUser = new ApplicationUser
            {
                UserName = "felicity",

                Email = "hr@sterlinbanklms.com",
                DateOfEmployment = new DateTime(2009, 08, 01),
                UserType = UserType.Normal,
                DateOfBirth = new DateTime(1981, 10, 11),
                FirstName = "Felicity",
                Functions = "Regional Service Manager",
                LastName = "Ramsey",
                Gender = Gender.Female,
                StaffId = "000117",
                PhoneNumber = "08064354902",
                OrganizationId = orgId

            };

            var hr = userManager.FindByNameAsync(hrUser.UserName).Result;

            if (hr == null) {
                var result = userManager.CreateAsync(hrUser, "sterlingbank@hr").Result;

                if (result.Succeeded) {

                    var identity = new User { ApplicationUserId = hrUser.Id };

                    dbCtxt.Set<User>().Add(identity);
                    dbCtxt.SaveChanges();

                    var lockoutResult = userManager.SetLockoutEnabledAsync(hrUser.Id, false).Result;

                    var hrRole = roleManager.FindByNameAsync(AppConstants.Role.HR).Result;

                    if (hrRole != null) {
                        var roleResut = userManager.AddToRoleAsync(hrUser.Id, hrRole.Name).Result;
                    }
                }
            }

            var itUser = new ApplicationUser
            {
                UserName = "mark",
                Email = "it@sterlinbanklms.com",
                DateOfEmployment = new DateTime(2011, 10, 01),
                UserType = UserType.Normal,
                DateOfBirth = new DateTime(1972, 10, 11),
                FirstName = "Mark",
                Functions = "Regional Service Manager",
                LastName = "Ghill",
                Gender = Gender.Male,
                StaffId = "000158",
                PhoneNumber = "08064354902",
                OrganizationId = orgId
            };

            var it = userManager.FindByNameAsync(itUser.UserName).Result;

            if (it == null) {
                var result = userManager.CreateAsync(itUser, "sterlingbank@it").Result;

                if (result.Succeeded) {

                    var identity = new User { ApplicationUserId = itUser.Id };

                    dbCtxt.Set<User>().Add(identity);
                    dbCtxt.SaveChanges();

                    var lockoutResult = userManager.SetLockoutEnabledAsync(itUser.Id, false).Result;

                    var itRole = roleManager.FindByNameAsync(AppConstants.Role.IT).Result;

                    if (itRole != null) {
                        var roleResut = userManager.AddToRoleAsync(itUser.Id, itRole.Name).Result;
                    }
                }
            }

            var mgtUser = new ApplicationUser
            {
                UserName = "irene",
                Email = "richard@sterlinbanklms.com",
                DateOfEmployment = new DateTime(2007, 10, 01),
                UserType = UserType.Normal,
                DateOfBirth = new DateTime(1989, 10, 11),
                FirstName = "Irene",
                Functions = "Regional Service Manager",
                LastName = "Kammy",
                Gender = Gender.Female,
                StaffId = "000111",
                PhoneNumber = "08064354902",
                OrganizationId = orgId
            };

            var mgt = userManager.FindByNameAsync(mgtUser.UserName).Result;

            if (mgt == null) {
                var result = userManager.CreateAsync(mgtUser, "sterlingbank@mgt").Result;

                if (result.Succeeded) {

                    var identity = new User { ApplicationUserId = mgtUser.Id };

                    dbCtxt.Set<User>().Add(identity);
                    dbCtxt.SaveChanges();

                    var lockoutResult = userManager.SetLockoutEnabledAsync(mgtUser.Id, false).Result;

                    var mgtRole = roleManager.FindByNameAsync(AppConstants.Role.Mgt).Result;

                    if (mgtRole != null) {
                        var roleResut = userManager.AddToRoleAsync(mgtUser.Id, mgtRole.Name).Result;
                    }
                }
            }
        }

        public static async Task InsertTestUsers(DbContext dbCtxt, int orgId)
        {
            var userStore = new ApplicationUserStore(dbCtxt);
            var roleStore = new ApplicationRoleStore(dbCtxt);

            var userManager = new ApplicationUserManager(userStore);
            var roleManager = new ApplicationRoleManager(roleStore);


            var users = new List<ApplicationUser> {
                new ApplicationUser
                    {
                        UserName = "testuser1",
                        Email = "testuser1@sterlinbanklms.com",
                        DateOfEmployment = new DateTime(2007, 10, 01),
                        UserType = UserType.Normal,
                        DateOfBirth = new DateTime(1989, 10, 11),
                        FirstName = "Test",
                        LastName = "User",
                        StaffId = "000111",
                        OrganizationId = orgId,
                        EmailConfirmed = true
                    },

                 new ApplicationUser
                    {
                        UserName = "testuser2",
                        Email = "testuser2@sterlinbanklms.com",
                        DateOfEmployment = new DateTime(2007, 10, 01),
                        UserType = UserType.Normal,
                        DateOfBirth = new DateTime(1989, 10, 11),
                        FirstName = "Test",
                        LastName = "User",
                        StaffId = "000113",
                        OrganizationId = orgId,
                        EmailConfirmed = true
                    },

                  new ApplicationUser
                    {
                        UserName = "testuser3",
                        Email = "testuser3@sterlinbanklms.com",
                        DateOfEmployment = new DateTime(2007, 10, 01),
                        UserType = UserType.Normal,
                        DateOfBirth = new DateTime(1989, 10, 11),
                        FirstName = "Test",
                        LastName = "User",
                        StaffId = "000115",
                        OrganizationId = orgId,
                        EmailConfirmed = true
                    },

                   new ApplicationUser
                    {
                        UserName = "testuser4",
                        Email = "testuser4@sterlinbanklms.com",
                        DateOfEmployment = new DateTime(2007, 10, 01),
                        UserType = UserType.Normal,
                        DateOfBirth = new DateTime(1989, 10, 11),
                        FirstName = "Test",
                        LastName = "User",
                        StaffId = "000117",
                        OrganizationId = orgId,
                        EmailConfirmed = true
                    },
                    new ApplicationUser
                    {
                        UserName = "testuser5",
                        Email = "testuser5@sterlinbanklms.com",
                        DateOfEmployment = new DateTime(2007, 10, 01),
                        UserType = UserType.Normal,
                        DateOfBirth = new DateTime(1989, 10, 11),
                        FirstName = "Test",
                        LastName = "User",
                        StaffId = "000118",
                        OrganizationId = orgId,
                        EmailConfirmed = true
                    }
            };

            var admins = new List<ApplicationUser> {
                new ApplicationUser
                    {
                        UserName = "testadmin2",
                        Email = "administrator2@sterlinbanklms.com",
                        DateOfEmployment = new DateTime(2007, 10, 01),
                        UserType = UserType.Normal,
                        DateOfBirth = new DateTime(1989, 10, 11),
                        FirstName = "Test",
                        LastName = "Admin",
                        StaffId = "000119",
                        OrganizationId = orgId,
                        EmailConfirmed = true

                    },

                 new ApplicationUser
                    {
                       UserName = "testadmin3",
                        Email = "administrator3@sterlinbanklms.com",
                        DateOfEmployment = new DateTime(2007, 10, 01),
                        UserType = UserType.Normal,
                        DateOfBirth = new DateTime(1989, 10, 11),
                        FirstName = "Test",
                        LastName = "Admin",
                        StaffId = "000123",
                        OrganizationId = orgId,
                        EmailConfirmed = true
                    }
            };


            users.ForEach((user) => {

                if (userManager.FindByNameAsync(user.UserName).Result == null) {
                    IdentityResult identityResult;

                    identityResult = userManager.CreateAsync(user, "sterlingbank@test").Result;

                    if (identityResult.Succeeded) {

                        var identity = new User { ApplicationUserId = user.Id };

                        dbCtxt.Set<User>().Add(identity);
                        dbCtxt.SaveChanges();

                        identityResult = userManager.SetLockoutEnabledAsync(user.Id, false).Result;

                        var roleResult = roleManager.RoleExistsAsync(AppConstants.Role.Employee);
                        if (roleResult.Result) {

                            var isInRole = userManager.IsInRoleAsync(user.Id, AppConstants.Role.Employee);
                            if (!isInRole.Result)
                                identityResult = userManager.AddToRoleAsync(user.Id, AppConstants.Role.Employee).Result;
                        }
                    }
                }
            });


            admins.ForEach((user) => {
                if (userManager.FindByNameAsync(user.UserName).Result == null) {
                    IdentityResult identityResult;

                    identityResult = userManager.CreateAsync(user, "sterlingbank@admin").Result;

                    if (identityResult.Succeeded) {
                        var identity = new User { ApplicationUserId = user.Id };

                        dbCtxt.Set<User>().Add(identity);
                        dbCtxt.SaveChanges();

                        identityResult = userManager.SetLockoutEnabledAsync(user.Id, false).Result;

                        var roleResult = roleManager.RoleExistsAsync(AppConstants.Role.Admin);
                        if (roleResult.Result) {

                            var isInRole = userManager.IsInRoleAsync(user.Id, AppConstants.Role.Admin);
                            if (!isInRole.Result)
                                identityResult = userManager.AddToRoleAsync(user.Id, AppConstants.Role.Admin).Result;
                        }
                    }
                }
            });
        }

        public static async Task<int> InsertAdminUser(DbContext dbCtxt)
        {

            var userStore = new ApplicationUserStore(dbCtxt);

            var userManager = new ApplicationUserManager(userStore);

            var roleStore = new ApplicationRoleStore(dbCtxt);
            var roleManager = new ApplicationRoleManager(roleStore);


            var adminUser = new ApplicationUser
            {
                UserName = "administrator",
                SystemAccount = true,
                Email = "administrator@sterlinbanklms.com",
                DateOfEmployment = new DateTime(2007, 10, 01),
                UserType = UserType.Normal,
                DateOfBirth = new DateTime(1990, 10, 11),
                FirstName = "Mayowa",
                Functions = "Regional Service Manager",
                LastName = "Jericho",
                Gender = Gender.Male,
                StaffId = "000188",
                PhoneNumber = "08064354902",
            };

            var appUser = userManager.FindByNameAsync(adminUser.UserName).Result;

            var userId = 0;

            if (appUser == null) {
                var result = userManager.CreateAsync(adminUser, "sterlingbank@admin").Result;

                if (result.Succeeded) {

                    var identity = new User { ApplicationUserId = adminUser.Id };

                    dbCtxt.Set<User>().Add(identity);
                    dbCtxt.SaveChanges();

                    userId = identity.Id;

                    var lockoutResult = userManager.SetLockoutEnabledAsync(adminUser.Id, false).Result;

                    var adminRole = CreateAdminRole(dbCtxt).Result;
                    //var adminRole = roleManager.FindByNameAsync(AppConstants.Role.Admin).Result;

                    if (adminRole != null) {
                        var roleResut = userManager.AddToRoleAsync(adminUser.Id, adminRole.Name).Result;
                    }
                }
            }
            else {
                var user = dbCtxt.Set<User>().FirstOrDefault(x => x.ApplicationUserId == appUser.Id);
                return userId = user.Id;
            }
            return userId;
        }

    }
}