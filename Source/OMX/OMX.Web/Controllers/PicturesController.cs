namespace OMX.Web.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    using OMX.Common;
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

        public ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(
                        this.Server.MapPath(GlobalConstants.TemporaryFilesLocation),
                        fileName);

                    // The files are not actually saved 
                    // file.SaveAs(physicalPath);
                }
                this.TempData["uploaded-pics"] = files;
            }

            // Return an empty string to signify success
            return this.Content(string.Empty);
        }

        public ActionResult Remove(string[] fileNames)
        {
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(
                        this.Server.MapPath(GlobalConstants.TemporaryFilesLocation),
                        fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed
                        // System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return this.Content(string.Empty);
        }
    }
}