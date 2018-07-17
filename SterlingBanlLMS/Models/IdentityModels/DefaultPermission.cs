using System.Collections.Generic;

namespace SterlingBankLMS.Web.Models.IdentityModels
{
    public class DefaultPermission
    {
        public string RoleSystemName { get; set; }

        public IEnumerable<Permission> Permissions { get; set; }
    }
}