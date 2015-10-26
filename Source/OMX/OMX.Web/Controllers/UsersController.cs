namespace OMX.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;

    using OMX.Data.UoW;
    using OMX.Web.Models.ViewModels;


    [RoutePrefix("Users")]
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
        [Route("UploadProfileImage")]
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