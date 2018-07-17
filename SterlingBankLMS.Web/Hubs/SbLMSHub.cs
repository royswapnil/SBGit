using Microsoft.AspNet.SignalR;

namespace SterlingBankLMS.Web.Hubs
{
    public class SbLMSHub : Hub
    {
        public static void Send()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SbLMSHub>();
            context.Clients.All.displayStatus();
        }
    }
}