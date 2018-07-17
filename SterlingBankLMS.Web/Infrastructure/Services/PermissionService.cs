using Nop.Core.Caching;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Infrastructure.Auth;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Web.Infrastructure.Services
{
    public interface IPermissionService
    {
        bool TryCheckAccess(Permission permission);
        bool TryCheckAccess(string permissionName);
        bool TryCheckAccess(string permissionSystemName, UserClaims user);
        void ClearCachedPermission(int userId);
        void ClearPermissionCache();
        Permission Find(int id);
    }

    public class PermissionService : GenericService<Permission>, IPermissionService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IUserRoleService _roleSvc;
        private readonly IWorkContext _workContext;

        public PermissionService(IUnitOfWork unitOfWork, ICacheManager cacheManager,
            IUserRoleService roleSvc, IUserAccountService _userAccountSvc, IWorkContext workContext) : base(unitOfWork)
        {
            _cacheManager = cacheManager;
            _roleSvc = roleSvc;
            _workContext = workContext;
        }

        protected virtual bool TryCheckAccess(Permission permission, UserClaims user)
        {
            if (permission == null)
                return false;

            if (user == null)
                return false;

            return TryCheckAccess(permission.SystemName, user);
        }

        public virtual bool TryCheckAccess(string permissionSystemName, UserClaims user)
        {
            if (string.IsNullOrWhiteSpace(permissionSystemName))
                return false;

            if (TryCheckAccess(permissionSystemName, user.Permissions, user.Id))
                return true;

            return false;
        }

        protected virtual bool TryCheckAccess(string permissionRecordSystemName, IEnumerable<string> userPermissionList, int userId)
        {
            if (string.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            if (!userPermissionList.Any())
                return false;

            string key = string.Format(AppConstants.CacheKey.PERMISSIONS_ALLOWED_KEY, userId, permissionRecordSystemName);

          //Todo:(Samuel) How do we use cache here? return _cacheManager.Get(key, () => {
                foreach (var permission in userPermissionList)
                    if (permission.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase))
                        return true;

                return false;
           // });
        }

        public bool TryCheckAccess(Permission permission)
        {
            return TryCheckAccess(permission, _workContext.User);
        }

        public bool TryCheckAccess(string permissionName)
        {
            return TryCheckAccess(permissionName, _workContext.User);
        }

        public void ClearPermissionCache()
        {
            _cacheManager.RemoveByPattern(AppConstants.CacheKey.PERMISSIONS_PATTERN_KEY);
        }

        public void ClearCachedPermission(int userId)
        {
            var key = string.Format(AppConstants.CacheKey.PERMISSIONS_USER_PATTERN_KEY, userId);
            _cacheManager.RemoveByPattern(key);
        }
    }
}