using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace SterlingBankLMS.Core.Helper
{
    public static class AppHelper
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

        public static string TimeFuture(DateTime dt, DateTime dt2)
        {
            TimeSpan span = dt - dt2;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("{0} {1}", years, years == 1 ? "year ago" : "years ago");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("{0} {1} ", months, months == 1 ? "month ago" : "months ago");
            }
            if (span.Days > 0)
                return String.Format("{0} {1} ", span.Days, span.Days == 1 ? "day ago" : "days ago");
            if (span.Hours > 0)
                return String.Format("{0} {1} ", span.Hours, span.Hours == 1 ? "hour ago" : "hours ago");
            if (span.Minutes > 0)
                return String.Format("{0} {1} ", span.Minutes, span.Minutes == 1 ? "minute ago" : "minutes ago");
            if (span.Seconds > 5)
                return String.Format("{0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }

        private static int RandomNumber( int min, int max )
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private static string RandomString( int size, bool lowerCase )
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;

            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);

            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static string GeneratePwd()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(2, true));
            builder.Append(RandomNumber(10, 99));
            builder.Append(RandomString(4, false));
            return builder.ToString();
        }

    }
}
