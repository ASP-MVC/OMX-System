namespace OMX.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using OMX.Data.UoW;
    using OMX.Models;
    using OMX.Web.Models.BindingModels;

    [Authorize]
    public class CommentsController : BaseController
    {
        public CommentsController(IOMXData data)
             : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendComment(CommentBindingModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return new HttpStatusCodeResult(400, "Invalid user input");
            }

            var comment = new Comment
            {
                AuthorId = this.User.Identity.GetUserId(),
                CommentedAt = DateTime.Now,
                Content = model.Content,
                AdId = model.AdId,
                RecipientId = model.RecipientId
            };

            this.Data.Comments.Add(comment);
            this.Data.SaveChanges();

            return this.Json(
                "Successfully sended your comment!",
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ReadComment(int id)
        {
            var comment = this.Data.Comments.GetById(id);
            if (comment == null)
            {
                return this.HttpNotFound("Comment no longer exists");
            }

            comment.IsRead = true;
            this.Data.Comments.Update(comment);
            this.Data.SaveChanges();
            return this.Content(string.Empty);
        }
    }
}