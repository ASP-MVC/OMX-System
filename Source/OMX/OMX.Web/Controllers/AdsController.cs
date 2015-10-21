namespace OMX.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using OMX.Data.UoW;
    using OMX.Web.Models.ViewModels;

    public class AdsController : BaseController
    {
        public AdsController(IOMXData data)
            : base(data)
        {
        }

        // GET: Ads for sub category
        public ActionResult Ads(int id)
        {
            var ads =
                this.Data.Ads.All()
                    .OrderByDescending(a => a.CreatedOn)
                    .ThenBy(a => a.Id)
                    .Where(a => a.SubCategoryId == id)
                    .Project()
                    .To<AdViewModel>()
                    .ToList();

            return this.View(ads);
        }
    }
}