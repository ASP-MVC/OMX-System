namespace OMX.Web.Hubs
{
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Routing;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    [HubName("notification")]
    public class NotificationHub : Hub
    {
        public override Task OnConnected()
        {
            this.Clients.User(HttpContext.Current.User.Identity.Name).getNotificationsCount();
            return base.OnConnected();
        }
    }
}