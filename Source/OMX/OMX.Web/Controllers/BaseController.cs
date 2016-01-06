using OMX.Data;
using OMX.Infrastructure.Caching;
using OMX.Infrastructure.Populators;

namespace OMX.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.AspNet.SignalR;

    using OMX.Data.UoW;
    using OMX.Models;
    using OMX.Web.Hubs;

    public class BaseController : Controller
    {
        public BaseController(IOMXData data)
        {
            this.Populator = new DropDownListPopulator(new OMXData(new OMXDbContext()), new InMemoryCache());
            this.Data = data;
            this.HubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        }

        protected IHubContext HubContext { get; private set; }

        protected IDropDownListPopulator Populator { get; private set; }

        protected IOMXData Data { get; private set; }

        protected User UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                this.UserProfile =
                this.Data.Users.All()
                .FirstOrDefault(u => u.UserName == requestContext.HttpContext.User.Identity.Name);
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}