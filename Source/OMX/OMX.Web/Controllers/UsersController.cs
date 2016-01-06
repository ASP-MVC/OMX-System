namespace OMX.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;

    using OMX.Common;
    using OMX.Data.UoW;
    using OMX.Web.Hubs;
    using OMX.Web.Models.ViewModels;

    using PagedList;

    [RoutePrefix("users")]
    [System.Web.Mvc.Authorize]
    public class UsersController : BaseController
    {
        private const string UserImagesStore = "/Files/UserImages";
        public UsersController(IOMXData data)
            : base(data)
        {
        }

        [Route("{username}")]
        public ActionResult Me(string username)
        {
            var user =
                this.Data.Users.All()
                    .Where(u => u.UserName == username)
                    .Project()
                    .To<UserViewModel>()
                    .FirstOrDefault();
            if (user == null)
            {
                return this.HttpNotFound();
            }

            return this.View("UserProfile", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("upload-profile")]
        public ActionResult UploadProfileImage(HttpPostedFileBase file)
        {
            this.SaveFileToFileSystem(file);

            var fileName = Path.GetFileName(file.FileName);

            var user = this.Data.Users.GetById(this.User.Identity.GetUserId());
            user.PictureUrl = UserImagesStore + "/" + fileName;
            this.Data.Users.Update(user);
            this.Data.SaveChanges();

            return this.RedirectToAction(this.User.Identity.GetUserName(), "Users");
        }

        [HttpGet]
        [Route("get-unread-notifications")]
        public ActionResult GetUnreadNotifications()
        {
            if (this.UserProfile == null)
            {
                return null;
            }
            var unreadNotifications = 
                this.UserProfile.RecievedComments
                    .Count(c => c.IsRead == false);

            return this.Json(new {unreadMsgs = unreadNotifications}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("my-notifications")]
        public ActionResult MyNotifications()
        {
            if (this.UserProfile != null)
            {
                var myNotifications = 
                    this.UserProfile.RecievedComments
                    .Where(u => u.IsRead == false)
                    .AsQueryable()
                    .ProjectTo<CommentViewModel>()
                    .ToList();
                return this.View(myNotifications);
            }

            return this.RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [Route("preview-notification")]
        public ActionResult PreviewNotification(int id)
        {
            var notification = 
                this.Data.Comments.All()
                .Where(c => c.Id == id)
                .ProjectTo<CommentViewModel>()
                .ToList();
            return this.View("DisplayTemplates/NotificationViewModel", notification);
        }

        [HttpGet]
        [Route("my-ads")]
        [OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult MyAds(int page = 1)
        {
            if (this.UserProfile != null)
            {
                var myAds =
                    this.Populator.GetUserAds(this.UserProfile.Id)
                        .AsQueryable()
                        .ProjectTo<AdViewModel>()
                        .ToList();

                return this.View(myAds.ToPagedList(page, GlobalConstants.AdsPageSize));
            }

            return this.RedirectToAction("Login", "Account");
        }

        private void SaveFileToFileSystem(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(this.Server.MapPath("~/Files/UserImages"), fileName);
                file.SaveAs(path);
            }
        }
    }
}