using System.Collections.Generic;

namespace SterlingBankLMS.Web.Utilities
{
    /// <summary>
    /// Contains constants values and key lookups used throughout the application
    /// </summary>
    public class AppConstants
    {
        public static readonly string AuthenticationType = "sterling-lms";
        public static readonly string AppErrorLogger = "filelogger";
        public static readonly string FileUploadPath = "/Content/img/";
        public static readonly string DocUploadPath = "/Content/docs/";
        public static readonly string FileChunkPath = "/Content/Chunks/";
        public static readonly string CertUploadPath = "/Content/img/";
        public static readonly string VideoUploadPath = "/Content/video/";
        public static readonly string excelUploadPath = "/Content/excel/";
        public static readonly string DefautltUrl = "~/common/home/index";

        public static readonly List<string> AllowedFileExtensions = new List<string>() { "docx", "doc", "pdf", "pptx", "ppt", "xls",
            "xlsx", "csv", "txt", "htm", "html", "xml", "mp4", "avi", "mov", "webm", "mkv", "flv", "amv", "ogg", "mpg", "mpeg" , "m4v",
            "3gp", "wmv", "jpg", "jpeg", "gif", "png"
        };

        public static readonly string SterlingEmailDomainName = "sterlingbankng.com";

        public static readonly string PasswordReset = "/app_data/emailtemplates/resetpassword.html";


        public static readonly string Owin = "owin.Environment";
        public static readonly string AuthProviderEndpoint = "/authentication/login";

        public class Keys
        {

            //AD parameter
            public static readonly string UseActiveDirectoryKey = "app.UseActiveDirectory";
            public static readonly string ActiveDirectoryUrlKey = "app.ActiveDirectoryUrl";
            public static readonly string ActionKey = "app.ActiveDirectoryAction";

            //smtp
            public static readonly string SmtpUsernameKey = "smtp.username";
            public static readonly string SmtpPasswordKey = "smtp.password";
            public static readonly string SmtpServerKey = "smtp.server";
            public static readonly string SmtpPortKey = "smtp.port";
            public static readonly string SmtpUseSslKey = "smtp.usessl";
            public static readonly string SmtpUseDefautlCredentialsKey = "smtp.defaultCredentials";

            //app email
            public static readonly string AdminEmailKey = "app.AdminEmail";
            public static readonly string AppEmailKey = "app.AppEmail";
            public static readonly string ServiceDeskEmailKey = "app.ServicedeskEmail";

            //redis cache
            public static readonly string UseRedisCacheService = "app.UseRedisCache";
            public static readonly string RedisCacheConnectionString = "app.RedisConnectionstring";
        }

        public class Role
        {
            public static readonly string Admin = "Administrator";
            public static readonly string HR = "Human Resource";
            public static readonly string Employee = "Employee";
            public static readonly string IT = "Information Technology";
            public static readonly string Mgt = "Management";
            public static readonly string Auditor = "Auditor";
        }

        public class Paging
        {

            public static readonly int MaxPageSize = 100;
            public static readonly int DefaultPageSize = 10;
        }

        public class ClaimsKey
        {
            public static readonly string Function = "Function";
            public static readonly string Branch = "Branch";
            public static readonly string StaffId = "StaffId";
            public static readonly string UserId = "UserId";
            public static readonly string OrganizationId = "OrganizationId";
            public static readonly string LineManagerId = "LineManagerId";
            public static readonly string Permissions = "Permissions";
        }

        public class CacheKey
        {
            public static readonly string DEPARTMENTDDL = "departments.ddl.cache";
            public static readonly string PERMISSIONS_ALLOWED_KEY = "permissions.allowed-{0}-{1}";
            public static readonly string PERMISSIONS_USER_PATTERN_KEY = "permissions.allowed-{0}";
            public static readonly string PERMISSIONS_PATTERN_KEY = "permissions.";
        }

        public static readonly AdvertDimensions TopAdvert = new AdvertDimensions { MaxWidth = 1200, MaxHeight = 200 };
        public static readonly AdvertDimensions SideAdvert = new AdvertDimensions { MaxWidth = 300, MaxHeight = 400 };


        public class AdvertDimensions
        {
            public int MaxWidth { get; set; }

            public int MaxHeight { get; set; }
        }

    }
}