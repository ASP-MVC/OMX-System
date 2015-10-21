namespace OMX.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using OMX.Common;
    using OMX.Data.UoW;
    using OMX.Web.Models.ViewModels;

    using PagedList;

    public class AdsController : BaseController
    {
        public AdsController(IOMXData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Ads(int id, int page = 1)
        {
            var ads =
                this.Data.Ads.All()
                    .OrderByDescending(a => a.CreatedOn)
                    .ThenBy(a => a.Id)
                    .Where(a => a.SubCategoryId == id)
                    .Project()
                    .To<AdViewModel>();

            return this.View(ads.ToPagedList(page, GlobalConstants.AdsPageSize));
        }

        [HttpGet]
        public ActionResult PreviewAdById(int id)
        {
            var ad = this.Data.Ads.GetById(id);
            if (ad == null)
            {
                return this.HttpNotFound("Ad no longer exists");
            }

            var viewModel = Mapper.Map<AdViewModel>(ad);

            return this.View(viewModel);
        }
    }
}