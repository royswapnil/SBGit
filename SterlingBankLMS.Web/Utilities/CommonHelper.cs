using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace SterlingBankLMS.Web.Utilities
{
    public static class CommonHelper
    {
        public static T GetAppSetting<T>(string key, T defValue = default(T))
        {
            var setting = ConfigurationManager.AppSettings[key];

            if (setting == null)
                return defValue;

            try { return (T) Convert.ChangeType(setting, typeof(T), CultureInfo.InvariantCulture); }
            catch { }

            return defValue;
        }

        public static DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }

        public static string GetName(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        public static string GetDescription(this Enum value)
        {

            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (null != fi)
            {
                object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return value.ToString();

        }

        public static string ToAbsolutePath(string relativePath)
        {
            var virtualPath = ToVirtualPath(relativePath);
            return VirtualPathUtility.ToAbsolute(virtualPath);
        }

        public static string ToVirtualPath(string relativePath)
        {
            string relName = relativePath.StartsWith("~") ? relativePath : relativePath.StartsWith("/") ? string.Concat("~", relativePath) : relativePath;
            return relName;
        }

        public static string MapPath(string path, bool findAppRoot = true)
        {
            path = ToVirtualPath(path);

            if (HostingEnvironment.IsHosted) {
                // hosted
                return HostingEnvironment.MapPath(path);
            }
            else {
                // not hosted. For example, running in unit tests or EF tooling
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');

                var testPath = Path.Combine(baseDirectory, path);

                if (findAppRoot /* && !Directory.Exists(testPath)*/) {
                    // most likely we're in unit tests or design-mode (EF migration scaffolding)
                    // find solution root directory first
                    var dir = FindSolutionRoot(baseDirectory);

                    // concat the web root
                    if (dir != null) {
                        baseDirectory = Path.Combine(dir.FullName, "SterlingBankLMS.Web");
                        testPath = Path.Combine(baseDirectory, path);
                    }
                }

                return testPath;
            }
        }


        private static DirectoryInfo FindSolutionRoot(string currentDirectory)
        {
            var dir = Directory.GetParent(currentDirectory);
            while (true) {
                if (dir == null || IsSolutionRoot(dir))
                    break;

                dir = dir.Parent;
            }

            return dir;
        }

        private static bool IsSolutionRoot(DirectoryInfo directoryInfo)
        {
            return File.Exists(Path.Combine(directoryInfo.FullName, "SterlingBankLMS.sln"));
        }

        internal static void GetAppSetting<T>(object useMemoryCache)
        {
            throw new NotImplementedException();
        }
    }
}