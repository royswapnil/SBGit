using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.ViewModels
{
    public class RolePermissionModel
    {
        public int Id { get; set; }
        public bool IsPermissionName { get; set; }
    }

    public class UserRoleModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

    }
}
