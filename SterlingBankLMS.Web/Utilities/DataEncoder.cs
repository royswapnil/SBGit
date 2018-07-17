using Newtonsoft.Json;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace SterlingBankLMS.Web.Utilities
{
    public static class DataEncoder
    {
        const string MachineKeyPurpose = "sterlingbankLMSApp:Username:{0}";
        const string Anonymous = "<anonymous>";

        static string GetMachineKeyPurpose(IPrincipal user, bool purpose)
        {
            return string.Format(MachineKeyPurpose,
                purpose ? Thread.CurrentPrincipal.Identity.Name : Anonymous);
        }

        public static string Protect<T>(T objectData, bool useIdentity = false)
        {
            var json = JsonConvert.SerializeObject(objectData);
            var bytes = Encoding.Default.GetBytes(json);
            if (bytes == null || bytes.Length == 0) return null;
            var purpose = GetMachineKeyPurpose(Thread.CurrentPrincipal, useIdentity);
            var value = MachineKey.Protect(bytes, purpose);
            return HttpServerUtility.UrlTokenEncode(value);
        }

        public static T Unprotect<T>(string value, bool useIdentity = false)
        {
            if (string.IsNullOrWhiteSpace(value))
                return default(T);

            var bytes = HttpServerUtility.UrlTokenDecode(value);
            var purpose = GetMachineKeyPurpose(Thread.CurrentPrincipal, useIdentity);
            var dataBytes = MachineKey.Unprotect(bytes, purpose);
            var json = Encoding.Default.GetString(dataBytes);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}