using System.Collections.Specialized;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.Infrastructure.Messaging.Email
{
    public interface IMailerService
    {
        Task SendMailAsync(Mail mail);
        Task SendMailAsync(Mail mail, StringDictionary Replacements);
        void SendMail(Mail mail);
        void SendMail(Mail mail, StringDictionary Replacements);
    }
}