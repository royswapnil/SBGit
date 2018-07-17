using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode]
    internal class IdentityResources
    {
        private static CultureInfo resourceCulture;
        private static ResourceManager resourceMan;

        internal IdentityResources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static string DbValidationFailed
        {
            get
            {
                return ResourceManager.GetString("DbValidationFailed", resourceCulture);
            }
        }

        internal static string DuplicateEmail
        {
            get
            {
                return ResourceManager.GetString("DuplicateEmail", resourceCulture);
            }
        }

        internal static string DuplicateUserName
        {
            get
            {
                return ResourceManager.GetString("DuplicateUserName", resourceCulture);
            }
        }

        internal static string EntityFailedValidation
        {
            get
            {
                return ResourceManager.GetString("EntityFailedValidation", resourceCulture);
            }
        }

        internal static string ExternalLoginExists
        {
            get
            {
                return ResourceManager.GetString("ExternalLoginExists", resourceCulture);
            }
        }

        internal static string IdentityV1SchemaError
        {
            get
            {
                return ResourceManager.GetString("IdentityV1SchemaError", resourceCulture);
            }
        }

        internal static string IncorrectType
        {
            get
            {
                return ResourceManager.GetString("IncorrectType", resourceCulture);
            }
        }

        internal static string PropertyCannotBeEmpty
        {
            get
            {
                return ResourceManager.GetString("PropertyCannotBeEmpty", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null)) {
                    ResourceManager manager = new ResourceManager("Microsoft.AspNet.Identity.EntityFramework.IdentityResources", typeof(IdentityResources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static string RoleAlreadyExists
        {
            get
            {
                return ResourceManager.GetString("RoleAlreadyExists", resourceCulture);
            }
        }

        internal static string RoleIsNotEmpty
        {
            get
            {
                return ResourceManager.GetString("RoleIsNotEmpty", resourceCulture);
            }
        }

        internal static string RoleNotFound
        {
            get
            {
                return ResourceManager.GetString("RoleNotFound", resourceCulture);
            }
        }

        internal static string UserAlreadyInRole
        {
            get
            {
                return ResourceManager.GetString("UserAlreadyInRole", resourceCulture);
            }
        }

        internal static string UserIdNotFound
        {
            get
            {
                return ResourceManager.GetString("UserIdNotFound", resourceCulture);
            }
        }

        internal static string UserLoginAlreadyExists
        {
            get
            {
                return ResourceManager.GetString("UserLoginAlreadyExists", resourceCulture);
            }
        }

        internal static string UserNameNotFound
        {
            get
            {
                return ResourceManager.GetString("UserNameNotFound", resourceCulture);
            }
        }

        internal static string UserNotInRole
        {
            get
            {
                return ResourceManager.GetString("UserNotInRole", resourceCulture);
            }
        }

        internal static string ValueCannotBeNullOrEmpty
        {
            get
            {
                return ResourceManager.GetString("ValueCannotBeNullOrEmpty", resourceCulture);
            }
        }
    }
}
