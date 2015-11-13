namespace OMX.Web.Controllers
{
    using System.Web.Mvc;

    using OMX.Data.UoW;

    [Authorize]
    public class PicturesController : BaseController
    {
        public PicturesController(IOMXData data)
            : base(data)
        {
        }

        public ActionResult Delete(int id)
        {
            var picture = this.Data.Pictures.GetById(id);
            if (picture == null)
            {
                return this.HttpNotFound("Picture with such id does not exists");
            }
            this.Data.Pictures.Delete(picture.Id);
            this.Data.SaveChanges();
            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
    }
}